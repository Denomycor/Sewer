using Godot;
using System.Collections.Generic;

/* Upgrade Object on the Upgrade Menu, handles a lot of stuff
 *
 */
public class UpgradeMenuObj : Control {

    //Node vars
    public UpgradeMenu upgradeMenu;
    public UpgradeMenuTiles upgradeMenuTiles;
    public UpgradeMenuContext upgradeMenuContext;

    //Stat vars
    public Upgrade upgradeRef;
    public readonly bool isStatic = false;

    //State vars
    public bool inUse = false;
    public Matrix<UpgradeMenuObj> recordRef;
    
    //Fold state vars
    public bool visited = false;
    public bool initialized = false;


    /* FIXME: TEMP Simulate having an Upgrade object*/
    public UpgradeMenuObj(){
        this.upgradeRef = new Upgrade();
        upgradeRef.connectionsMap = new Dictionary<Vector2, int>(4);
        upgradeRef.connectionsMap.Add(Vector2.Up, 1);
        upgradeRef.connectionsMap.Add(Vector2.Right, -1);
        upgradeRef.connectionsMap.Add(Vector2.Down, 1);
        upgradeRef.connectionsMap.Add(Vector2.Left, -1);
    }
    /* TEMP */

    public void Init(UpgradeMenu upgradeMenu){
        this.upgradeMenu = upgradeMenu;
        upgradeMenuTiles = upgradeMenu.upgradeMenuTiles;
        recordRef = upgradeMenu.record;
        upgradeMenuContext = upgradeMenu.GetNode<UpgradeMenuContext>("UpgradeMenuContext");

    }


    //Godot functions
    public override void _GuiInput(InputEvent e){
        if(e is InputEventMouseButton){
            InputEventMouseButton emb = e as InputEventMouseButton;
            if(emb.ButtonIndex == (int)ButtonList.Right && emb.Pressed){
                upgradeMenuContext.Reload(this);
            }

            if(emb.ButtonIndex == (int)ButtonList.Left && emb.Pressed && emb.Doubleclick && CanGoOnGrid()){
                PlaceOnGrid();
            }
        }
    }

    public override object GetDragData(Vector2 _){
        return this;
    }



    //Gets the cell position on the tileMap
    public Vector2 GetTilePos(){
        return upgradeMenuTiles.tileMap.WorldToMap(RectPosition);
    }


    //Sets obj position given the desired cell coordinates
    public void SetPosThroughTile(Vector2 pos){
        RectPosition = upgradeMenuTiles.tileMap.MapToWorld(pos);
    }


    //Get directions where there can be a neighboor
    public LinkedList<Vector2> ApplicableDirections(Vector2 cellCoord){
        LinkedList<Vector2> ll = new LinkedList<Vector2>();
        Rect2 tile = upgradeMenuTiles.tileMap.GetUsedRect();

        if(cellCoord.y > tile.Position.y)
            ll.AddLast(Vector2.Up);
        if(cellCoord.y < tile.End.y-1)
            ll.AddLast(Vector2.Down);
        if(cellCoord.x > tile.Position.x)
            ll.AddLast(Vector2.Left);
        if(cellCoord.x < tile.End.x-1)
            ll.AddLast(Vector2.Right);
        return ll;
    }


    //Can 2 connections be next to each other
    public static bool CanBeNeighbours(int st, int nd){
        return ((st == nd) || (nd == 99) || (st == 99)) && (st!=-1 && nd!=-1);
    }

    //Are 2 sides connected
    public static bool AreConnected(int st, int nd){
        return CanBeNeighbours(st, nd) && (st != 0);
    }


/// Drag system - Checks on where it can be dropped

    //Can this go on grid
    public bool CanGoOnGrid(){
        return inUse && !isStatic;
    }

    //Can this go on tile on this global pos
    public bool CanGoOnTile(Vector2 position){
        Vector2 tileCoord = upgradeMenuTiles.tileMap.WorldToMap(position - upgradeMenuTiles.tileMap.Position);
        bool result = true;

        foreach(Vector2 d in ApplicableDirections(tileCoord)){
            UpgradeMenuObj other = upgradeMenu.record.GetRelative(tileCoord, d);
            if(other != null){
                if(other.GetInstanceId() != this.GetInstanceId()){
                    int o = other.upgradeRef.connectionsMap[d*-1];
                    int t = this.upgradeRef.connectionsMap[d];
                    result = result && CanBeNeighbours(o, t);
                }
            }
        }
        return result;
    }

    //Can obj go on this
    public bool CanGoOnThis(UpgradeMenuObj obj){
        if(obj.inUse && this.inUse){
            return obj.CanGoOnTile(this.RectGlobalPosition) && this.CanGoOnTile(obj.RectGlobalPosition);
        }else if(obj.inUse && !this.inUse){
            return obj.CanGoOnGrid() && this.CanGoOnTile(obj.RectGlobalPosition);
        } else if(!obj.inUse && this.inUse){
            return obj.CanGoOnTile(this.RectGlobalPosition) && this.CanGoOnGrid();
        } else if(!obj.inUse && !this.inUse){
            return obj.CanGoOnGrid() && this.CanGoOnGrid();
        }

        return false;
    }

    public override bool CanDropData(Vector2 position, object data) {
        UpgradeMenuObj obj = data as UpgradeMenuObj;
        if(obj != null){
            if(obj.GetInstanceId() != this.GetInstanceId()){
                //Is a UpgradeMenuObj
                return CanGoOnThis(obj);
            }
        }
        return false;
    }


/// Drag system - Places objs in new positions

    //Switch places with objs
    public void SwitchPos(UpgradeMenuObj obj){
        Vector2 temp = this.RectPosition;
        this.RectPosition = obj.RectPosition;
        obj.RectPosition = temp;

        recordRef.Set(obj.GetTilePos(), obj);
        recordRef.Set(this.GetTilePos(), this);
    }

    //Switch containers with obj
    public void SwitchContainers(UpgradeMenuObj obj){
        obj.inUse = true;
        obj.GetParent().RemoveChild(obj);
        this.upgradeMenuTiles.tileMap.AddChild(obj);
        obj.RectPosition = this.RectPosition;


        this.inUse = false;
        this.GetParent().RemoveChild(this);
        this.upgradeMenu.grid.AddChild(this);

        recordRef.Set(obj.GetTilePos(), obj);
    }

    //Place on grid
    public void PlaceOnGrid(){
        recordRef.Set(this.GetTilePos(), null);

        inUse = false;
        GetParent().RemoveChild(this);
        upgradeMenu.grid.AddChild(this);
    }

    //Place on TileMap
    public void PlaceOnTiles(Vector2 position){
        TileMap tile = upgradeMenuTiles.tileMap;
        
        if(this.inUse){
            recordRef.Set(this.GetTilePos(), null);
        }

        inUse = true;
        GetParent().RemoveChild(this);
        tile.AddChild(this);
        SetPosThroughTile(tile.WorldToMap(position - tile.Position));
        recordRef.Set(this.GetTilePos(), this);
    }

    public override void DropData(Vector2 _, object data) {
        UpgradeMenuObj obj = data as UpgradeMenuObj;
        if(obj != null){
            if(this.inUse && obj.inUse){
                SwitchPos(obj);
            }else if(this.inUse && !obj.inUse){
                SwitchContainers(obj);
            }else if(!this.inUse && obj.inUse){
                obj.PlaceOnGrid();
            }
        }
    }
    
}
