using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    [Header("Weight Variables")]
    [SerializeField] private int maxWeight = 50;
    [SerializeField] private int currentWeight = 0;
    // Private Arguments
    [SerializeField] private List<Item> itemList;
    
    // TODO: CREATE UI TO DISPLAY THE ITEMS
    // TODO: CREATE A FUNCTION TO DROP THE ITEMS
    // TODO: MIGRATE ALL THE FUNCTIONS TO A PLAYER PARENT CODE THAT UNIFIES ALL THE INDIVIDUAL CODES INTO A SINGLE INSTANCE?
    [SerializeField] private InventoryUI inventoryUI;

    private void Awake() {
        this.AddLoot(new Item());
        this.AddLoot(new Item());
        this.AddLoot(new Item());
        this.AddLoot(new Item());
        this.AddLoot(new Item());
        this.AddLoot(new Item());
        this.AddLoot(new Item());
        this.AddLoot(new Item());
        this.AddLoot(new Item());
        this.AddLoot(new Item());
        this.AddLoot(new Item());
        this.AddLoot(new Item());
        this.AddLoot(new Item());
        this.AddLoot(new Item());
        this.AddLoot(new Item());
        this.AddLoot(new Item());
        this.AddLoot(new Item());
        this.AddLoot(new Item());
        this.AddLoot(new Item());
        this.AddLoot(new Item());
        

        inventoryUI.SetInventory(this);
    }

    public bool AddLoot(Item item) {
        if (currentWeight + item.weight > maxWeight || currentWeight == maxWeight) {
            return false;
        }

        itemList.Add(item);
        currentWeight += item.weight;
        return true;
    }

    public List<Item> GetItemList() {
        return itemList;
    }
}
