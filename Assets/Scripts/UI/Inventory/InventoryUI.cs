using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {

    private Inventory inventory;
    [SerializeField] private Transform itemSlotContainer;
    [SerializeField] private Transform itemSlotTemplate;

    public void Awake() {
        itemSlotContainer = transform.Find("ItemSlotContainer");
        itemSlotTemplate = transform.Find("ItemSlotContainer/ItemSlotTemplate");
    }

    public void SetInventory(Inventory inventory) {
        this.inventory = inventory;
        RefreshItemItems();
    }

    private void RefreshItemItems() {
        // Initial Coordinates Relative to Container
        var x = 0;
        var y = 0;
        
        // Size of Item Frame
        var itemSlotCellSize = 110f;
        var offsetX = 20f;
        var offsetY = 20f;

        foreach (var item in inventory.GetItemList()) {
            var itemSlotRectTransform =
                Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            
            itemSlotRectTransform.gameObject.SetActive(true);
            itemSlotRectTransform.anchoredPosition = new Vector2((x * itemSlotCellSize) + offsetX, (y * -itemSlotCellSize) - offsetY);
            var sprite = item.GetSprite();
            itemSlotRectTransform.GetComponent<Image>().sprite = sprite;
            x++;

            // TODO: UPDATE DYNAMICALLY THE INVENTORY

            if (x >= 6) {
                x = 0;
                y++;
            }
        }
    }
    

}
