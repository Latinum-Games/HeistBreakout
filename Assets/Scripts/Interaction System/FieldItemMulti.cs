using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class FieldItemMulti : MonoBehaviourPunCallbacks, IInteractable { // TODO RENAME FUNCTION TO FIELD_ITEM or smt
    
    // Private Variables
    [Header("Item")]
    [SerializeField] private Item item;
    [SerializeField] private Sprite sprite;
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
        var inventory = GameObject.Find("VRMissionManager").GetComponent<InventoryMulti>();

        if (inventory == null) {
            return false;
        }

        /*
        if (!inventory.AddLoot(item: item)) {
            return false;
        }*/
        /*
        if (!inventory.AddLootRPC(item: item)) {
            return false;
        }*/

        Debug.Log("Interact add loot");
        inventory.AddLootRPC(item: item);

        inventory.RefreshInventory();

        photonView.RPC("DestroyFieldItem", RpcTarget.All);
        return true;
    }
    
    [PunRPC]
    private void DestroyFieldItem() {
        Destroy(this.gameObject);
    }
}
