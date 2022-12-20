using Godot;

public class TestProjectile : Projectile {
    
    public TestProjectile(Gun gun)
        :base("Test Projectile", "res://icon.png", Upgrade.Type.TEST, Upgrade.Rarity.TEST, 1, gun)
    {
    }

    
    protected override void PrepareScene(Player player){
        projectileTemplate = GD.Load<PackedScene>("res://assets/upgrades/testProjectile/scenes/TestProjectile.tscn");
    }

    protected override void RemoveScene(Player player){
        ClearPool();
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

}