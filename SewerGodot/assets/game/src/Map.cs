using Godot;
using System.Collections.Generic;

public class Map : Node2D
{
    //singleton pattern
    private Map(){
        instance = this;
    }
    public static Map GetInstance(){
        if (instance == null){
            instance = new Map();
        }
        return instance;
    }
    private static Map instance;

    //vars
    [Export]
    private string[] roomLib = {"res://assets/game/scenes/Room.tscn", "res://assets/game/scenes/Room1.tscn"};
    private List<Room> roomList = new List<Room>();
    private Player player;
    public Room currentRoom;
    public int currentRoomIndex;

    public override void _Ready()
    {
        player = (Player)FindNode("Player");
        //FIXME: loading a map just for testing
        roomList.Add(InstantiateRoom(0));
        for(int i=0; i<4; i++){
            roomList.Add(InstantiateRoom(1));
        }
        ConnectGates(0,0,1,3);
        ConnectGates(0,1,1,2);
        ConnectGates(0,2,1,1);
        ConnectGates(0,3,1,0);
        EnterRoom(0,0);
    }

    //sets values for gate connections
    private void ConnectGates(int roomIndex1, int gateIndex1, int roomIndex2, int gateIndex2){
        //set variables for 1st gate
        if(roomList[roomIndex1]!=null){
            roomList[roomIndex1].GetGate(gateIndex1)?.Connect(roomIndex2, gateIndex2);
        }
        //set variables for 2nd gate
        if(roomList[roomIndex1]!=null){
            roomList[roomIndex2].GetGate(gateIndex2)?.Connect(roomIndex1, gateIndex1);
        }
    }

    //exits room
    public void ExitRoom(){
        currentRoom.RemoveChild(player);
        AddChild(player);
        RemoveChild(roomList[currentRoomIndex]);
    }

    //enters room
    public void EnterRoom(int roomIndex, int gateIndex){
        AddChild(roomList[roomIndex]);
        currentRoomIndex = roomIndex;
        currentRoom = roomList[currentRoomIndex];

        RemoveChild(player);
        currentRoom.AddChild(player);
        currentRoom.GetGate(gateIndex).DeployPlayer(player);
    }

    public void MoveToRoom(int roomIndex, int gateIndex){
        currentRoom.RemoveChild(player);
        RemoveChild(currentRoom);

        CallDeferred("add_child", roomList[roomIndex]);
        currentRoomIndex = roomIndex;
        currentRoom = roomList[currentRoomIndex];

        //currentRoom.AddChild(player);
        currentRoom.GetGate(gateIndex).DeployPlayer(player);
    }

    //returns an instance of a room taken from the room library
    private Room InstantiateRoom(int roomLibIndex){
        Room room = (Room)ResourceLoader.Load<PackedScene>(roomLib[roomLibIndex]).Instance();
        room.FindGates();
        return room;
    }
}
