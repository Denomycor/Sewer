using Godot;
using System.Collections.Generic;

public class Room : YSort
{
    [Export]
    private List<Gate> gateList = new List<Gate>();
    private Map map;

    public Room(){
        
    }

    public override void _Ready()
    {
        //instantiate vars
        map = Map.GetInstance();
        //wait for tile map to replace tiles with scenes (to instantiate the gate objects)
        /*
        await ToSignal(GetTree(), "idle_frame");
        FindGates();
        */
    }

    //searches for gates 
    public void FindGates(){
        //find all gates in the rooms children
        foreach(Node child in GetChildren()){
            if(child is Gate){
                gateList.Add(child as Gate);
            }
        }
    }

    //returns gate from gate list
    public Gate GetGate(int gateIndex){
        if(gateList[gateIndex]!=null){
            return gateList[gateIndex];
        }
        else{return null;}
    }
}
