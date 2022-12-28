using Godot;
using System.Collections.Generic;

public class TestOnShoot : HookUpgrade {
    public TestOnShoot()
        :base("Test Multishot", "res://yellow.png", Upgrade.Type.TEST, Upgrade.Rarity.TEST)
    {
    }

    protected override void AddAction(Player player) {
        player.gun.OnShoot += Print;
    }

    protected override void RemoveAction(Player player){
        player.gun.OnShoot -= Print;
    }

    public static void Print(ProjectileEntity p){
        GD.Print("Ola");
    }

    public override string GetDescription(){
        return "Add onShoot effect";
    }

    public override int GetValue(){
        return -1;
    }

    public override void InitConnections(){
        this.connectionsMap = new Dictionary<Godot.Vector2, int>();
        connectionsMap.Add(Vector2.Up, 1);
        connectionsMap.Add(Vector2.Down, 1);
        connectionsMap.Add(Vector2.Left, -1);
        connectionsMap.Add(Vector2.Right, -1);
    }
}