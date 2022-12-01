using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FieldItem : MonoBehaviour, IInteractable { // TODO RENAME FUNCTION TO FIELD_ITEM or smt
    
    // Private Variables
    [Header("Item")]
    [SerializeField] private Item item;
    [SerializeField] private Sprite sprite;
    
    //Unity Events
    [Header("Unity Events")] 
    public UnityEvent pickItem;
    
    public string InteractionPrompt => item.interactionPrompt == string.Empty ? "Pickup " + item.title  : item.interactionPrompt;

    private void Start() {
        //Initializer for item sprites and box colliders
        sprite = item.GetSprite();
        Vector3 boxCollider2D = gameObject.GetComponent<BoxCollider2D>().size;
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    //Interaction with items in map
    public bool Interact(Interactor interactor) {
        return Loot(interactor);
    }
    
    //Adding item to loot depending in the item presence
    private bool Loot(Interactor interactor) {
        var inventory = interactor.GetComponent<Inventory>();

        if (inventory == null) {
            return false;
        }

        if (!inventory.AddLoot(item: item)) {
            return false;
        }
        
        inventory.RefreshInventory();

        pickItem.Invoke();
        Destroy(this.gameObject);
        return true;
    }
}
