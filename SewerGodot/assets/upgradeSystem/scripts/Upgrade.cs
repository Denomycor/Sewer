using Godot;
using System;
using System.Collections.Generic;

/* Base class for all upgades
 * not abstract so UpgradeMenu can be tested
 */
public /*FIXME: TEMP abstract*/ class Upgrade {
    
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

    //FIXME: TEMP
    public Upgrade(){}
    
    //Constructor
    public Upgrade(string name, string texture, Type type, Rarity rarity){
        this.name = name;
        this.spriteString = texture;
        this.sprite = GD.Load<Texture>(texture);
        this.type = type;
        this.rarity = rarity;
        this.isBundled = false;
        //FIXME: erase comment InitConnections();
    }
    
    //Instance UpgradeMenuObj and add it to upgradeMenu
    public void CreateUpgradeMenuObj(UpgradeMenu upgradeMenu){
        if(!isBundled){
            upgradeMenuObj = MENU_OBJ_TEMPLATE.Instance<UpgradeMenuObj>();
            upgradeMenuObj.Init(upgradeMenu);
            upgradeMenu.grid.AddChild(upgradeMenuObj);
        }
    }

///Enums

    public enum Type {
        PLAYERSTAT, PROJECTILE, TEST,
    }

    public enum Rarity {
        COMMON, TEST,
    }


///Abstracts

    //gets this upgrade description
    public virtual string GetDescription(){
        //TODO: abstract
        throw new NotImplementedException();
    }

    //Prepare upgrade, initialize scenes etc
    public virtual void Install(Player player){
        //TODO: abstract
        throw new NotImplementedException();
    }

    //Frees resources
    public virtual void Remove(Player player){
        //TODO: abstract
        throw new NotImplementedException();
    }

    //Gets an integer representing how good an upgrade is
    public virtual int GetValue(){
        //TODO: abstract
        throw new NotImplementedException();
    }

    //Initializes connections map
    public virtual int InitConnections(){
        //TODO: abstract
        throw new NotImplementedException();
    }

}
