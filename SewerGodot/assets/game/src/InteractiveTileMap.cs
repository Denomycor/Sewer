using Godot;
using System.Collections.Generic;

/*Script for replacing maked tiles with their corresponding scenes
 *
 */

public class InteractiveTileMap : TileMap
{
    
    private Vector2 HALF_CELL_SIZE;
    private Node2D room;
    [Export] 
    private Dictionary<int, PackedScene> TileIDToScene = new Dictionary<int, PackedScene>(){
        {2 , ResourceLoader.Load<PackedScene>("res://assets/game/scenes/TopGate.tscn")},
        {3 , ResourceLoader.Load<PackedScene>("res://assets/game/scenes/RightGate.tscn")},
        {4 , ResourceLoader.Load<PackedScene>("res://assets/game/scenes/LeftGate.tscn")},
        {5 , ResourceLoader.Load<PackedScene>("res://assets/game/scenes/BottomGate.tscn")}
    };

    public override async void _Ready()
    {
        room = GetRoom();
        HALF_CELL_SIZE = CellSize/2;
        await ToSignal(GetTree(), "idle_frame");
        ReplaceTiles(TileIDToScene);
    }

    // goes through all tiles in the tile map and replaces the tiles that belong to the dictionary with the corresponding scenes
    private void ReplaceTiles(Dictionary<int, PackedScene> dict){
        foreach(Vector2 tilePos in GetUsedCells()){
            int ID = GetCellv(tilePos);

            if(dict.ContainsKey(ID)){
                PackedScene scene = dict[ID];
                ReplaceTileWithScene(tilePos, scene, room);
            }
        }
    }

    //places a scene in a given tile position adding it to the room hierarchy
    private void ReplaceTileWithScene(Vector2 tilePos, PackedScene scene, Node2D roomNode){
        //clear tile
        if (GetCellv(tilePos)!=TileMap.InvalidCell){
            SetCellv(tilePos, -1);
            UpdateBitmaskRegion();
        }

        //place scene in tile position
        if(scene != null){
            //instantiate scene
            Node2D sceneInstance = scene.Instance<Node2D>();
            if(roomNode != null){
                roomNode.AddChild(sceneInstance);
            }
            //put it in the right coordinates
            Vector2 position = MapToWorld(tilePos) + HALF_CELL_SIZE;
            sceneInstance.Position = position;
        }
    }

    //returns room node2D
    private Node2D GetRoom(){
        return GetParent<Node2D>();
    }

}
