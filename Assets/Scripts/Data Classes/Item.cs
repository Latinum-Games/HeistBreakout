using System;
using Photon.Pun;
using UnityEngine;

[Serializable]
public class Item {
    
    // Main Item Classification
    public enum ItemType {
        KeyItem,
        Treasure
    }

    // Specific Item Classification
    public enum ItemEntity {
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
    public ItemEntity entity = ItemEntity.Duck;
    public int weight = 1;
    public int amount = 1;

    // Interaction Values
    public string interactionPrompt = "DEFAULT ITEM PROMPT";
    
    // Sprite Resources
    public Sprite GetSprite() {
        switch (entity) {
            default:
            // Treasure Sprites
            case ItemEntity.Ring: return ItemAssets.Instance.ring;
            case ItemEntity.Harp: return ItemAssets.Instance.harp;
            case ItemEntity.Staff: return ItemAssets.Instance.staff;
            case ItemEntity.Necklace: return ItemAssets.Instance.necklace;
            case ItemEntity.Goblet: return ItemAssets.Instance.goblet;
            case ItemEntity.Crown: return ItemAssets.Instance.crown;
            case ItemEntity.Diamond: return ItemAssets.Instance.diamond;
            case ItemEntity.Emerald: return ItemAssets.Instance.emerald;
            case ItemEntity.Egg: return ItemAssets.Instance.egg;
            case ItemEntity.Jar: return ItemAssets.Instance.jar;
            case ItemEntity.FaceMask: return ItemAssets.Instance.faceMask;
            case ItemEntity.Mask: return ItemAssets.Instance.mask;
            case ItemEntity.Globe: return ItemAssets.Instance.globe;
            case ItemEntity.Duck: return ItemAssets.Instance.duck;
            case ItemEntity.Clock: return ItemAssets.Instance.clock;
            case ItemEntity.Ruby: return ItemAssets.Instance.ruby;
            case ItemEntity.Sapphire: return ItemAssets.Instance.sapphire;
        }
    }
    
    // Is Stackable
    public bool IsStackable() {
        return type == ItemType.Treasure;
    }
    
    
}
