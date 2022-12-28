using Godot;
using System;
using System.Collections.Generic;

/* Base class for all upgades
 * not abstract so UpgradeMenu can be tested
 */
public abstract class Upgrade {
    
    public static PackedScene MENU_OBJ_TEMPLATE = GD.Load<PackedScene>("res://assets/ui/upgradeMenu/scenes/UpgradeMenuObj.tscn");

    //Stat vars
    public string name {get; private set;}

    public string spriteString {get; private set;}
    public Texture sprite {get; private set;}

    public Type type {get; private set;}
    public Rarity rarity {get; private set;}

    public bool isBundled {get; set;}

    public Dictionary<Vector2, int> connectionsMap;
    public UpgradeMenuObj upgradeMenuObj = null;


///Initializations
    
    //Constructor
    public Upgrade(string name, string texture, Type type, Rarity rarity){
        this.name = name;
        this.spriteString = texture;
        this.sprite = GD.Load<Texture>(texture);
        this.type = type;
        this.rarity = rarity;
        this.isBundled = false;
        InitConnections();
    }
    
    //Instance UpgradeMenuObj and add it to upgradeMenu
    public void CreateUpgradeMenuObj(UpgradeMenu upgradeMenu){
        if(!isBundled){
            upgradeMenuObj = MENU_OBJ_TEMPLATE.Instance<UpgradeMenuObj>();
            upgradeMenuObj.upgradeRef = this;
            upgradeMenuObj.Init(upgradeMenu);

            upgradeMenu.grid.AddChild(upgradeMenuObj);
            upgradeMenu.allUpgrades.AddLast(upgradeMenuObj);
        }
    }

    //Instance a static UpgradeMenuObj and add it to upgradeMenu
    public void CreateStaticUpgradeMenuObj(UpgradeMenu upgradeMenu, Vector2 pos){
        if(!isBundled){
            upgradeMenuObj = MENU_OBJ_TEMPLATE.Instance<UpgradeMenuObj>();
            upgradeMenuObj.upgradeRef = this;
            upgradeMenuObj.Init(upgradeMenu, true);
            
            upgradeMenuObj.inUse = true;
            upgradeMenuObj.recordRef.Set(pos, upgradeMenuObj);
            upgradeMenuObj.SetPosThroughTile(pos);
            upgradeMenuObj.upgradeMenuTiles.AddChild(upgradeMenuObj);

            upgradeMenu.allUpgrades.AddLast(upgradeMenuObj);
        }
    }


///Enums

    public enum Type {
        PLAYERSTAT, PROJECTILE, TEST, ROOT,
    }

    public enum Rarity {
        COMMON, TEST,
    }


///Abstracts

    //gets this upgrade description
    public abstract string GetDescription();

    //Prepare upgrade, initialize scenes etc
    public abstract void Install(Player player);

    //Frees resources
    public abstract void Remove(Player player);

    //Gets an integer representing how good an upgrade is
    public abstract int GetValue();

    //Initializes connections map
    public abstract void InitConnections();

}
