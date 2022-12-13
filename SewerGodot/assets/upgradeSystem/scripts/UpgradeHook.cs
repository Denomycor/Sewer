
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
    

    //Override to add/remove subscription to event
    protected abstract void AddAction(object player);
    protected abstract void RemoveAction(object player);


    //Overriding this must call base.Initiate()
    public override void Initiate(object player){
        isBinded = true;
        AddAction(player);
    }

    //Overriding this must call base.Remove()
    public override void Remove(object player){
        isBinded = false;
        RemoveAction(player);
    }

}
