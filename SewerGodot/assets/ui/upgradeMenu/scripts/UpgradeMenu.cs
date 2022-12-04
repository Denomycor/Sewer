using Godot;

/* Menu for configuring the upgrade system, Root of Upgrade Menu
 *
 */
public class UpgradeMenu : Control {
    
    //Constants
    //Size of tileMap
    public static readonly int TILE_SIZE = 80;
    public static readonly Vector2[] DIRECTIONS = {Vector2.Up, Vector2.Right, Vector2.Down, Vector2.Left};

    //Node vars
    public UpgradeMenuTiles upgradeMenuTiles;
    public GridContainer grid;
    
    //State vars
    public Matrix<UpgradeMenuObj> record;


    public override void _Ready(){
        upgradeMenuTiles = GetNode<UpgradeMenuTiles>("UpgradeMenuTiles");
        grid = GetNode<GridContainer>("RightPanel/ScrollPanel/Scroll/List/Grid");

        record = new Matrix<UpgradeMenuObj>(upgradeMenuTiles.tileMap.GetUsedRect());

        //FIXME: temp, init pre-existing UpgradeMenuObj, on the final products all instances of this scene are created dinamycally
        GetNode<UpgradeMenuObj>("RightPanel/ScrollPanel/Scroll/List/Grid/UpgradeMenuObj").Init(this);
        GetNode<UpgradeMenuObj>("RightPanel/ScrollPanel/Scroll/List/Grid/UpgradeMenuObj2").Init(this);
        GetNode<UpgradeMenuObj>("RightPanel/ScrollPanel/Scroll/List/Grid/UpgradeMenuObj3").Init(this);
    }

}
