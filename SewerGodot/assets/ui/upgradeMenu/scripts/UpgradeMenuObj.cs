using Godot;

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
    public bool isStatic = false;

    //State vars
    public bool inUse = false;



    public void Init(UpgradeMenu upgradeMenu){
        this.upgradeMenu = upgradeMenu;
        upgradeMenuTiles = upgradeMenu.upgradeMenuTiles;
        upgradeMenuContext = upgradeMenu.GetNode<UpgradeMenuContext>("UpgradeMenuContext");
    }


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


/// Drag system - Checks on where it can be dropped

    //Check if a given UpgradeMenuObj can be placed at the grid
    public bool CanGoOnGrid(){
        return inUse && !isStatic;
    }

    //Checks if an obj can be placed on tile
    public bool CanGoOnTile(Vector2 position){
        return true;
    }

    //Always can place on other obj place unless they are both on grid
    public bool CanGoOnThis(UpgradeMenuObj obj){
        if((this.isStatic && obj.inUse) || (this.inUse && obj.isStatic)){
            return true;
        }else{
            return this.inUse || obj.inUse;
        }
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
