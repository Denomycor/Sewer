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
            //TODO: not being able to paan over the limit of tileMap
            tileMap.Position += (e as InputEventMouseMotion).Relative;
        }
    }



    public override bool CanDropData(Vector2 position, object data) {
        UpgradeMenuObj obj = data as UpgradeMenuObj;
        if(obj != null){
            //Is a UpgradeMenuObj
            return obj.CanGoOnTile(position);
        }

        return false;
    }

    public override void DropData(Vector2 position, object data) {
        UpgradeMenuObj obj = data as UpgradeMenuObj;
        if(obj != null){
            //Is a UpgradeMenuObj
            obj.PlaceOnTiles(position);
        }
    }

}
