using UnityEngine;

public class ItemAction : MonoBehaviour, IInteractable {
    
    // Private Variables
    [Header("Item Values")]
    [SerializeField] private int itemWeight;
    
    [Header("Player Interaction")]
    [SerializeField] private string actionPrompt;
    public string InteractionPrompt => actionPrompt;
    
    public bool Interact(Interactor interactor) {
        return Loot(interactor);
    }
    
    private bool Loot(Interactor interactor) {
        var inventory = interactor.GetComponent<Inventory>();

        if (inventory == null) {
            return false;
        }

        if (!inventory.AddLoot(itemWeight: itemWeight)) {
            return false;
        }

        Destroy(this.gameObject);
        return true;
    }
}
