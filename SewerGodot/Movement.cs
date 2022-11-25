using Godot;
using System;

public class Movement : KinematicBody2D
{

    private Vector2 _moveDirection;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public override void _Process(float delta)
    {
        _moveDirection = GetMovementDirtection();
    }

    public override void _PhysicsProcess(float delta)
    {
        MoveAndSlide(_moveDirection * Stats.movement_speed, Vector2.Up);
    }

    // Handle input events
    public override void _Input(InputEvent inputEvent){

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

        return new Vector2(x,y);
    }
}
