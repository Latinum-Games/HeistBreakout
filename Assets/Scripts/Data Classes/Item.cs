using System;
using UnityEngine;

[Serializable]
public class Item {
    
    // Main Item Classification
    public enum ItemType {
        KeyItem,
        Treasure
    }

    // Specific Item Classification
    public enum ItemSprite {
        Ring,
        Harp,
        Staff,
        Necklace,
        Goblet,
        Crown,
        Diamond,
        Emerald,
        Egg,
        Jar,
        FaceMask,
        Mask,
        Globe,
        Duck,
        Clock,
        Ruby,
        Sapphire
    }

    // Attributes
    public string title = "DEFAULT ITEM";
    public ItemType type = ItemType.Treasure;
    public ItemSprite itemSprite = ItemSprite.Duck;
    public int weight = 0;

    // Interaction Values
    public string interactionPrompt = "DEFAULT ITEM PROMPT";
    
    // Sprite Resources
    public Sprite GetSprite() {
        switch (itemSprite) {
            default:
            // Treasure Sprites
            case ItemSprite.Ring: return ItemAssets.Instance.ring;
            case ItemSprite.Harp: return ItemAssets.Instance.harp;
            case ItemSprite.Staff: return ItemAssets.Instance.staff;
            case ItemSprite.Necklace: return ItemAssets.Instance.necklace;
            case ItemSprite.Goblet: return ItemAssets.Instance.goblet;
            case ItemSprite.Crown: return ItemAssets.Instance.crown;
            case ItemSprite.Diamond: return ItemAssets.Instance.diamond;
            case ItemSprite.Emerald: return ItemAssets.Instance.emerald;
            case ItemSprite.Egg: return ItemAssets.Instance.egg;
            case ItemSprite.Jar: return ItemAssets.Instance.jar;
            case ItemSprite.FaceMask: return ItemAssets.Instance.faceMask;
            case ItemSprite.Mask: return ItemAssets.Instance.mask;
            case ItemSprite.Globe: return ItemAssets.Instance.globe;
            case ItemSprite.Duck: return ItemAssets.Instance.duck;
            case ItemSprite.Clock: return ItemAssets.Instance.clock;
            case ItemSprite.Ruby: return ItemAssets.Instance.ruby;
            case ItemSprite.Sapphire: return ItemAssets.Instance.sapphire;
        }
    }
}
