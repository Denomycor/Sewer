using System.Collections.Generic;

/* Upgrade composed of multiple upgrades which just act as effects
 * ! Upgrades from this bundle don't get an UpgradeMenuObj
 */
public abstract class BundledUpgrade : Upgrade {

    public LinkedList<Upgrade> upgradeList {get;private set;}


///Initializations

    //Constructor
    public BundledUpgrade(string name, string texture, Type type, Rarity rarity, bool staticBundle=false)
        :base(name, texture, type, rarity)
    {
        this.upgradeList = new LinkedList<Upgrade>();
        InitStartingUpgrades();
    }

    //Constructor
    public BundledUpgrade(string name, string texture, Type type, Rarity rarity, LinkedList<Upgrade> upgradeList, bool staticBundle=false)
        :base(name, texture, type, rarity)
    {
        this.upgradeList = upgradeList;
        InitStartingUpgrades();
    }

    //Install all upgrades
    public override void InstallImpl(Player player){
        foreach(Upgrade u in upgradeList){
            u.InstallImpl(player);
        }
    }

    //Remove all upgrades
    public override void RemoveImpl(Player player){
        foreach(Upgrade u in upgradeList){
            u.RemoveImpl(player);
        }
    }


///Abstracts

    //Create upgrades for this bundle, don't forget to set isBundled to true
    public abstract void InitStartingUpgrades();

}
