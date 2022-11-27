using Godot;

/* Menu for configuring the upgrade system
 *
 */
public class UpgradeMenu : Control {
    
    //Node vars
    public UpgradeMenuTiles upgradeMenuTiles {get;set;}
    public GridContainer grid {get;set;}
    
    public override void _Ready(){
        upgradeMenuTiles = GetNode<UpgradeMenuTiles>("UpgradeMenuTiles");
        grid = GetNode<GridContainer>("RightPanel/ScrollPanel/Scroll/List/Grid");

        SetChildParents();

        //Temp
        GetNode<UpgradeMenuObj>("RightPanel/ScrollPanel/Scroll/List/Grid/UpgradeMenuObj").Init(this, upgradeMenuTiles);
    }

    private void SetChildParents(){
        upgradeMenuTiles.upgradeMenu = this;
    }

}
