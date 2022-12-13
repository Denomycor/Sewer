/* Upgrade to a stat of type T
 *
 */
public abstract class StatUpgrade<T> : Upgrade {

    //Stat vars
    public T value {get; private set;}

    //Constructor
    public StatUpgrade(string name, string texture, Type type, Rarity rarity, int valueInt, T value)
        :base(name, texture, type, rarity, valueInt)
    {
        this.value = value;
    }

    //Fold this stat
    public abstract T Fold(Stat<T> acc);

    
    //Override to add/remove Stat
    protected abstract void BindToStat(object player);
    protected abstract void UnbindFromStat(object player);


    //Overriding this must call base.Initiate()
    public override void Initiate(object player){
        BindToStat(player);
    }

    //Overriding this must call base.Remove()
    public override void Remove(object player){
        UnbindFromStat(player);
    }
    
}
