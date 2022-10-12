using System;

[Serializable]
public class Item {
    public enum ItemType {
        KeyItem,
        Treasure
    }

    // Attributes
    public string title;
    public ItemType type;
    public int weight;

    // Interaction Values
    public string interactionPrompt;
}
