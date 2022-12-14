using Godot;

/* Gamemaster class
 *
 */
public class Game : Node {

    //Node vars
    public UpgradeMenu upgradeMenu;



    public override void _Ready() {
        //Toggle off upgradeMenu on start
        upgradeMenu = GetNode<UpgradeMenu>("UpgradeMenu/Main");
        upgradeMenu.MenuToggle();
    }

}
