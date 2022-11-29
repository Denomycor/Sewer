using Godot;
using System;

public class CheckForZIndex : Area2D
{

    //connect signals to self
    public override void _Ready(){
        this.Connect("area_shape_entered", this, nameof(_on_Area_area_shape_entered));
        this.Connect("area_shape_exited", this, nameof(_on_Area_area_shape_exited));
    }

    //if collided adjust z index accordingly
    public void _on_Area_area_shape_entered(RID rid, Area2D other, int index, int local_index){
        if(((Node2D)other.Owner).Position.y > ((Node2D)this.Owner).Position.y){
            ((Node2D)this.Owner).ZIndex -=1;
        }else{
            ((Node2D)this.Owner).ZIndex +=1;
        }
    }

    //when exiting collision resent zindex
    public void _on_Area_area_shape_exited(RID rid, Area2D other, int index, int local_index){
            ((Node2D)this.Owner).ZIndex = 0;
    }

}
