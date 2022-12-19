using Godot;

/* Gamemaster class
 *
 */
public class Game : Node {

    //Node vars
    public UpgradeMenu upgradeMenu;
    public Player player;


///Initializations

    public override void _Ready() {
        //Toggle off upgradeMenu on start
        upgradeMenu = GetNode<UpgradeMenu>("UpgradeMenu/Main");
        player = GetNode<Player>("Room/YSort/Player");
        upgradeMenu.MenuToggle();
        upgradeMenu.player = player;
    }

}
