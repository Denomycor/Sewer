using System.Collections.Generic;
using System;
using Godot;

/* Handles shooting mechanics, Selects projectiles and receives upgrades to stats and events
 *
 */
public class Gun{

    //Gun stats
    public Stat<float> spread;
    public Stat<float> damage;
    public Stat<float> speed;
    public Stat<float> range;
    public Stat<int> multishot;
    public Stat<float> firerate;
    public Stat<float> size;
    public Stat<ProjectileEntity.PathFunction> Path;

    public Player player;

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
    public Gun(float spread, float damage, float speed, float range, int multishot, float firerate, float size, Player player){
        this.spread = new Stat<float>(spread, (Stat<float> x)=>{});
        this.damage = new Stat<float>(damage, (Stat<float> x)=>{});
        this.speed = new Stat<float>(speed, (Stat<float> x)=>{});
        this.range = new Stat<float>(range, (Stat<float> x)=>{});
        this.multishot = new Stat<int>(multishot, (Stat<int> x)=>{});
        this.firerate = new Stat<float>(firerate, (Stat<float> x)=>{});
        this.size = new Stat<float>(size, (Stat<float> x)=>{});
        this.Path = new Stat<ProjectileEntity.PathFunction>(ProjectileEntity.LinearMovement, (Stat<ProjectileEntity.PathFunction> x)=>{});
        this.projectiles = new List<Projectile>();
        
        this.player = player;
    }


    //Fire a shot
    public void Shoot(){
        for(int i=0; i<multishot.value; i++){
            Projectile proj = SelectProjectile();
            proj.Shoot();
        }
    }

    //Selects a projectile from the list
    public Projectile SelectProjectile(){
        if(projectiles.Count > 0){
            return projectiles[rd.Next(projectiles.Count)];
        }else{
            return defaultProjectile;
        }
    }
}