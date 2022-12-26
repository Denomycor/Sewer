
/* Autonomous upgrade
 *
 */
public abstract class AutoUpgrade : Upgrade {

///Initializations

    //Constructor
    public AutoUpgrade(string name, string texture, Type type, Rarity rarity)
        :base(name, texture, type, rarity)
    {
    }


    //Overriding this must call base.Install()
    public override void Install(Player player){
        PrepareScene(player);
    }

    //Overriding this must call base.Remove()
    public override void Remove(Player player){
        RemoveScene(player);
    }


///Abstracts

    //Override to create/remove Scene
    protected abstract void PrepareScene(Player player);
    protected abstract void RemoveScene(Player player);

}
