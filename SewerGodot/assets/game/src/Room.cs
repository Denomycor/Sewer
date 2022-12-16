using Godot;
using System.Collections.Generic;

public class Room : Node2D
{
    public List<Gate> gateList = new List<Gate>();

    public override async void _Ready()
    {
        //wait for tile map to replace tiles with scenes (to instantiate the gate objects)
        await ToSignal(GetTree(), "idle_frame");
        FindGates();
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
}
