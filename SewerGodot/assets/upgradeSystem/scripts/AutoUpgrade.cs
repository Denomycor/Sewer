
/* Autonomous upgrade
 *
 */
public abstract class AutoUpgrade : Upgrade {

    //Constructor
    public AutoUpgrade(string name, string texture, Type type, Rarity rarity, int valueInt)
        :base(name, texture, type, rarity, valueInt)
    {
    }

    
    //Override to create/remove Scene
    protected abstract void CreateScene(object player);
    protected abstract void DeleteScene(object player);


    //Overriding this must call base.Initiate()
    public override void Initiate(object player){
        CreateScene(player);
    }

    //Overriding this must call base.Remove()
    public override void Remove(object player){
        DeleteScene(player);
    }

}
