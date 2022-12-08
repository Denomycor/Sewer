using System.Collections.Generic;

/* Class that represents a Stat of an objects, accepts upgrades and automatically manages changes
 *
 */
public class Stat<T> {

    //TODO: Identifier, to match StatUpgrades to correct stats
    //TODO: StateStat

    protected T value;
    protected T defaultValue;
    protected LinkedList<StatUpgrade<T>> upgradeList;

    //Delegates
    //Takes final value of fold and transforms it
    public delegate T TransformFunction(T a);
    protected TransformFunction Transform;


    //Constructor
    public Stat(T defaultValue, T value, TransformFunction Transform){
        this.defaultValue = defaultValue;
        this.value = value;

        upgradeList = new LinkedList<StatUpgrade<T>>();

        this.Transform = Transform;
    }


    //Adds upgrade to this stat
    public void AddUpgrade(StatUpgrade<T> upgrade){
        upgradeList.AddLast(upgrade);
        Calculate();
    }


    //TODO: way to remove or alter list


    //Calculates value of this stat
    public void Calculate(){
        value = defaultValue;
        foreach(StatUpgrade<T> upgrade in upgradeList){
            value = upgrade.Fold(value);
        }
        value = Transform(value);
    }


    //Gets value of this stat
    public T Get(){
        return value;
    }

    public static implicit operator T(Stat<T> s) => s.Get();

}