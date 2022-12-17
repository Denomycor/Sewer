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
    public UpgradeMenuContext upgradeMenuContext;
    public GridContainer grid;
    
    //State vars
    public bool isPaused = false;
    public Matrix<UpgradeMenuObj> record;
    public LinkedList<UpgradeMenuObj> allUpgrades = new LinkedList<UpgradeMenuObj>();

    //player
    public Player player;

    public override void _Ready(){
        upgradeMenuTiles = GetNode<UpgradeMenuTiles>("UpgradeMenuTiles");
        upgradeMenuContext = GetNode<UpgradeMenuContext>("UpgradeMenuContext");
        grid = GetNode<GridContainer>("RightPanel/ScrollPanel/Scroll/List/Grid");

        record = new Matrix<UpgradeMenuObj>(upgradeMenuTiles.tileMap.GetUsedRect());

        //FIXME: temp, init pre-existing UpgradeMenuObj, on the final products all instances of this scene are created dinamycally
        GetNode<UpgradeMenuObj>("RightPanel/ScrollPanel/Scroll/List/Grid/UpgradeMenuObj").Init(this);
        GetNode<UpgradeMenuObj>("RightPanel/ScrollPanel/Scroll/List/Grid/UpgradeMenuObj2").Init(this);
        GetNode<UpgradeMenuObj>("RightPanel/ScrollPanel/Scroll/List/Grid/UpgradeMenuObj3").Init(this);

    }


    //Adds a new UpgradeMenuObj from an Upgrade to the UpgradeMenu
    public void AddUpgrade(Upgrade upgrade){
        //TODO: also dont forget to add to allUpgrades
    }


    //Toggles this menu on screen
    public void MenuToggle(){
        GetTree().Paused = isPaused;
        GetParent<CanvasLayer>().Visible = isPaused;
        upgradeMenuContext.SetProcessInput(isPaused);
        if(isPaused){
            MenuEnter();
            isPaused = false;
        }else{
            MenuLeave();
            isPaused = true;
        }
    }


    //Called on entering the Menu
    public void MenuEnter(){
        //TODO: Recover state
    }

    //Called on leaving the Menu
    public void MenuLeave(){
        //TODO: Calculate New state of upgarde system and dispatch them
    }



    //Check tileState and set active upgrades accordingly
    public LinkedList<Upgrade> CalculateActiveUpgradesAndInstall(UpgradeMenuObj root){
        LinkedList<Upgrade> activeUpgrades = new LinkedList<Upgrade>();
        foreach(UpgradeMenuObj o in allUpgrades){
            o.visited = false;
        }
        Queue<UpgradeMenuObj> queue = new Queue<UpgradeMenuObj>();
        queue.Enqueue(root);

        //iterate through upgrades
        while(queue.Count > 0){
            HandleUpgrade(queue, activeUpgrades);
        }

        //Remove for unused upgrades
        foreach(UpgradeMenuObj o in allUpgrades){
            if(!o.visited){
                if(o.initialized){
                    o.initialized = false;
                    o.upgradeRef.Remove(player);
                }
            }
        }
        
        return activeUpgrades;
    }

    public void HandleUpgrade(Queue<UpgradeMenuObj> queue, LinkedList<Upgrade> activeUpgrades){
        //Get upgrade and initiate
        UpgradeMenuObj current = queue.Dequeue();
        activeUpgrades.AddLast(current.upgradeRef);
        current.visited = true;

        if(!current.initialized){
            current.initialized = true;
            current.upgradeRef.Initiate(player);
        }
        
        //check neighboors
        foreach(Vector2 d in current.ApplicableDirections(current.GetTilePos())){
            UpgradeMenuObj next = record.GetRelative(current.GetTilePos(), d);
            if(next != null){
                int o = next.upgradeRef.connectionsMap[d*-1];
                int t = current.upgradeRef.connectionsMap[d];
                if(UpgradeMenuObj.AreConnected(o,t) && o!=0){
                    if(!next.visited){
                        queue.Enqueue(next);
                    }
                }
            }
        }
    }



///DEBUG

    public override void _Input(InputEvent e){
        if(e.IsActionPressed("ui_cancel")){
            MenuToggle();
        }
    }

    //FIXME: DEBUG only
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
