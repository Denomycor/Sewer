using Godot;

/* Right Panel grouping objects, handles drag logic in the right grid
 *
 */
public class UpgradeMenuScroll : ScrollContainer {
    
    //Node vars
    public GridContainer grid;



    public override void _Ready() {
        grid = GetNode<GridContainer>("List/Grid");
    }



    public override bool CanDropData(Vector2 position, object data){
        UpgradeMenuObj obj = data as UpgradeMenuObj;
        if(obj != null){
            //Is a UpgradeMenuObj
            return obj.CanGoOnGrid();
        }

        return false;
    }

    public override void DropData(Vector2 position, object data) {
        UpgradeMenuObj obj = data as UpgradeMenuObj;
        if(obj != null){
            //Is a UpgradeMenuObj
            obj.PlaceOnGrid();
        }
    }

}
