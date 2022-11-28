using Godot;

/* Upgrade Object on the Upgrade Menu
 *
 */
public class UpgradeMenuObj : Control {

    //Node vars
    public UpgradeMenu upgradeMenu {get;set;}
    public UpgradeMenuTiles upgradeMenuTiles {get;set;}

    //Stat vars
    public UpgradeInfo info {get;set;}
    public bool isStatic {get;set;} = false;

    //State vars
    public bool inUse {get;set;} = false;



    public void Init(UpgradeMenu upgradeMenu, UpgradeMenuTiles upgradeMenuTiles){
        this.upgradeMenu = upgradeMenu;
        this.upgradeMenuTiles = upgradeMenuTiles;
    }



    public override object GetDragData(Vector2 _){
        GD.Print("test");
        return this;
    }

}
