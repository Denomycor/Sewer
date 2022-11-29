using Godot;
using System;

public class EnemyCursor : Polygon2D
{

    private Node2D _parent;
    private Node2D _player;

    public override void _Ready()
    {
        _parent = (Node2D)GetParent();
        _player = (Node2D)GetParent().GetParent().FindNode("Player");
    }

    public override void _Process(float delta)
    {
        if(_player!=null)
            Rotation = GetAngleFromPlayer();
        GD.Print("Enemy " + _parent.ZIndex);
        GD.Print("Enemy Cursor" + ZIndex);
    }

    //returns angle of mouse in relation to center of the player
    private float GetAngleFromPlayer(){
        Vector2 playerPosition = _player.Position;
        float angle = 0;

        playerPosition = _parent.ToLocal(playerPosition);

        if(playerPosition.Length()!=0)
            angle = Mathf.Acos(playerPosition.x/playerPosition.Length());
        if(playerPosition.y<0){
            angle *= -1;
            ZIndex = -1;
        }else{
            ZIndex = 1;
        }

        return angle;
    }
}
