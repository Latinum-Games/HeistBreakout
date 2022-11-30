using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class InventoryMulti : MonoBehaviourPunCallbacks {
    // Initializes item list with representation in UI
    [Header("Foreign Components")]
    [SerializeField] private List<Item> itemList;
    [SerializeField] private InventoryUIMulti inventoryUI;

    private void Awake() {
        //Setter of inventory for player
        inventoryUI.SetInventory(this);
    }

    [PunRPC]
    //Adding of items picked to the weight of the player and to the inventory
    public bool AddLoot(Item item) {
        photonView.RPC("RefreshItemListMulti", RpcTarget.Others, item.title);
        photonView.RPC("RefreshInventory", RpcTarget.Others);
        itemList.Add(item);
        return true;
    }

    //Getter for item list
    public List<Item> GetItemList() {
        return itemList;
    }

    [PunRPC]
    //Updater for the representation of items in UI
    public void RefreshInventory() {
        inventoryUI.OnInventoryChange();
    }
    
    //Getter for the count of items in list
    public int GetInventoryItemCount() {
        return itemList.Count;
    }

    [PunRPC]
    public void RefreshItemListMulti(string nameItem) {
        Item item = GameObject.Find(nameItem).GetComponent<FieldItemMulti>().item;
        itemList.Add(item);
    }
    
}
