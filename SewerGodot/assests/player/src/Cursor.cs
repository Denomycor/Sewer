using Godot;
using System;
/* Script for setting player cursor to the intended position and height
 *
 */
public class Cursor : Polygon2D
{
    //reference to player
    private Node2D _parent;

    //initialize reference
    public override void _Ready()
    {
        _parent = (Node2D)GetParent();
    }

    //sets cursor rotation to the angle pointing to the mouse
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
        //sets zindex of cursor depending on if it is above or below player
        if(mousePosition.y<0){
            angle *= -1;
            ZIndex = _parent.ZIndex-1;
        }else{
            ZIndex = _parent.ZIndex+1;        }

        return angle;
    }

}
