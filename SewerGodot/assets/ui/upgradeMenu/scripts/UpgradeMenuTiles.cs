using Godot;

/* Handles operations of the Upgrade Menu on the tile system
 * 
 */
public class UpgradeMenuTiles : Node2D{

    //Node vars
    public TileMap tileMap {get;set;}
    public UpgradeMenu upgradeMenu {get;set;}

    //State vars
    public bool beingDragged {get;set;}


    public override void _Ready() {
        tileMap = GetNode<TileMap>("TileMap");

    }

    public override void _UnhandledInput(InputEvent e){
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

}
