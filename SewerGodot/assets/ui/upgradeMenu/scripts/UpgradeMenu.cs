using Godot;

/* Menu for configuring the upgrade system, Root of Upgrade Menu
 *
 */
public class UpgradeMenu : Control {
    
    //Constants
    public static readonly int IEM_SIZE = 64;

    //Node vars
    public UpgradeMenuTiles upgradeMenuTiles {get;set;} //Unused
    public GridContainer grid {get;set;}
    


    public override void _Ready(){
        upgradeMenuTiles = GetNode<UpgradeMenuTiles>("UpgradeMenuTiles");
        grid = GetNode<GridContainer>("RightPanel/ScrollPanel/Scroll/List/Grid");

        //FIXME: temp, init pre-existing UpgradeMenuObj
        GetNode<UpgradeMenuObj>("RightPanel/ScrollPanel/Scroll/List/Grid/UpgradeMenuObj").Init(this);
        GetNode<UpgradeMenuObj>("RightPanel/ScrollPanel/Scroll/List/Grid/UpgradeMenuObj2").Init(this);
    }

}
