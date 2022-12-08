using Godot;
using System;
using System.Collections.Generic;

/* Base class for all upgades
 * not abstract so UpgradeMenu can be tested
 */
public /*FIXME: TEMP abstract*/ class Upgrade {
    
    //Stat vars
    public string name {get; private set;}

    public string textureString {get; private set;}
    public Texture texture {get; private set;}

    public Type type {get; private set;}
    public Rarity rarity {get; private set;}
    public int valueInt {get; private set;}

    //State vars //TODO: figure how to initialize state vars
    public Dictionary<Vector2, int> connectionsMap {get; private set;}
    public UpgradeMenuObj upgradeMenuObj {get; private set;}

    
    //Constructor
    public Upgrade(string name, string texture, Type type, Rarity rarity, int valueInt){
        this.name = name;
        this.textureString = texture;
        this.texture = GD.Load<Texture>(texture);
        this.type = type;
        this.rarity = rarity;
        this.valueInt = valueInt;
    }


///Enums

    public enum Type {
        PLAYERSTAT,
    }

    public enum Rarity {
        COMMON,
    }


///Functions

    public virtual string GetDescription(){
        //TODO: abstract
        throw new NotImplementedException();
    }

}
