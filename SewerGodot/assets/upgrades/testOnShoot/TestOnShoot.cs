using Godot;

public class TestOnShoot : HookUpgrade {
    public TestOnShoot()
        :base("Test Multishot", "res://icon.png", Upgrade.Type.TEST, Upgrade.Rarity.TEST)
    {
    }

    protected override void AddAction(Player player) {
        player.gun.OnShoot += Print;
    }

    protected override void RemoveAction(Player player){
        player.gun.OnShoot -= Print;
    }

    public static void Print(ProjectileEntity p){
        GD.Print("Ola");
    }

}