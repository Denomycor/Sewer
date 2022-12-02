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

    public void Pan(Vector2 relative){
        Rect2 tileArea = tileMap.GetUsedRect();
        tileArea.Size *= UpgradeMenu.IEM_SIZE;

        Vector2 final = new Vector2();
        final.x = Mathf.Clamp(tileMap.Position.x+relative.x, 1680-tileArea.End.x, 0-tileArea.Position.x);
        final.y = Mathf.Clamp(tileMap.Position.y+relative.y, 1080-tileArea.End.y, 0-tileArea.Position.y);

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
