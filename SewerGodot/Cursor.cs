using Godot;
using System;

public class Cursor : Polygon2D
{
    public override void _Process(float delta)
    {
        Rotation = GetAngleFromMouse();
    }

    //returns angle of mouse in relation to center of the player
    private float GetAngleFromMouse(){
        CanvasItem parent = (CanvasItem)GetParent();
        Vector2 mousePosition = parent.GetLocalMousePosition();
        float angle = 0;

        if(mousePosition.Length()!=0)
            angle = Mathf.Acos(mousePosition.x/mousePosition.Length());
        if(mousePosition.y<0)
            angle *= -1;

        return angle;
    }

}
