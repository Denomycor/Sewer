using Godot;
using System.Text;
using System.Collections.Generic;

/* Menu for configuring the upgrade system, Root of Upgrade Menu
 *
 */
public class UpgradeMenu : Control {
    
    //Constants
    public static Vector2 PAN_LIMITS = new Vector2(1682, 1082);

    //Node vars
    public UpgradeMenuTiles upgradeMenuTiles;
    public GridContainer grid;
    
    //State vars
    public Matrix<UpgradeMenuObj> record;
    public LinkedList<UpgradeMenuObj> allUpgrades = new LinkedList<UpgradeMenuObj>();


    public override void _Ready(){
        upgradeMenuTiles = GetNode<UpgradeMenuTiles>("UpgradeMenuTiles");
        grid = GetNode<GridContainer>("RightPanel/ScrollPanel/Scroll/List/Grid");

        record = new Matrix<UpgradeMenuObj>(upgradeMenuTiles.tileMap.GetUsedRect());

        //FIXME: temp, init pre-existing UpgradeMenuObj, on the final products all instances of this scene are created dinamycally
        GetNode<UpgradeMenuObj>("RightPanel/ScrollPanel/Scroll/List/Grid/UpgradeMenuObj").Init(this);
        GetNode<UpgradeMenuObj>("RightPanel/ScrollPanel/Scroll/List/Grid/UpgradeMenuObj2").Init(this);
        GetNode<UpgradeMenuObj>("RightPanel/ScrollPanel/Scroll/List/Grid/UpgradeMenuObj3").Init(this);
    }


    
    //Returns a list with all active upgrades
    public LinkedList<Upgrade> Fold(UpgradeMenuObj root){
        foreach(UpgradeMenuObj o in allUpgrades){
            o.visited = false;
        }
        LinkedList<Upgrade> activeUpgrades = new LinkedList<Upgrade>();
        FoldImp(root, activeUpgrades);
        return activeUpgrades;
    }

    private void FoldImp(UpgradeMenuObj obj, LinkedList<Upgrade> activeUpgrades){
        if(obj.visited){
            return;
        }
        obj.visited = true;
        activeUpgrades.AddLast(obj.upgradeRef);

        foreach(Vector2 d in obj.ApplicableDirections(obj.GetTilePos())){
            UpgradeMenuObj next = record.GetRelative(obj.GetTilePos(), d);
            if(next != null){
                int o = next.upgradeRef.connectionsMap[d*-1];
                int t = obj.upgradeRef.connectionsMap[d];
                if(UpgradeMenuObj.AreConnected(o,t)){
                    FoldImp(next, activeUpgrades);
                }
            }
        }
    }


    //Adds a new UpgradeMenuObj from an Upgrade to the UpgradeMenu
    public void AddUpgrade(Upgrade upgrade){
        //TODO: dont forget to add to allUpgrades
    }


///DEBUG

    public static void DebugMatrix(Matrix<UpgradeMenuObj> record){
        StringBuilder s = new StringBuilder();
        for(int i=record.GetStartY(); i<record.GetEndY(); i++){
            s.Append('\n');
            for(int j=record.GetStartX(); j<record.GetEndX(); j++){
                if(record[j, i] == null){
                    s.Append(" 0");
                }else{
                    s.Append(" 1");
                }
            }
        } s.Append("\n---------------------------------------------------------------");
        GD.Print(s.ToString());
    }

}
