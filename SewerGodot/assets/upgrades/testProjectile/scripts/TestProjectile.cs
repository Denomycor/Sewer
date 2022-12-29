using Godot;
using System.Collections.Generic;

//Note how should we handle projectileTemplate var, it should be static, but we dont it always instanciated

public class TestProjectile : Projectile {
    
    public TestProjectile(Gun gun)
        :base("Test Projectile", "res://red.png", Upgrade.Type.TEST, Upgrade.Rarity.TEST, gun)
    {
    }

    protected override void InitStatsAndPool() {
        spread = new Stat<float>(Mathf.Pi/4, Stat<float>.NullFunc, gun.spreadUpgrades);
        damage = new Stat<float>(1, Stat<float>.NullFunc, gun.damageUpgrades);
        speed = new Stat<float>(300, Stat<float>.NullFunc, gun.speedUpgrades);
        range = new Stat<float>(300, Stat<float>.NullFunc, gun.rangeUpgrades);
        multishot = new Stat<int>(1, Stat<int>.NullFunc, gun.multishotUpgrades);
        size = new Stat<float>(1, Stat<float>.NullFunc, gun.sizeUpgrades);

        Path = new Stat<ProjectileEntity.PathFunction>(
            ProjectileEntity.LinearPath,
            Stat<ProjectileEntity.PathFunction>.NullFunc,
            gun.PathUpgrades
        );
        this.poolCapacity = 5;
    }

    public override string GetDescription(){
        return "Test projectile";
    }

    public override int GetValue(){
        return -1;
    }

    public override void InitConnections(){
        this.connectionsMap = new Dictionary<Godot.Vector2, int>();
        connectionsMap.Add(Vector2.Up, 1);
        connectionsMap.Add(Vector2.Down, 1);
        connectionsMap.Add(Vector2.Left, -1);
        connectionsMap.Add(Vector2.Right, -1);
    }

    protected override void PrepareScene(Player player){
        projectileTemplate = GD.Load<PackedScene>("res://assets/upgrades/testProjectile/scenes/TestProjectile.tscn");
    }

    protected override void RemoveScene(Player player){
        projectileTemplate = null;
    }
}
