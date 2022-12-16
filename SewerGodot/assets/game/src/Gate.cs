using Godot;
using System;

public class Gate : StaticBody2D
{
    //deploy vars
    [Export] private Side side;
    [Export] private float deployDistance = 48;
    private Vector2 deployPos;

    //connection vars
    public int connectingRoomIndex;
    public int connectingGateIndex;


    public override void _Ready()
    {
        deployPos = GetDeployPosition(side);
    }

    //returns the polistion on wich to deploy the player if it enters the room through this gate
    private Vector2 GetDeployPosition(Side side)
    {
        Vector2 dir = new Vector2(0,0);
        switch(side){

            case Side.TOP:{
                dir = new Vector2(0,1) * deployDistance;
                break;
            }

            case Side.LEFT:{
                dir = new Vector2(1,0) * deployDistance;
                break;
            }

            case Side.BOTTOM:{
                dir = new Vector2(0,-1) * deployDistance;
                break;
            }

            case Side.RIGHT:{
                dir = new Vector2(-1,0) * deployDistance;
                break;
            }
        }
        return dir;
    }

}

//enum listing all side options
public enum Side{
    TOP,
    LEFT,
    BOTTOM,
    RIGHT
}