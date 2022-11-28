using Godot;

/* Menu for configuring the upgrade system
 *
 */
public class UpgradeMenu : Control {
    
    //Constants
    public static readonly int STD_CELL_SIZE = 64;

    //Node vars
    public UpgradeMenuTiles upgradeMenuTiles {get;set;}
    public GridContainer grid {get;set;}
    


    public override void _Ready(){
        upgradeMenuTiles = GetNode<UpgradeMenuTiles>("UpgradeMenuTiles");
        grid = GetNode<GridContainer>("RightPanel/ScrollPanel/Scroll/List/Grid");

        SetChildParents();

        //Temp init pre-existing UpgradeMenuObj
        GetNode<UpgradeMenuObj>("RightPanel/ScrollPanel/Scroll/List/Grid/UpgradeMenuObj").Init(this, upgradeMenuTiles);
    }


    //Give child nodes with scripts a reference to UpgradeMenu
    private void SetChildParents(){
        upgradeMenuTiles.upgradeMenu = this;
    }

}
