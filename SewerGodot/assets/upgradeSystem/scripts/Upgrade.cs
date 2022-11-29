using Godot;

/* Base class for all upgades
 *
 */
public abstract class Upgrade {
    
    public string name {get;set;}
    public string textureString {get;set;}
    public Texture texture {get;set;}
    public Rarity rarity {get;set;}

    public enum Rarity {
        COMMON,
    }

    public abstract string GetDescription();
}
