
public class TestMultishot : StatUpgrade<int>{
   
    public TestMultishot()
        :base("Test Multishot", "res://icon.png", Upgrade.Type.TEST, Upgrade.Rarity.TEST, 1)
    {
    }

    public override int Fold(Stat<int> acc){
        return acc.value + value;
    }

    protected override void BindToStat(Player player){
        player.gun.multishotUpgrades.AddLast(this);
    }

    protected override void UnbindFromStat(Player player){
        player.gun.multishotUpgrades.Remove(this);
    }
}