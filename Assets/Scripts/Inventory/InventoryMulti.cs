using System.Collections.Generic;
using UnityEngine;

public class InventoryMulti : MonoBehaviour {
    //Initializes amounts of weight
    [Header("Weight Variables")]
    [SerializeField] private int maxWeight = 50;
    [SerializeField] private int currentWeight = 0;
    
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
        //Checks the possibility to add more items based on weight
        if (currentWeight + item.weight > maxWeight || currentWeight == maxWeight) {
            return false;
        }

        //Checks that item is of treasure type and adds to item list
        if (item.IsStackable()) {
            var isItemInInventory = false;

            foreach (var inventoryItem in itemList) {
                if (inventoryItem.entity == item.entity) {
                    inventoryItem.amount += item.amount;
                    isItemInInventory = true;
                }
            }

            if (!isItemInInventory) {
                itemList.Add(item);
            }
        }
        else {
            itemList.Add(item);
        }
        
        currentWeight += item.weight;
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
