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

    //Constructor
    protected Projectile(string name, string texture, Type type, Rarity rarity, int valueInt, Gun gun)
        :base(name, texture, type, rarity, valueInt)
    {
        this.gun = gun;
        this.objectPool = new Queue<ProjectileEntity>();
        InitStats();
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
            proj.Reset(GetStartPos(), GetDirectionWithSpread(), GetDamage(), GetSpeed(), GetRange(), GetSize(), GetPath());
            return proj;
        }else{
            ProjectileEntity proj = projectileTemplate.Instance<ProjectileEntity>();
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
        return GetDirection().Rotated((float)(rd.NextDouble()*spread*2)-spread);
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
        return gun.player.Position;
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
    
    //Init stats with upgrade lists from parent gun and init poolCapacity vard
    protected abstract void InitStats();

}
