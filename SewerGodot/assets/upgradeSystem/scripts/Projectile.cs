using Godot;
using System.Collections.Generic;
using System;

/* Handles shooting of a specific type of projectile, calculates final values after upgrades
 * and prepares the projectile
 */
public abstract class Projectile : AutoUpgrade {

    //ProjectileEntity template
    public PackedScene projectileTemplate;

    //Projectile stats
    public Stat<float> spread;
    public Stat<float> damage;
    public Stat<float> speed;
    public Stat<float> range;
    public Stat<int> multishot;
    public Stat<float> size;
    public Stat<ProjectileEntity.PathFunction> Path;

    //Reference to gun
    public Gun gun;

    //Object Pool
    public Queue<ProjectileEntity> objectPool;
    public int poolCapacity;

    //Random
    private Random rd = new Random();


///Initializations

    //Constructor
    protected Projectile(string name, string texture, Type type, Rarity rarity, Gun gun)
        :base(name, texture, type, rarity)
    {
        this.gun = gun;
        this.objectPool = new Queue<ProjectileEntity>();
        InitStatsAndPool();
    }

    //Install upgrade
    public override void Install(Player player){
        base.Install(player);
        AssignToGun(player);
    }

    //Remove upgrade
    public override void Remove(Player player){
        base.Remove(player);
        RemoveFromGun(player);
    }

    //Assign this projectile to gun
    protected void AssignToGun(Player player){
        player.gun.AddProjectile(this);
    }

    //Remove this projectile from gun
    protected void RemoveFromGun(Player player){
        player.gun.RemoveProjectile(this);
    }


///Logic

    //Fire this projectile
    public virtual void Shoot(){
        for(int i=0; i<GetMultishot(); i++){
            ProjectileEntity proj = GetProjectile();
            proj.Shoot();
        }
    }
    

    //Gets a projectile and prepares it to shoot
    private ProjectileEntity GetProjectile(){
        if(objectPool.Count > 0){
            ProjectileEntity proj = objectPool.Dequeue();
            proj.Reset(GetStartPos(), GetDirectionWithSpread(), GetDamage(), GetSpeed(), GetRange(), GetSize(), GetPath());
            return proj;
        }else{
            ProjectileEntity proj = projectileTemplate.Instance<ProjectileEntity>();
            gun.player.GetParent().GetParent().AddChild(proj);
            proj.Init(GetStartPos(), GetDirectionWithSpread(), GetDamage(), GetSpeed(), GetRange(), GetSize(), GetPath(), this);
            return proj;
        }
    }


    //Saves projectile in object pool
    public void SaveProjectile(ProjectileEntity proj){
        if(objectPool.Count < poolCapacity){
            objectPool.Enqueue(proj);
        }else{
            proj.Destroy();
        }
    }


    //Applies spread to a direction
    public virtual Vector2 GetDirectionWithSpread(){
        float spread = GetSpread();
        double angle = (rd.NextDouble()*spread*2)-spread;
        return GetDirection().Rotated((float)angle);
    }


    //Destroy all objects in the pool
    public void ClearPool(){
        foreach (ProjectileEntity p in objectPool){
            p.Destroy();
        }
    }


///Getters for stats

    //Override to calculate final value of spread (this stats + gun default)
    protected virtual float GetSpread(){
        return spread.value;
    }

    //Override to calculate final value of direction (this stats + gun default)
    protected virtual Vector2 GetDirection(){
        return gun.player.GetRelativeCursorDirection();
    }

    //Override to calculate final value of postion (this stats + gun default)
    protected virtual Vector2 GetStartPos(){
        return gun.player.GlobalPosition;
    }

    //Override to calculate final value of PathFunction (this stats + gun default)
    protected virtual ProjectileEntity.PathFunction GetPath(){
        return Path.value;
    }

    //Override to calculate final value of multishot (this stats + gun default)
    protected virtual float GetMultishot(){
        return multishot.value;
    }

    //Override to calculate final value of damage (this stats + gun default)
    protected virtual float GetDamage(){
        return damage.value;
    }

    //Override to calculate final value of speed (this stats + gun default)
    protected virtual float GetSpeed(){
        return speed.value;
    }

    //Override to calculate final value of range
    protected virtual float GetRange(){
        return range.value;
    }

    //Override to calculate final value of size
    protected virtual float GetSize(){
        return size.value;
    }


///Abstracts
    
    //Init stats with upgrade lists from parent gun and init poolCapacity var
    protected abstract void InitStatsAndPool();

}
