using Godot;

/* Upgrade Object on the Upgrade Menu
 *
 */
public class UpgradeMenuObj : Control {

    //Node vars
    public UpgradeMenu upgradeMenu {get;set;} //Unused

    //Stat vars
    public UpgradeInfo info {get;set;}
    public bool isStatic {get;set;} = false;

    //State vars
    public bool inUse {get;set;} = false;



    public void Init(UpgradeMenu upgradeMenu){
        this.upgradeMenu = upgradeMenu;
    }



    public override object GetDragData(Vector2 _){
        return this;
    }

}
