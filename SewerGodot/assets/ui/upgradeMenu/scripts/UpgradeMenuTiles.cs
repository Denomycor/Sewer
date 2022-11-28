using Godot;

/* Tile system, handles drag logic in the tiles and panning mechanics
 * 
 */
public class UpgradeMenuTiles : Control{

    //Node vars
    public TileMap tileMap {get;set;}

    //State vars
    public bool beingDragged {get;set;}



    public override void _Ready() {
        tileMap = GetNode<TileMap>("TileMap");
    }



    public override void _GuiInput(InputEvent e){
        if(e.IsActionPressed("pan")){
            beingDragged = true;
        }
        if(e.IsActionReleased("pan")){
            beingDragged = false;
        }
        if(e is InputEventMouseMotion && beingDragged){
            tileMap.Position += (e as InputEventMouseMotion).Relative;
        }
    }



    //Check if a given UpgradeMenuObj can be placed at position in the tileMap
    private bool CheckTilePlacement(Vector2 position, UpgradeMenuObj obj){
        //TODO: check compatibility of connections
        return true;
    }

    public override bool CanDropData(Vector2 position, object data) {
        UpgradeMenuObj obj = data as UpgradeMenuObj;
        if(obj != null){
            //Is a UpgradeMenuObj
            return CheckTilePlacement(position, obj);
        }

        return false;
    }



    //Place UpgradeMenuObj in the tileMap and snap to grid
    private void PlaceObj(Vector2 position, UpgradeMenuObj obj){
        obj.inUse = true;
        obj.GetParent().RemoveChild(obj);
        this.tileMap.AddChild(obj);
        obj.RectPosition = tileMap.WorldToMap(position - tileMap.Position) * UpgradeMenu.IEM_SIZE;

        //TODO: Calculate state of objs after connections
    }

    public override void DropData(Vector2 position, object data) {
        UpgradeMenuObj obj = data as UpgradeMenuObj;
        if(obj != null){
            //Is a UpgradeMenuObj
            PlaceObj(position, obj);
        }
    }

}
