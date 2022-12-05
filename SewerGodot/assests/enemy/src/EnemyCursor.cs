using Godot;

/* Script for setting enemy cursor to the intended position and height
 *
 */
public class EnemyCursor : Polygon2D {
    
    //enemy reference
    private Enemy _parent;

    //player reference
    private Player _player;

    private Polygon2D _body;

    //initializing these references
    public override void _Ready() {
        _parent = GetParent<Enemy>();
        _player = GetParent().GetParent().GetNode<Player>("Player");
        _body = GetBody(_parent);
    }

    //sets cursor rotation to the angle pointing to the player
    public override void _Process(float delta) {
        if(_player!=null){
            Rotation = GetAngleFromPlayer();
        }
    }

    //returns angle of mouse in relation to center of the player
    private float GetAngleFromPlayer(){
        Vector2 playerPosition = _player.Position;
        float angle = 0;

        playerPosition = _parent.ToLocal(playerPosition);

        if(playerPosition.Length()!=0)
            angle = Mathf.Acos(playerPosition.x/playerPosition.Length());
        //sets order in hierarchy
        if(playerPosition.y<0){
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
    private Polygon2D GetBody(Enemy player)
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
