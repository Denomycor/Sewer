using Godot;
using System;

public class Cursor : Polygon2D
{

    Node2D _parent;

    public override void _Ready()
    {
        _parent = (Node2D)GetParent();
    }

    public override void _Process(float delta)
    {
        Rotation = GetAngleFromMouse();
        GD.Print("Player " + _parent.ZIndex);
        GD.Print("Player Cursor" + ZIndex);
    }

    //returns angle of mouse in relation to center of the player
    private float GetAngleFromMouse(){
        Vector2 mousePosition = _parent.GetLocalMousePosition();
        float angle = 0;

        if(mousePosition.Length()!=0)
            angle = Mathf.Acos(mousePosition.x/mousePosition.Length());
        if(mousePosition.y<0){
            angle *= -1;
            ZIndex = _parent.ZIndex-1;
        }else{
            ZIndex = _parent.ZIndex+1;        }

        return angle;
    }

}
