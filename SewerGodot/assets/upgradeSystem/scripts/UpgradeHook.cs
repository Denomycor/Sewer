
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
    protected abstract void AddAction(object player);
    protected abstract void RemoveAction(object player);


    public void Bind(object player){
        isBinded = true;
        AddAction(player);
    }

    public void Unbind(object player){
        isBinded = false;
        RemoveAction(player);
    }

}
