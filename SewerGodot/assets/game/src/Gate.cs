using Godot;
using System;

public class Gate : StaticBody2D
{
    //deploy vars
    [Export] private Side side;
    [Export] private float deployDistance = 48;
    private Vector2 deployPos;
    private Area2D exitArea;


    //connection vars default = -1 (MUST BE)
    private int connectingRoomIndex=-1;
    private int connectingGateIndex=-1;


    public override void _Ready()
    {
        exitArea = (Area2D)FindNode("ExitArea");
        exitArea.Connect("body_shape_entered", this, nameof(_on_ExitArea_body_shape_entered));
        deployPos = GetDeployPosition(side);
    }

    //returns the polistion on wich to deploy the player if it enters the room through this gate
    private Vector2 GetDeployPosition(Side side)
    {
        Vector2 pos = new Vector2(0,0);
        switch(side){

            case Side.TOP:{
                pos = new Vector2(0,1) * deployDistance;
                break;
            }

            case Side.LEFT:{
                pos = new Vector2(1,0) * deployDistance;
                break;
            }

            case Side.BOTTOM:{
                pos = new Vector2(0,-1) * deployDistance;
                break;
            }

            case Side.RIGHT:{
                pos = new Vector2(-1,0) * deployDistance;
                break;
            }
        }
        return ToGlobal(pos);
    }

    //connect gate
    public void Connect(int roomIndex, int gateIndex){
        connectingRoomIndex = roomIndex;
        connectingGateIndex = gateIndex;
    }

    //on entering the doors vicinity
    public void _on_ExitArea_body_shape_entered(RID body_rid, Node body, int index, int local_index){
        //check if it was player
        if(body is Player){
            //check if there is a connection
            if(IsConnected()){
                //enter the room
                Map.GetInstance().MoveToRoom(connectingGateIndex, connectingRoomIndex);
            }
        }
    }

    //deploys player in the room scene at intended position
    public void DeployPlayer(Player player){
        player.Position = deployPos;
    }

    //returns a boolean checking if there is a connection
    public bool IsConnected(){
        if(connectingRoomIndex != -1 && connectingGateIndex!=-1){
            return true;
        }else{
            return false;
        }

    }
}

//enum listing all side options
public enum Side{
    TOP,
    LEFT,
    BOTTOM,
    RIGHT
}