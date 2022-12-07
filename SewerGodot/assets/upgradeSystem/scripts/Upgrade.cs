using Godot;
using System.Collections.Generic;

/* Base class for all upgades
 *
 */
public /*FIXME: TEMP abstract*/ class Upgrade {
    
    //Stat vars
    public readonly string name;
    public readonly string textureString;
    public readonly Texture texture;
    public readonly Rarity rarity;
    public Dictionary<Vector2, int> connectionsMap;


    public enum Rarity {
        COMMON,
    }

    /*FIXME: TEMP public abstract string GetDescription();*/
}
