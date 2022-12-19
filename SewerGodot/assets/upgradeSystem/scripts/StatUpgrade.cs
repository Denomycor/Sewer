/* Upgrade to a stat of type T
 *
 */
public abstract class StatUpgrade<T> : Upgrade {

    //Stat vars
    public T value {get; private set;}


///Initializations

    //Constructor
    public StatUpgrade(string name, string texture, Type type, Rarity rarity, int defaultValueInt, T value)
        :base(name, texture, type, rarity, defaultValueInt)
    {
        this.value = value;
    }


    //Overriding this must call base.Install()
    public override void Install(Player player){
        BindToStat(player);
    }

    //Overriding this must call base.Remove()
    public override void Remove(Player player){
        UnbindFromStat(player);
    }


///Abstracts
        
    //Override to add/remove Stat
    protected abstract void BindToStat(Player player);
    protected abstract void UnbindFromStat(Player player);
    
    //Fold this stat
    public abstract T Fold(Stat<T> acc);
}
