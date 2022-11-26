using System.Collections.Generic;
using UnityEngine;

public class InventoryMulti : MonoBehaviour {
    // Initializes item list with representation in UI
    [Header("Foreign Components")]
    [SerializeField] private List<Item> itemList;
    //[SerializeField] private InventoryUI inventoryUI;

    private void Awake() {
        //Setter of inventory for player
        //inventoryUI.SetInventory(this);
    }

    //Adding of items picked to the weight of the player and to the inventory
    public bool AddLoot(Item item) {
        itemList.Add(item);
        return true;
    }

    //Getter for item list
    public List<Item> GetItemList() {
        return itemList;
    }

    //Updater for the representation of items in UI
    public void RefreshInventory() {
        //inventoryUI.OnInventoryChange();
    }
    
    //Getter for the count of items in list
    public int GetInventoryItemCount() {
        return itemList.Count;
    }
}
