using Godot;

/* Script for setting enemy cursor to the intended position and height
 *
 */
public class EnemyCursor : Polygon2D {
    
    //enemy reference
    private Enemy _parent;

    //player reference
    private Player _player;

    //initializing these references
    public override void _Ready() {
        _parent = GetParent<Enemy>();
        _player = GetParent().GetParent().GetNode<Player>("Player");
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
        //sets zindex of cursor depending on if it is above or below player
        if(playerPosition.y<0){
            angle *= -1;
            ZIndex = -1;
        }else{
            ZIndex = 1;
        }

        return angle;
    }
}
