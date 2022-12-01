using Godot;

/* Base class for all upgades
 *
 */
public abstract class Upgrade {
    
    //Stat vars
    public readonly string name;
    public readonly string textureString;
    public readonly Texture texture;
    public readonly Rarity rarity;

    public enum Rarity {
        COMMON,
    }

    public abstract string GetDescription();
}
