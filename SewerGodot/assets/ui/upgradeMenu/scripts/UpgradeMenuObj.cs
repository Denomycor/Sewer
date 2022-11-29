using Godot;

/* Upgrade Object on the Upgrade Menu, handles a lot of stuff
 *
 */
public class UpgradeMenuObj : Control {

    //Node vars
    public UpgradeMenu upgradeMenu {get;set;}
    public UpgradeMenuTiles upgradeMenuTiles {get;set;}

    //Stat vars
    public UpgradeInfo info {get;set;}
    public bool isStatic {get;set;} = false;

    //State vars
    public bool inUse {get;set;} = false;



    public void Init(UpgradeMenu upgradeMenu){
        this.upgradeMenu = upgradeMenu;
        upgradeMenuTiles = upgradeMenu.upgradeMenuTiles;
    }



    public override object GetDragData(Vector2 _){
        return this;
    }


/// Drag system - Checks on where it can be dropped

    //Check if a given UpgradeMenuObj can be placed at the grid
    public bool CanGoOnGrid(){
        return inUse && !isStatic;
    }

    public bool CanGoOnTile(Vector2 position){
        //TODO: Can this objs be palced on this cell?
        return true;
    }

    //Always can place on other obj place unless they are both on grid
    public bool CanGoOnThis(UpgradeMenuObj obj){
        //TODO: Can the obj(s) going on the grid be placed in the cell?
        return this.inUse || obj.inUse;
    }

    public override bool CanDropData(Vector2 position, object data) {
        UpgradeMenuObj obj = data as UpgradeMenuObj;
        if(obj != null){
            //Is a UpgradeMenuObj
            return CanGoOnThis(obj);
        }

        return false;
    }


/// Drag system - Places objs in new positions

    //Switch places with objs
    public void SwitchPos(UpgradeMenuObj obj2){
        Vector2 temp = this.RectPosition;
        this.RectPosition = obj2.RectPosition;
        obj2.RectPosition = temp;
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
    }

    //Place on grid
    public void PlaceOnGrid(){
        inUse = false;
        GetParent().RemoveChild(this);
        upgradeMenu.grid.AddChild(this);
    }

    //Place on TileMap
    public void PlaceOnTiles(Vector2 position){
        TileMap tile = upgradeMenu.upgradeMenuTiles.tileMap;
        
        inUse = true;
        GetParent().RemoveChild(this);
        tile.AddChild(this);
        RectPosition = tile.WorldToMap(position - tile.Position) * UpgradeMenu.IEM_SIZE;

        //TODO: ReCalculate state of objs after connections
    }

    public override void DropData(Vector2 position, object data) {
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
