using Godot;
using System.Collections.Generic;

public class Root : Upgrade {
    
    public Root()
        :base("Root", "res://assets/ui/upgradeMenu/sprites/root.png", Type.ROOT, Rarity.COMMON)
    {
    }

    public override string GetDescription() {
        return "This is you";
    }

    public override int GetValue(){
        return 1;
    }

    public override void InitConnections(){
        this.connectionsMap = new Dictionary<Godot.Vector2, int>();
        connectionsMap.Add(Vector2.Up, 1);
        connectionsMap.Add(Vector2.Down, 1);
        connectionsMap.Add(Vector2.Left, 1);
        connectionsMap.Add(Vector2.Right, 1);
    }

    public override void InstallImpl(Player player){
        //No installation
    }

    public override void RemoveImpl(Player player){
        //No remove
    }

}
