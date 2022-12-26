using System.Collections.Generic;

/* Upgrade composed of multiple upgrades
 *
 */
public abstract class BundledUpgrade : Upgrade {

    public LinkedList<Upgrade> upgradeList {get;private set;}

    //Whether this bundles' upgrades are just a composition of effects{true}, or a colletion of multiple actual upgrades{false}
    //if true properties of upgrades in this list don't matter, as this is a single upgarde with just multiple effects
    public bool staticBundle {get;private set;}

///Initializations

    //Constructor
    public BundledUpgrade(string name, string texture, Type type, Rarity rarity, int defaultValueInt, bool staticBundle=false)
        :base(name, texture, type, rarity, defaultValueInt)
    {
        this.upgradeList = new LinkedList<Upgrade>();
        this.staticBundle = staticBundle;
    }

    //Constructor
    public BundledUpgrade(string name, string texture, Type type, Rarity rarity, int defaultValueInt, LinkedList<Upgrade> upgradeList, bool staticBundle=false)
        :base(name, texture, type, rarity, defaultValueInt)
    {
        this.upgradeList = upgradeList;
        this.staticBundle = staticBundle;
    }

    //Install all upgrades
    public override void Install(Player player){
        foreach(Upgrade u in upgradeList){
            u.Install(player);
        }
    }

    //Remove all upgrades
    public override void Remove(Player player){
        foreach(Upgrade u in upgradeList){
            u.Remove(player);
        }
    }


///Logic

    //Add Upgrade to bundle
    public void AddUpgrade(Upgrade upgrade){
        upgradeList.AddLast(upgrade);
    }

    //Remove Upgrade from bundle
    public void RemoveUpgrade(Upgrade upgrade){
        upgradeList.Remove(upgrade);
    }

}
