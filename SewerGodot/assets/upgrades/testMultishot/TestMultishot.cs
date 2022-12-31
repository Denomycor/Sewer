using  System.Collections.Generic;
using Godot;

public class TestMultishot : StatUpgrade<int>{
   
    public TestMultishot()
        :base("Test Multishot", "res://assets/ui/upgradeMenu/sprites/blue.png", Upgrade.Type.TEST, Upgrade.Rarity.TEST, 1)
    {
    }

    public override int Fold(Stat<int> acc){
        return acc.value + value;
    }

    public override string GetDescription(){
        return "Add multishot to projectiles";
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

    protected override void BindToStat(Player player){
        player.gun.multishotUpgrades.AddLast(this);
    }

    protected override void UnbindFromStat(Player player){
        player.gun.multishotUpgrades.Remove(this);
    }
}