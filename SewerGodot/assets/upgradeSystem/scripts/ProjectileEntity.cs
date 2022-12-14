using Godot;

public abstract class ProjectileEntity : KinematicBody2D {

    //Instance stats
    public float damage;
    public float speed;
    public float range;
    public float size;
    public Vector2 direction;
    public PathFunction Path;
    
    //delegates
    public delegate Vector2 PathFunction(Vector2 direction, float delta);

    //Projectile upgrade
    public Projectile parent;

    //State Stats
    public float currentRange;


///Initializations

    //Init Projectile
    public virtual void Init(Vector2 startPos, Vector2 direction, float damage, float speed, float range, float size, PathFunction Path, Projectile parent){
        Reset(startPos, direction, damage, speed, range, size, Path);
        this.parent = parent;
    }

    //Reset Projectile
    public virtual void Reset(Vector2 startPos, Vector2 direction, float damage, float speed, float range, float size, PathFunction Path){
        this.GlobalPosition = startPos;
        this.direction = direction;
        this.damage = damage;
        this.speed = speed;
        this.range = range;
        this.size = size;
        this.Scale = Vector2.One * size;
        this.Path = Path;
        currentRange = 0;
    }


///Logic

    //Fire this projectile
    public virtual void Shoot(){
        SetPhysicsProcess(true);
        Visible = true;
        parent.gun.OnShoot(this);
    }

    //Process
    public override void _PhysicsProcess(float delta){
        parent.gun.OnProcess(this);
        currentRange+=speed*delta;
        Move(delta);
        
        if(currentRange>range){
            Fade();
        }
    }

    //Moves the projectile
    public virtual void Move(float delta){
        Vector2 path = Path(direction, delta);
        this.Rotation = path.Angle();
        KinematicCollision2D collision = MoveAndCollide(path*speed*delta);
        OnCollision(collision);
    }

    //Remove this projectile and add to object pool
    public virtual void Fade(){
        parent.gun.OnFade(this);
        Visible = false;
        SetPhysicsProcess(false);
        parent.SaveProjectile(this);
    }

    //Destroys this projectile
    public virtual void Destroy(){
        QueueFree();
    }

    //Collide with entity
    public virtual void OnCollision(KinematicCollision2D collision){
        parent.gun.OnEntityCollision(this, collision);
    }


///Statics

    //Standard linear movement
    public static Vector2 LinearPath(Vector2 direction, float delta){
        return direction;
    }
    
}
