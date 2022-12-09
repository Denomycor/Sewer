
/* Class that represents a Stat of an objects, accepts upgrades and automatically manages changes
 *
 */
public class StateStat<T> : Stat<T>{

    protected T currentValue;

    //Constructor
    public StateStat(T defaultMaxValue, TransformFunction Transform, StatKey key, T startingValue)
        :base(defaultMaxValue, Transform, key)
    {
        this.currentValue = startingValue;

    }


    //Gets current value of this stat
    public override T Get(){
       return currentValue;
    }

    //Sets current value of this stat
    public void Set(T value){
        currentValue = value;
    }

    //Gets max value of this stat
    public T GetMax(){
        return value;
    }

}
