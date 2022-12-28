using Godot;
using System.Collections.Generic;

public class TestSpeed : StatUpgrade<float>{
    
    public TestSpeed()
        :base("Test Multishot", "res://yellow.png", Upgrade.Type.TEST, Upgrade.Rarity.TEST, 700f)
    {
    }

    public override float Fold(Stat<float> acc){
        return acc.value + value;
    }

    protected override void BindToStat(Player player){
        player.gun.speedUpgrades.AddLast(this);
    }

    protected override void UnbindFromStat(Player player){
        player.gun.speedUpgrades.Remove(this);
    }

    public override string GetDescription(){
        return "Add speed to projectiles";
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
