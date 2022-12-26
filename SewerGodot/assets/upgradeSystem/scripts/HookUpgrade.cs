
/* Upgrade that subscribes to an event
 * since the number of arguments depends on the event, derived classes must implement their own
 */
public abstract class HookUpgrade : Upgrade {

    //State vars
    public bool isBinded {get; private set;}


///Initializations

    //Constructor
    public HookUpgrade(string name, string texture, Type type, Rarity rarity)
        :base(name, texture, type, rarity)
    {
    }


    //Overriding this must call base.Install()
    public override void Install(Player player){
        isBinded = true;
        AddAction(player);
    }

    //Overriding this must call base.Remove()
    public override void Remove(Player player){
        isBinded = false;
        RemoveAction(player);
    }


///Abstracts

    //Override to add/remove subscription to event
    protected abstract void AddAction(Player player);
    protected abstract void RemoveAction(Player player);

}
