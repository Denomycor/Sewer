using Godot;

/* Right Panel grouping objects
 *
 */
public class UpgradeMenuScroll : ScrollContainer {
    
    //Node vars
    public GridContainer grid {get;set;}



    public override void _Ready() {
        grid = GetNode<GridContainer>("List/Grid");
    }



    //Check if a given UpgradeMenuObj can be placed at position in the grid
    private bool CheckGridPlacement(Vector2 position, UpgradeMenuObj obj){
        return obj.inUse && !obj.isStatic;
    }

    public override bool CanDropData(Vector2 position, object data){
        UpgradeMenuObj obj = data as UpgradeMenuObj;
        if(obj != null){
            //Is a UpgradeMenuObj
            return CheckGridPlacement(position, obj);
        }

        return false;
    }



    //Place UpgradeMenuObj in the grid
    private void PlaceObj(Vector2 position, UpgradeMenuObj obj){
        obj.inUse = false;
        obj.GetParent().RemoveChild(obj);
        this.grid.AddChild(obj);

        //TODO: Calculate state of objs after removing connections
    }

    public override void DropData(Vector2 position, object data) {
        UpgradeMenuObj obj = data as UpgradeMenuObj;
        if(obj != null){
            //Is a UpgradeMenuObj
            PlaceObj(position, obj);
        }
    }

}
