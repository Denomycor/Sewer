using System.Collections.Generic;
using System;
using Godot;

/* Handles shooting mechanics, Selects projectiles and receives upgrades to stats and events
 *
 */
public class Gun{

    //Gun upgrades
    public LinkedList<float> spreadUpgrades;
    public LinkedList<float> damageUpgrades;
    public LinkedList<float> speedUpgrades;
    public LinkedList<float> rangeUpgrades;
    public LinkedList<int> multishotUpgrades;
    public LinkedList<float> sizeUpgrades;
    public LinkedList<ProjectileEntity.PathFunction> PathUpgrades;
    
    //Gun stats
    public Stat<float> firerate;
    public Stat<int> gunMultishot;

    //Player who holds this gun
    public Player player;

    //Projectiles
    public List<Projectile> projectiles;
    public Projectile defaultProjectile;

    //Events
    public Action<ProjectileEntity> OnShoot;
    public Action<ProjectileEntity, KinematicCollision2D> OnEntityCollision;
    public Action<ProjectileEntity> OnProcess;
    public Action<ProjectileEntity> OnFade;

    //Random
    private Random rd = new Random();

    //Constructor
    public Gun(float firerate, int gunMultishot, Player player, Projectile defaultProjectile){
        this.spreadUpgrades = new LinkedList<float>();
        this.damageUpgrades = new LinkedList<float>();
        this.speedUpgrades = new LinkedList<float>();
        this.rangeUpgrades = new LinkedList<float>();
        this.multishotUpgrades = new LinkedList<int>();
        this.sizeUpgrades = new LinkedList<float>();
        this.PathUpgrades = new LinkedList<ProjectileEntity.PathFunction>();

        this.firerate = new Stat<float>(firerate, (Stat<float> x)=>{});
        this.gunMultishot = new Stat<int>(gunMultishot, (Stat<int> x)=>{});
        
        this.player = player;
        this.projectiles = new List<Projectile>();
        this.defaultProjectile = defaultProjectile;
    }


    //Fire a shot
    public void Shoot(){
        for(int i=0; i<gunMultishot.value; i++){
            Projectile proj = SelectProjectile();
            proj.Shoot();
        }
    }


    //Selects a projectile from the list
    public Projectile SelectProjectile(){
        if(projectiles.Count > 0){
            return SelectLinear();
        }else{
            return defaultProjectile;
        }
    }


    //Choose how to pick a projectile from the list
    public Projectile SelectRandom(){
        return projectiles[rd.Next(projectiles.Count)];
    }
    int i=-1;
    public Projectile SelectLinear(){
        i = (i+1) % projectiles.Count;
        return projectiles[i];
    }


///Calculates

    public void CalculateSpread(){
        foreach(Projectile p in projectiles){
            p.spread.Calculate();
        }
        defaultProjectile.spread.Calculate();
    }

    public void CalculateDamage(){
        foreach(Projectile p in projectiles){
            p.damage.Calculate();
        }
        defaultProjectile.damage.Calculate();
    }

    public void CalculateSpeed(){
        foreach(Projectile p in projectiles){
            p.speed.Calculate();
        }
        defaultProjectile.speed.Calculate();
    }

    public void CalculateRange(){
        foreach(Projectile p in projectiles){
            p.range.Calculate();
        }
        defaultProjectile.range.Calculate();
    }

    public void CalculateMultishot(){
        foreach(Projectile p in projectiles){
            p.multishot.Calculate();
        }
        defaultProjectile.multishot.Calculate();
    }

    public void CalculateSize(){
        foreach(Projectile p in projectiles){
            p.size.Calculate();
        }
        defaultProjectile.size.Calculate();
    }

    public void CalculatePath(){
        foreach(Projectile p in projectiles){
            p.Path.Calculate();
        }
        defaultProjectile.Path.Calculate();
    }

}
