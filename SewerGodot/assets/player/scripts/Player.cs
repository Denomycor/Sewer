using Godot;

/* handles the player movement
 *
 */
public class Player : KinematicBody2D {

    //Player Stats
    public Stat<float> moveSpeed;
    public StateStat<int> health;

    public Gun gun;
    public Timer fireDelayTimer; 

    //State vars
    private Vector2 moveDirection;


    public override void _Ready(){
        fireDelayTimer = GetNode<Timer>("FireDelayTimer");
    }

    //Constructor
    public Player(){
        moveSpeed = new Stat<float>(500f, (Stat<float> _) => {});
        health = new StateStat<int>(100, (Stat<int> _) => {}, 100);

        gun = new Gun(1f, 1, this, null);
    }


    //sets the movement direction
    public override void _Process(float delta){
        moveDirection = GetMovementDirtection();
    }

    //moves the player node to the intended direnction at player movement speed
    public override void _PhysicsProcess(float delta){
        MoveAndSlide(moveDirection * moveSpeed.value, Vector2.Up);
    }
    
    //returns movement direction
    private Vector2 GetMovementDirtection(){
        int x=0, y=0;
        if(Input.IsActionPressed("MoveUp")){
            y = -1;
        }
        if(Input.IsActionPressed("MoveDown")){
            y += 1;
        }
        if(Input.IsActionPressed("MoveRight")){
            x = 1;
        }
        if(Input.IsActionPressed("MoveLeft")){
            x -= 1;
        }
        return new Vector2(x,y).Normalized();
    }

    //Get cursor direction relativo to player
    public Vector2 GetRelativeCursorDirection(){
        Vector2 dif = GetLocalMousePosition() - Position;
        return dif.Normalized();
    }

    //SINGAL CALLBACK
    public void OnFireDelayTimerTimeout(){
        gun.readyToShoot = true;
    }

}
