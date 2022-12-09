using System.Collections.Generic;

/* Class that represents a Stat of an objects, accepts upgrades and automatically manages changes
 *
 */
public class Stat<T> {

    //TODO: StateStat

    protected T value;
    protected T defaultValue;
    protected LinkedList<StatUpgrade<T>> upgradeList;
    public StatKey key {get; private set;}

    //Delegates
    //Takes final value of fold and transforms it
    public delegate T TransformFunction(T a);
    protected TransformFunction Transform;


    //Constructor
    public Stat(T defaultValue, TransformFunction Transform, StatKey key){
        this.defaultValue = defaultValue;
        this.value = defaultValue;
        this.key = key;

        upgradeList = new LinkedList<StatUpgrade<T>>();

        this.Transform = Transform;
    }


    //Adds upgrade to this stat
    public void AddUpgrade(StatUpgrade<T> upgrade){
        upgradeList.AddLast(upgrade);
        Calculate();
    }

    //Clear all upgrades
    public void ClearUpgrades(){
        upgradeList.Clear();
    }


    //Calculates value of this stat
    public virtual void Calculate(){
        value = defaultValue;
        foreach(StatUpgrade<T> upgrade in upgradeList){
            value = upgrade.Fold(value);
        }
        value = Transform(value);
    }


    //Gets value of this stat
    public virtual T Get(){
        return value;
    }


    //Enums
    public enum StatKey {
        PLAYERHP,
        PLAYERMS,
    }    

}
