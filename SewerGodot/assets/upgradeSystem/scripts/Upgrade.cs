using Godot;
using System;
using System.Collections.Generic;

/* Base class for all upgades
 * not abstract so UpgradeMenu can be tested
 */
public /*FIXME: TEMP abstract*/ class Upgrade {
    
    //Stat vars
    public string name {get; private set;}

    public string spriteString {get; private set;}
    public Texture sprite {get; private set;}

    public Type type {get; private set;}
    public Rarity rarity {get; private set;}
    protected int valueInt {get; private set;}

    //State vars //TODO: figure how to initialize state vars
    public Dictionary<Vector2, int> connectionsMap;
    public UpgradeMenuObj upgradeMenuObj;


    //FIXME: TEMP
    public Upgrade(){}
    
    //Constructor
    public Upgrade(string name, string texture, Type type, Rarity rarity, int defaultValueInt){
        this.name = name;
        this.spriteString = texture;
        this.sprite = GD.Load<Texture>(texture);
        this.type = type;
        this.rarity = rarity;
        this.valueInt = defaultValueInt;
    }


///Enums

    public enum Type {
        PLAYERSTAT, PROJECTILE,
    }

    public enum Rarity {
        COMMON,
    }


///Functions

    //gets this upgrade description
    public virtual string GetDescription(){
        //TODO: abstract
        throw new NotImplementedException();
    }

    //Prepare upgrade, initialize scenes etc
    public virtual void Initiate(object player){
        //TODO: abstract
        throw new NotImplementedException();
    }

    //Frees resources
    public virtual void Remove(object player){
        //TODO: abstract
        throw new NotImplementedException();
    }

    //Gets an integer representing how good an upgrade is
    public virtual int GetValue(object player){
        //TODO: abstract
        throw new NotImplementedException();
    }

}
