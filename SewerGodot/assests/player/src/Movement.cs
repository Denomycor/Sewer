using Godot;
using System;
/* handles the player movement
 *
 */
public class Movement : KinematicBody2D
{

    private Vector2 _moveDirection;

    //sets the movement direction
    public override void _Process(float delta)
    {
        _moveDirection = GetMovementDirtection();
    }

    //moves the player node to the intended direnction at player movement speed
    public override void _PhysicsProcess(float delta)
    {
        MoveAndSlide(_moveDirection * PlayerStats.movement_speed, Vector2.Up);
    }
    
    //returns movement direction
    private Vector2 GetMovementDirtection(){

        int x=0, y=0;

        if(Input.IsActionPressed("MoveUp")){
            y = -1;
        }

        if(Input.IsActionPressed("MoveDown")){
            y += 1;
        }

        if(Input.IsActionPressed("MoveRight")){
            x = 1;
        }

        if(Input.IsActionPressed("MoveLeft")){
            x -= 1;
        }

        return new Vector2(x,y).Normalized();
    }
}
