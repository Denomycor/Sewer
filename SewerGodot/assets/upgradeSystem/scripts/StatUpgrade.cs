/* Upgrade to a stat of type T
 *
 */
public abstract class StatUpgrade<T> : Upgrade {

    //TODO: Identifier, to match StatUpgrades to correct stats

    //Stat vars
    public T value {get; private set;}
    
    //Delegates
    //Takes stat accumulator and folds it with value
    public delegate T FoldFunction(T acc);
    public FoldFunction Fold {get; private set;}

    protected StatUpgrade(string name, string texture, Type type, Rarity rarity, int valueInt, T value, FoldFunction Fold)
        :base(name, texture, type, rarity, valueInt)
    {
        this.value = value;
        this.Fold = Fold;
    }
    
}
