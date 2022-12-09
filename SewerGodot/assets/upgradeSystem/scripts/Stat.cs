using System.Collections.Generic;

/* Class that represents a Stat of an objects, accepts upgrades and automatically manages changes
 *
 */
public class Stat<T> {

    //TODO: Inacessibale members on Transform?, Should Transform be called after Set on StateStat?

    public T value {get; private set;}
    protected T defaultValue;
    protected LinkedList<StatUpgrade<T>> upgradeList;
    public StatKey key {get; private set;}

    //Delegates
    //Transform and do bound checking
    public delegate void TransformFunction(Stat<T> s);
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
    public void Calculate(){
        value = defaultValue;
        foreach(StatUpgrade<T> upgrade in upgradeList){
            value = upgrade.Fold(value, defaultValue);
        }
        Transform(this);
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
