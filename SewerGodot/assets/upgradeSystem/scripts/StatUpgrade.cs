/* Upgrade to a stat of type T
 *
 */
public abstract class StatUpgrade<T> : Upgrade {

    //Stat vars
    public T value {get; private set;}
    public Stat<T>.StatKey key {get; private set;}

    //Delegates
    //Takes stat accumulator and folds it with value
    public delegate T FoldFunction(T acc, T startingValue);
    public FoldFunction Fold {get; private set;}


    //Constructor
    protected StatUpgrade(string name, string texture, Type type, Rarity rarity, int valueInt, T value, FoldFunction Fold, Stat<T>.StatKey key)
        :base(name, texture, type, rarity, valueInt)
    {
        this.value = value;
        this.Fold = Fold;
        this.key = key;
    }
    
}
