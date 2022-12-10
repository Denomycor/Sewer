
/* Autonomous upgrade
 *
 */
public abstract class AutoUpgrade : Upgrade {

    //Constructor
    public AutoUpgrade(string name, string texture, Type type, Rarity rarity, int valueInt)
        :base(name, texture, type, rarity, valueInt)
    {
    }

    //To be called every frame by player
    public abstract void Process(object player);

}
