using Godot;

/* Checks is another entity has entered the players space to ajust his Z index according to the heights of the entety and player
 *
 */
public class PlayerZIndex : Area2D {

    private bool _intersecting = false;
    Area2D _other;

    //connect signals to self
    public override void _Ready(){
        this.Connect("area_shape_entered", this, nameof(_on_Area_area_shape_entered));
        this.Connect("area_shape_exited", this, nameof(_on_Area_area_shape_exited));
    }

    public override void _Process(float delta){
        if(_intersecting && _other!= null){
            CheckZIndex(_other);
        }else{
            GetParent<Player>().ZIndex = 0;
        }
    }

    //set intersecting bool to true and set _other to the other entity aread 2d
    public void _on_Area_area_shape_entered(RID rid, Area2D other, int index, int local_index){
        _intersecting = true;
        _other = other;
    }

    //set intersecting bool to false
    public void _on_Area_area_shape_exited(RID rid, Area2D other, int index, int local_index){
        _intersecting = false;
    }

    //if collided adjust z index accordingly
    private void CheckZIndex(Area2D other){
        if(other.GetParent<Node2D>().Position.y > GetParent<Player>().Position.y){
            GetParent<Player>().ZIndex = -2;
        }else{
            GetParent<Player>().ZIndex = 2;
        }
    }
}
