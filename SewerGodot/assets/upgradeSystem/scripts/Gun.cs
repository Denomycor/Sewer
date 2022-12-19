using System.Collections.Generic;
using System;
using Godot;

/* Handles shooting mechanics, Selects projectiles and receives upgrades to stats and events
 *
 */
public class Gun{

    //Gun upgrades
    public LinkedList<StatUpgrade<float>> spreadUpgrades;
    public LinkedList<StatUpgrade<float>> damageUpgrades;
    public LinkedList<StatUpgrade<float>> speedUpgrades;
    public LinkedList<StatUpgrade<float>> rangeUpgrades;
    public LinkedList<StatUpgrade<int>> multishotUpgrades;
    public LinkedList<StatUpgrade<float>> sizeUpgrades;
    public LinkedList<StatUpgrade<ProjectileEntity.PathFunction>> PathUpgrades;
    
    //Gun stats
    public Stat<float> fireDelay;
    public Stat<int> gunMultishot;

    //Player who holds this gun
    public Player player;

    //Projectiles
    public List<Projectile> projectiles;

    //Events
    public Action<ProjectileEntity> OnShoot;
    public Action<ProjectileEntity, KinematicCollision2D> OnEntityCollision;
    public Action<ProjectileEntity> OnProcess;
    public Action<ProjectileEntity> OnFade;
    
    //Timers
    public bool readyToShoot = true;

    //Random
    private Random rd = new Random();


///Initializations

    //Constructor
    public Gun(float firerate, int gunMultishot, Player player){
        this.spreadUpgrades = new LinkedList<StatUpgrade<float>>();
        this.damageUpgrades = new LinkedList<StatUpgrade<float>>();
        this.speedUpgrades = new LinkedList<StatUpgrade<float>>();
        this.rangeUpgrades = new LinkedList<StatUpgrade<float>>();
        this.multishotUpgrades = new LinkedList<StatUpgrade<int>>();
        this.sizeUpgrades = new LinkedList<StatUpgrade<float>>();
        this.PathUpgrades = new LinkedList<StatUpgrade<ProjectileEntity.PathFunction>>();

        this.fireDelay = new Stat<float>(firerate, (Stat<float> x)=>{});
        this.gunMultishot = new Stat<int>(gunMultishot, (Stat<int> x)=>{});
        
        this.player = player;
        this.projectiles = new List<Projectile>();
        
        OnShoot += (ProjectileEntity pe)=>{};
        OnEntityCollision += (ProjectileEntity pe, KinematicCollision2D kc) => {};
        OnProcess += (ProjectileEntity pe)=>{};
        OnFade += (ProjectileEntity pe)=>{};
    }


///Logic

    //Fire a shot
    public void Shoot(){
        if(readyToShoot){            
            for(int i=0; i<gunMultishot.value; i++){
                Projectile proj = SelectProjectile();
                if(proj!=null){
                    proj.Shoot();
                }
            }
            readyToShoot = false;
            player.fireDelayTimer.Start();
        }
    }


    //Selects a projectile from the list
    public Projectile SelectProjectile(){
        if(projectiles.Count > 0){
            return SelectLinear();
        }else{
            return null;
        }
    }


    //Adds projectile to this gun
    public void AddProjectile(Projectile p){
        projectiles.Add(p);
    }

    //Removes projectile from this gun
    public void RemoveProjectile(Projectile p){
        projectiles.Remove(p);
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

    //Calculates fireDelay and updates timer
    public void CalculateFireDelay(){
        fireDelay.Calculate();
        player.fireDelayTimer.WaitTime = fireDelay.value;
    }

    public void CalculateSpread(){
        foreach(Projectile p in projectiles){
            p.spread.Calculate();
        }
    }

    public void CalculateDamage(){
        foreach(Projectile p in projectiles){
            p.damage.Calculate();
        }
    }

    public void CalculateSpeed(){
        foreach(Projectile p in projectiles){
            p.speed.Calculate();
        }
    }

    public void CalculateRange(){
        foreach(Projectile p in projectiles){
            p.range.Calculate();
        }
    }

    public void CalculateMultishot(){
        foreach(Projectile p in projectiles){
            p.multishot.Calculate();
        }
    }

    public void CalculateSize(){
        foreach(Projectile p in projectiles){
            p.size.Calculate();
        }
    }

    public void CalculatePath(){
        foreach(Projectile p in projectiles){
            p.Path.Calculate();
        }
    }

    public void CalculateAllStats(){
        CalculateFireDelay();
        CalculateSpread();
        CalculateDamage();
        CalculateSpeed();
        CalculateRange();
        CalculateMultishot();
        CalculateSize();
        CalculatePath();
        gunMultishot.Calculate();
    }
}
