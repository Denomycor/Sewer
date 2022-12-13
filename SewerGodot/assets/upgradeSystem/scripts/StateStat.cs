/* Class that represents a Stat of an objects, accepts upgrades and automatically manages changes
 *
 */
public class StateStat<T> : Stat<T>{

    public T current {get; set;}

    //Constructor
    public StateStat(T defaultMaxValue, TransformFunction Transform, T startingValue)
        :base(defaultMaxValue, Transform)
    {
        this.current = startingValue;
    }


    //Gets current value of this stat
    public override T Get(){
       return current;
    }

    //Sets current value of this stat
    public void Set(T value){
        current = value;
    }

    //Gets max value of this stat
    public T GetMax(){
        return value;
    }

}
