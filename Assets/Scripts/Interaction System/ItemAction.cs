using UnityEngine;

public class ItemAction : MonoBehaviour, IInteractable { // TODO RENAME FUNCTION TO FIELD_ITEM or smt
    // Private Variables
    [Header("Item")]
    [SerializeField] private Item item;
    public string InteractionPrompt => item.interactionPrompt == string.Empty ? "Pickup " + item.title  : item.interactionPrompt;
    
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

        Destroy(this.gameObject);
        return true;
    }
}
