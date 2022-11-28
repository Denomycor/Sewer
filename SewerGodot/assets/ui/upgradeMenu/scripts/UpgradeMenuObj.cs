using Godot;

/* Upgrade Object on the Upgrade Menu
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



    //Always can place on other obj place unless they are both on grid
    private bool CheckTilePlacement(UpgradeMenuObj obj){
        return this.inUse || obj.inUse;
    }

    public override bool CanDropData(Vector2 position, object data) {
        UpgradeMenuObj obj = data as UpgradeMenuObj;
        if(obj != null){
            //Is a UpgradeMenuObj
            return CheckTilePlacement(obj);
        }

        return false;
    }



    //Switch place with other obj
    private void SwitchPosWith(UpgradeMenuObj obj){
        Vector2 temp = this.RectPosition;
        this.RectPosition = obj.RectPosition;
        obj.RectPosition = temp;
    }

    //Switch but other obj is on grid
    private void SwitchFromGridToTile(UpgradeMenuObj obj){
        obj.inUse = true;
        obj.GetParent().RemoveChild(obj);
        upgradeMenuTiles.tileMap.AddChild(obj);
        obj.RectPosition = this.RectPosition;

        this.inUse = false;
        this.GetParent().RemoveChild(this);
        upgradeMenu.grid.AddChild(this);
    }

    //Put in grid anyways
    private void PlaceOnGrid(UpgradeMenuObj obj){
        obj.inUse = false;
        obj.GetParent().RemoveChild(obj);
        upgradeMenu.grid.AddChild(obj);
    }

    public override void DropData(Vector2 position, object data) {
        UpgradeMenuObj obj = data as UpgradeMenuObj;
        if(obj != null){
            if(this.inUse && obj.inUse){
                SwitchPosWith(obj);
            }else if(this.inUse && !obj.inUse){
                SwitchFromGridToTile(obj);
            }else if(!this.inUse && obj.inUse){
                PlaceOnGrid(obj);
            }
        }
    }
}
