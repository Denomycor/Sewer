using Godot;
using System;

/* Handles basic player AI and holds stats
 *
 */
public class Enemy : Entity {
    //refence to player
    private Node2D _player;

    //stats vars
    private float _moveSpeed = 200f;
    private float _range = 120f;
    private float _damage;
    private float _armour;



    //find player reference
    public override void _Ready(){
        _player = (Node2D)GetParent().FindNode("Player");
    }

    public override void _PhysicsProcess(float delta){
        //move towards player if enemy is out of range
        if(GetDistanceToPlayer()>=_range)
            Move(GetDirectionToPlayer());
    }

    //move in the players direction
    protected void Move(Vector2 Direction){
        MoveAndSlide(Direction * _moveSpeed);
    }

    protected void Attack(Vector2 Direction){
        throw new NotImplementedException();
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
