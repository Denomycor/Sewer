using Godot;
using System;

public class Enemy : Entity
{
    private Node2D _player;
    private float _moveSpeed = 200f;
    private float _range = 120f;
    private float _damage;
    private float _armour;

    public override void _Ready()
    {
        _player = (Node2D)GetParent().FindNode("Player");
    }

    public override void _PhysicsProcess(float delta)
    {
        //move towards player if enemy is out of range
        if(GetDistanceToPlayer()>=_range)
            Move(GetDirectionToPlayer());
    }

    //move in the players direction
    protected void Move(Vector2 Direction){
        MoveAndSlide(Direction * _moveSpeed);
    }

    protected void Attack(Vector2 Direction){

    }


    //returns a unit verctor in the direction of the player
    private Vector2 GetDirectionToPlayer(){
        return (_player.Position - Position).Normalized();
    }

    //returns distance to player
    private float GetDistanceToPlayer(){
        return (_player.Position - Position).Length();
    }
}
