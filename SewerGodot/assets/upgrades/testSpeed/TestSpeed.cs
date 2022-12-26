

public class TestSpeed : StatUpgrade<float>{
    
    public TestSpeed()
        :base("Test Multishot", "res://icon.png", Upgrade.Type.TEST, Upgrade.Rarity.TEST, 700f)
    {
    }

    public override float Fold(Stat<float> acc){
        return acc.value + value;
    }

    protected override void BindToStat(Player player){
        player.gun.speedUpgrades.AddLast(this);
    }

    protected override void UnbindFromStat(Player player){
        player.gun.speedUpgrades.Remove(this);
    }
}