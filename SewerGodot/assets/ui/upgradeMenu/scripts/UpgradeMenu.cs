using Godot;

/* Menu for configuring the upgrade system, Root of Upgrade Menu
 *
 */
public class UpgradeMenu : Control {
    
    //Constants
    //Size of tileMap
    public static readonly int IEM_SIZE = 80;

    //Node vars
    public UpgradeMenuTiles upgradeMenuTiles;
    public GridContainer grid;
    
    //State vars
    Matrix<Upgrade> record;


    public override void _Ready(){
        upgradeMenuTiles = GetNode<UpgradeMenuTiles>("UpgradeMenuTiles");
        grid = GetNode<GridContainer>("RightPanel/ScrollPanel/Scroll/List/Grid");

        record = new Matrix<Upgrade>(upgradeMenuTiles.tileMap.GetUsedRect());

        //FIXME: temp, init pre-existing UpgradeMenuObj, on the final products all instances of this scene are created dinamycally
        GetNode<UpgradeMenuObj>("RightPanel/ScrollPanel/Scroll/List/Grid/UpgradeMenuObj").Init(this);
        GetNode<UpgradeMenuObj>("RightPanel/ScrollPanel/Scroll/List/Grid/UpgradeMenuObj2").Init(this);
        GetNode<UpgradeMenuObj>("RightPanel/ScrollPanel/Scroll/List/Grid/UpgradeMenuObj3").Init(this);
    }

}
