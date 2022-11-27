using Godot;
using System;

public class Cursor : Polygon2D
{

    CanvasItem _parent;

    public override void _Ready()
    {
        _parent = (CanvasItem)GetParent();
    }

    public override void _Process(float delta)
    {
        Rotation = GetAngleFromMouse();
    }

    //returns angle of mouse in relation to center of the player
    private float GetAngleFromMouse(){
        Vector2 mousePosition = _parent.GetLocalMousePosition();
        float angle = 0;

        if(mousePosition.Length()!=0)
            angle = Mathf.Acos(mousePosition.x/mousePosition.Length());
        if(mousePosition.y<0){
            angle *= -1;
            ZIndex = -1;
        }else{
            ZIndex = 1;
        }

        return angle;
    }

}
