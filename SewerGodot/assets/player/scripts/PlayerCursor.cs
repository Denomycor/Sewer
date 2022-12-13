using Godot;

/* Script for setting player cursor to the intended position and height
 *
 */
public class PlayerCursor : Polygon2D {
    
    //reference to player
    private Player _parent;
    private Polygon2D _body;

    //initialize reference
    public override void _Ready(){
        _parent = GetParent<Player>();
        _body = GetBody(_parent);
    }

    //sets cursor rotation to the angle pointing to the mouse
    public override void _Process(float delta){
        Rotation = GetAngleFromMouse();
    }

    //returns angle of mouse in relation to center of the player
    private float GetAngleFromMouse(){
        Vector2 mousePosition = _parent.GetLocalMousePosition();
        float angle = 0;

        if(mousePosition.Length()!=0)
            angle = Mathf.Acos(mousePosition.x/mousePosition.Length());
        //sets order in hierarchy
        if(mousePosition.y<0){
            angle *= -1;
            PutCursorAboveBody();
        }else{
            PutCursorBellowBody();    
        }
        return angle;
    }

    //move the cursor bellow body in the hierarchy
    private void PutCursorBellowBody(){
        int bodyIndex = GetBodyIndex(_body);
        _parent.MoveChild(this, bodyIndex+1);
    }

    //move the cursor above body in the hierarchy
    private void PutCursorAboveBody(){
        int bodyIndex = GetBodyIndex(_body);
        _parent.MoveChild(this, bodyIndex-1);
    }


    //returns body node
    private Polygon2D GetBody(Player player)
    {   
        return (Polygon2D)player.FindNode("Body");
    }


    //returns body index in the player scene or -1 if error
    private int GetBodyIndex(Polygon2D body){
        if(body != null)
            return body.GetIndex();
        else
            return -1;
    }

}
