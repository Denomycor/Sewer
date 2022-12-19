using Godot;

/* Popup description of an upgrade
 *
 */
public class UpgradeMenuContext : Control {

    //State vars
    UpgradeMenuObj upgradeObj;


///Logic

    //Reloads this into the screen
    public void Reload(UpgradeMenuObj obj){
        RectPosition = GetDisplayPosition(obj);
        this.Visible = true;
    }


    //Decide where to place the UpgradeMenuContext
    private Vector2 GetDisplayPosition(UpgradeMenuObj obj){
        int margin = 20;
        Vector2 global = obj.RectGlobalPosition;
        MarginContainer rightPanel = obj.upgradeMenu.GetNode<MarginContainer>("RightPanel");
        Vector2 center = (obj.upgradeMenu.RectSize-rightPanel.RectMinSize*Vector2.Right)/2;

        Vector2 pos;
        if(obj.inUse){
            pos = global + new Vector2(obj.RectSize.x+margin, -RectSize.y/2+obj.RectSize.y/2);
            if(global.x > center.x){
                pos += new Vector2(-RectSize.x-obj.RectSize.x-margin*2,0);
            }
        }else{
            pos = new Vector2(obj.upgradeMenu.RectSize.x-rightPanel.RectMinSize.x-RectSize.x-margin, global.y);
        }

        pos.y = Mathf.Clamp(pos.y, 0+margin, obj.upgradeMenu.RectSize.y-RectSize.y-margin);   
        return pos;
    }


    //Disappear from screen
    public void HideWindow(){
        this.Visible = false;
    }


    public override void _Input(InputEvent e) {
        if(e is InputEventMouseButton){
            InputEventMouseButton emb = e as InputEventMouseButton;
            if(emb.Pressed && !GetRect().HasPoint(emb.Position)){
                HideWindow();
            }
        }
    }

}
