
/* Class that represents a Stat of an objects, accepts upgrades and automatically manages changes
 *
 */
public class StateStat<T> : Stat<T>{

    protected T currentValue;

    //Delegates
    //Do bounds check on current value
    public delegate void AssertFunction(StateStat<T> a);
    protected AssertFunction Assert;

    //Constructor
    public StateStat(T defaultMaxValue, TransformFunction Transform, StatKey key, T startingValue)
        :base(defaultMaxValue, Transform, key)
    {
        this.currentValue = startingValue;

    }

    //Calculates Max value of this stat
    public override void Calculate(){
        base.Calculate();
        Assert(this);
    }


    //Gets current value of this stat
    public override T Get(){
       return currentValue;
    }

    //Sets current value of this stat, should be clamped after for safety
    public void Set(T value){
        currentValue = value;
        Assert(this);
    }

    //Gets max value of this stat
    public T GetMax(){
        return value;
    }

}
