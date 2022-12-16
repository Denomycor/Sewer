using Godot;
using System.Collections.Generic;
using System;

/* Handles shooting of a specific type of projectile, calculates final values after upgrades
 * and prepares the projectile
 */
public abstract class Projectile : AutoUpgrade {

    //ProjectileEntity
    public PackedScene projectileTemplate;

    //Projectile stats
    public float spread;
    public float damage;
    public float speed;
    public float range;
    public int multishot;
    public float size;
    public float firerate; //Each projectile maybe doesnt care about fire rate
    public ProjectileEntity.PathFunction Path;

    public Gun gun;
    public Queue<ProjectileEntity> objectPool;

    private Random rd = new Random();

    //Constructor
    protected Projectile(string name, string texture, Type type, Rarity rarity, int valueInt, Gun gun)
        :base(name, texture, type, rarity, valueInt)
    {
        this.gun = gun;
        this.objectPool = new Queue<ProjectileEntity>();
    }

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
            proj.Reset(GetStartPos(), GetDirection(), GetDamage(), GetSpeed(), GetRange(), GetSize(), GetPath());
            return proj;
        }else{
            ProjectileEntity proj = projectileTemplate.Instance<ProjectileEntity>();
            proj.Init(GetStartPos(), GetDirection(), GetDamage(), GetSpeed(), GetRange(), GetSize(), GetPath(), this);
            return proj;
        }
    }

    //Saves projectile
    public void SaveProjectile(ProjectileEntity proj){
        if(objectPool.Count < GetPoolCapacity()){
            objectPool.Enqueue(proj);
        }else{
            proj.Destroy();
        }
    }


///ABSTRACT

    //#Note: its more expensive to keep track of up to date values than calculating them everytime


    protected virtual float GetSpread(){
        return (float)(rd.NextDouble()*spread*2.0)-spread;
    }

    protected virtual Vector2 GetDirection(){
        return gun.player.GetRelativeCursorDirection().Rotated(GetSpread());
    }

    protected virtual Vector2 GetStartPos(){
        return gun.player.Position;
    }

    protected abstract ProjectileEntity.PathFunction GetPath();

    //Override to calculate final value of multishot
    protected abstract float GetMultishot();

    //Override to calculate final value of damage
    protected abstract float GetDamage();

    //Override to calculate final value of speed
    protected abstract float GetSpeed();

    //Override to calculate final value of range
    protected abstract float GetRange();

    //Override to calculate final value of size
    protected abstract float GetSize();

    //Override to define the capacity of the object pool
    protected abstract int GetPoolCapacity();

}
