using Godot;

public abstract class ProjectileEntity : KinematicBody2D {

    //TODO: Remove test values

    //Instance stats
    //projectile dont care about spread, firerate or multishot, they only need information concerning their own instance
    public float damage;
    public float speed = 300f;
    public float range = 600f;
    public float size = 1;
    public Vector2 direction = Vector2.Up;
    public PathFunction Path = LinearPath;
    
    //delegates
    public delegate Vector2 PathFunction(Vector2 direction, float delta);

    public Projectile parent;

    //State Stats
    public float currentRange;

    //Init Projectile
    public virtual void Init(Vector2 startPos, Vector2 direction, float damage, float speed, float range, float size, PathFunction Path, Projectile parent){
        Reset(startPos, direction, damage, speed, range, size, Path);
        this.parent = parent;
    }

    //Reset Projectile
    public virtual void Reset(Vector2 startPos, Vector2 direction, float damage, float speed, float range, float size, PathFunction Path){
        this.Position = startPos;
        this.direction = direction;
        this.damage = damage;
        this.speed = speed;
        this.range = range;
        this.size = size;
        this.Scale = Vector2.One * size;
        this.Path = Path;
        this.Visible = true;
        currentRange = 0;
    }


    //Fire this projectile
    public virtual void Shoot(){
        parent.gun.OnShoot(this);
        SetProcess(true);
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


    //Remove this projectile and add to object pool
    public virtual void Fade(){
        parent.gun.OnFade(this);
        SetProcess(false);
        Visible = false;
        parent.SaveProjectile(this);
    }


    //Destroys this projectile
    public virtual void Destroy(){
        QueueFree();
    }


    //Moves the projectile
    public virtual void Move(float delta){
        Vector2 path = Path(direction, delta);
        this.Rotation = path.Angle();
        KinematicCollision2D collision = MoveAndCollide(path*speed*delta);
        OnCollision(collision);
    }


    //Collide with entity
    public virtual void OnCollision(KinematicCollision2D collision){
        parent.gun.OnEntityCollision(this, collision);
    }


    //Standard linear movement
    public static Vector2 LinearPath(Vector2 direction, float delta){
        return direction;
    }
    
}
