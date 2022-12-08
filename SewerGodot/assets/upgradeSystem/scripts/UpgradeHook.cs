
/* Upgrade that subscribes to an event
 * since the number of arguments depends on the event, derived classes must implement their own
 */
public abstract class UpgradeHook : Upgrade {

    //State vars
    public bool isBinded {get; private set;}


    //Constructor
    public UpgradeHook(string name, string texture, Type type, Rarity rarity, int valueInt)
        :base(name, texture, type, rarity, valueInt)
    {
    }

    //Override to ad/remove subscription to event
    protected abstract void AddAction();
    protected abstract void RemoveAction();


    public void Bind(){
        isBinded = true;
        AddAction();
    }

    public void Unbind(){
        isBinded = false;
        RemoveAction();
    }

}
