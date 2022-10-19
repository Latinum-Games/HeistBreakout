using System;
using UnityEngine;
using UnityEngine.UI;

public class FieldItem : MonoBehaviour, IInteractable { // TODO RENAME FUNCTION TO FIELD_ITEM or smt
    
    
    // Private Variables
    [Header("Item")]
    [SerializeField] private Item item;
    [SerializeField] private Sprite sprite;
    public string InteractionPrompt => item.interactionPrompt == string.Empty ? "Pickup " + item.title  : item.interactionPrompt;

    private void Start() {
        sprite = item.GetSprite();
        Vector3 boxCollider2D = gameObject.GetComponent<BoxCollider2D>().size;
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;

        // Vector2 S = gameObject.GetComponent<BoxCollider2D>().size;
        // var spriteBounds = gameObject.GetComponent<SpriteRenderer>().sprite.bounds;
        // spriteBounds.size = S;
        // gameObject.GetComponent<BoxCollider2D>().offset = new Vector2 ((S.x / 2), (S.x / 2));
        // gameObject.GetComponentInChildren<SpriteRenderer>().sprite = sprite;
    }

    public bool Interact(Interactor interactor) {
        return Loot(interactor);
    }
    
    private bool Loot(Interactor interactor) {
        var inventory = interactor.GetComponent<Inventory>();

        if (inventory == null) {
            return false;
        }

        if (!inventory.AddLoot(item: item)) {
            return false;
        }
        
        inventory.RefreshInventory();

        Destroy(this.gameObject);
        return true;
    }
}
