using Godot;

/* Tile system, handles drag logic in the tiles and panning mechanics
 * 
 */
public class UpgradeMenuTiles : Control{

    //Node vars
    public TileMap tileMap;

    //State vars
    public bool beingDragged = false;



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
            Pan((e as InputEventMouseMotion).Relative);
        }
    }



    //Moves tileset
    public void Pan(Vector2 relative){
        Rect2 tileArea = tileMap.GetUsedRect();
        tileArea.Size *= tileMap.CellSize;
        tileArea.Position *= tileMap.CellSize;

        Vector2 final = new Vector2();
        final.x = Mathf.Clamp(tileMap.Position.x+relative.x, UpgradeMenu.PAN_LIMITS.x-tileArea.End.x, -2-tileArea.Position.x);
        final.y = Mathf.Clamp(tileMap.Position.y+relative.y, UpgradeMenu.PAN_LIMITS.y-tileArea.End.y, -2-tileArea.Position.y);

        tileMap.Position = final;
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