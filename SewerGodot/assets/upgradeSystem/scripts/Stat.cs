using System.Collections.Generic;

/* Class that represents a Stat of an objects, accepts upgrades and automatically manages changes
 *
 */
public class Stat<T> {

    public T value {get;private set;}
    protected T defaultValue;
    protected LinkedList<StatUpgrade<T>> upgradeList;

    //Delegates
    //Transform and do bound checking
    public delegate void TransformFunction(Stat<T> s);
    protected TransformFunction Transform;


    //Constructor
    public Stat(T defaultValue, TransformFunction Transform){
        this.defaultValue = defaultValue;
        this.value = defaultValue;

        upgradeList = new LinkedList<StatUpgrade<T>>();

        this.Transform = Transform;
    }

    //Constructor shared
    public Stat(T defaultValue, TransformFunction Transform, LinkedList<StatUpgrade<T>> upgrades){
        this.defaultValue = defaultValue;
        this.value = defaultValue;

        upgradeList = upgrades;

        this.Transform = Transform;
    }


    //Adds upgrade to this stat
    public void AddUpgrade(StatUpgrade<T> upgrade){
        upgradeList.AddLast(upgrade);
    }

    //Clear all upgrades
    public void ClearUpgrades(){
        upgradeList.Clear();
    }


    //Calculates value of this stat
    public void Calculate(){
        value = defaultValue;
        foreach(StatUpgrade<T> upgrade in upgradeList){
            value = upgrade.Fold(this);
        }
        Transform(this);
    }


    //Gets value of this stat
    public virtual T Get(){
        return value;
    }


    //Dangerous, sets value manually
    public void OverrideValue(T value){
        this.value = value;
    }
}
