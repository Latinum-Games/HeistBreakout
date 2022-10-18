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
        Duck
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
                case ItemSprite.Duck: return ItemAssets.Instance.duck;
        }
    }
}
