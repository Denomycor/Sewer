using Godot;
using System.Collections.Generic;

/* handles the player movement
 *
 */
public class Player : KinematicBody2D {

    //Player Stats
    public Stat<float> moveSpeed;
    public StateStat<int> health;

    //Shooting
    public Gun gun;
    public Timer fireDelayTimer; 

    //State vars
    private Vector2 moveDirection;

    //Active upgrades
    public LinkedList<Upgrade> activeUpgrades;


///Initialization

    public override void _Ready(){
        fireDelayTimer = GetNode<Timer>("FireDelayTimer");

        gun = new Gun(0.2f, 1, this);
    }

    //Constructor
    public Player(){
        moveSpeed = new Stat<float>(500f, (Stat<float> _) => {});
        health = new StateStat<int>(100, (Stat<int> _) => {}, 100);
    }

    //FIXME: Remove this projectile, for test purposes only
    public void CreateUpgrades(UpgradeMenu um){
        Root r = new Root();
        r.CreateStaticUpgradeMenuObj(um, new Vector2(12, 6));
        um.root = r.upgradeMenuObj;

        TestProjectile p = new TestProjectile(gun);
        //p.Install(this);
        p.CreateUpgradeMenuObj(um);

        TestMultishot m = new TestMultishot();
        //m.Install(this);
        m.CreateUpgradeMenuObj(um);

        TestSpread sp = new TestSpread();
        //sp.Install(this);
        sp.CreateUpgradeMenuObj(um);

        TestSpeed s = new TestSpeed();
        //s.Install(this);
        s.CreateUpgradeMenuObj(um);

        /*TestMultishot m2 = new TestMultishot();
        m2.Install(this);
        
        TestOnShoot os = new TestOnShoot();
        os.Install(this);*/

        //gun.CalculateAllStats();
    }

///Logic

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
        Vector2 dif = GetGlobalMousePosition() - GlobalPosition;
        return dif.Normalized();
    }


    //SINGAL CALLBACK
    public void OnFireDelayTimerTimeout(){
        gun.readyToShoot = true;
    }


    //Calculate all stats of player
    public void CalculateAllStats(){
        moveSpeed.Calculate();
        health.Calculate();
    }


    public override void _Input(InputEvent e){
        if(e.IsActionPressed("ui_select")){
            gun.Shoot();
        }
    }
}
