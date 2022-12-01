using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FieldItemMulti : MonoBehaviourPunCallbacks, IInteractable { // TODO RENAME FUNCTION TO FIELD_ITEM or smt
    
    // Private Variables
    [Header("Item")]
    [SerializeField] public Item item;
    [SerializeField] private Sprite sprite;
    
    //Unity Events
    [Header("Events")] 
    public UnityEvent PickItem;
    public string InteractionPrompt => item.interactionPrompt == string.Empty ? "Pickup " + item.title  : item.interactionPrompt;

    [SerializeField] InventoryMulti inventory;
    
    private void Start() {
        this.name = item.title;
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
        inventory = GameObject.Find("VRMissionManager").GetComponent<InventoryMulti>();

        if (inventory == null) {
            return false;
        }

        
        if (!inventory.AddLoot(item: item)) {
            return false;
        }
        
        //inventory.AddLootRPC(item: item);

        //photonView.RPC("RefreshInventoryMulti", RpcTarget.All);
        inventory.RefreshInventory();

        photonView.RPC("DestroyFieldItem", RpcTarget.All);
        return true;
    }
    
    [PunRPC]
    private void DestroyFieldItem() {
        PickItem.Invoke();
        Destroy(this.gameObject);
    }

    [PunRPC]
    private void RefreshInventoryMulti() {
        inventory.RefreshInventory();
    }
}
