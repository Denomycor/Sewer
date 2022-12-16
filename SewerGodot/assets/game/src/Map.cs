using Godot;
using System.Collections.Generic;

public class Map : Node
{
    [Export]
    private PackedScene[] roomLib;
    private List<Room> roomList;
    public Room currentRoom;
    public int currentRoomIndex;

    //sets values for gate connections
    private void connectGates(int roomIndex1, int gateIndex1, int roomIndex2, int gateIndex2){
        //set variables for 1st gate
        if(roomList[roomIndex1]!=null){
            if(roomList[roomIndex1].gateList[gateIndex1]!=null){
                roomList[roomIndex1].gateList[gateIndex1].connectingRoomIndex = roomIndex2;
                roomList[roomIndex1].gateList[gateIndex1].connectingGateIndex = gateIndex2;
            }
        }
        //set variables for 2nd gate
        if(roomList[roomIndex1]!=null){
            if(roomList[roomIndex2].gateList[gateIndex2]!=null){
                roomList[roomIndex2].gateList[gateIndex2].connectingRoomIndex = roomIndex1;
                roomList[roomIndex2].gateList[gateIndex2].connectingGateIndex = gateIndex1;
            }
        }
    }
}
