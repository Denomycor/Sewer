

public class TestSpread : StatUpgrade<float>{
    public TestSpread()
        :base("Test Multishot", "res://icon.png", Upgrade.Type.TEST, Upgrade.Rarity.TEST, 1, 3)
    {
    }

    public override float Fold(Stat<float> acc){
        return acc.value / value;
    }

    protected override void BindToStat(Player player){
        player.gun.spreadUpgrades.AddLast(this);
    }

    protected override void UnbindFromStat(Player player){
        player.gun.spreadUpgrades.Remove(this);
    }
}