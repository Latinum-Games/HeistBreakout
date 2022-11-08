using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {

    //UI elements
    private Inventory inventory;
    [SerializeField] private Transform itemSlotContainer;
    [SerializeField] private Transform itemSlotTemplate;

    //ItemSlots initializers
    public void Awake() {
        itemSlotContainer = transform.Find("ItemSlotContainer");
        itemSlotTemplate = transform.Find("ItemSlotContainer/ItemSlotTemplate");
    }

    public void Start() {
        // TODO ADD CHECKER TO LOAD ALL THE RESOURCES BEFOREHAND, check if not null
        //Initializes the items
        RefreshInventoryItems();
    }

    //Assignment of the inventory
    public void SetInventory(Inventory inventory) {
        this.inventory = inventory;
        RefreshInventoryItems();
    }

    //Inventory update on change
    public void OnInventoryChange() {
        RefreshInventoryItems();
    }
    
    //UI representation of inventory based in objects picked up
    private void RefreshInventoryItems() {
        if (itemSlotContainer != null) {

            foreach (Transform child in itemSlotContainer) {
                if (child == itemSlotTemplate) continue;
                Destroy(child.gameObject);
            }

            // Initial Coordinates Relative to Container
            var x = 0;
            var y = 0;

            // Size of Item Frame
            var itemSlotCellSize = 100f;
            var offsetX = 20f;
            var offsetY = 20f;

            foreach (var item in inventory.GetItemList()) {
                var itemSlotRectTransform =
                    Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();

                // Instantiate Element in UI
                // TODO: UPDATE VIEW TO AN SCROLL VIEW
                itemSlotRectTransform.gameObject.SetActive(true);
                itemSlotRectTransform.anchoredPosition = new Vector2((x * itemSlotCellSize) + offsetX,
                    (y * -itemSlotCellSize) - offsetY);

                // Set Sprite
                var sprite = item.GetSprite();
                itemSlotRectTransform.GetComponent<Image>().sprite = sprite;

                // Set Amount Label
                var quantityLabel = itemSlotRectTransform.Find("QuantityLabel").GetComponent<TextMeshProUGUI>();
                quantityLabel.SetText(item.amount > 1 ? item.amount.ToString() : "");

                // On Select Animations
                itemSlotRectTransform.GetComponent<ButtonEventHandlers>().onSelectAction.AddListener(delegate {
                    OnSelectButton(itemSlotRectTransform.gameObject);
                });

                itemSlotRectTransform.GetComponent<ButtonEventHandlers>().onDeselectAction.AddListener(delegate {
                    OnDeselectButton(itemSlotRectTransform.gameObject);
                });

                x++;

                // Row delimiter
                if (x >= 5) {
                    x = 0;
                    y++;
                }
            }
        }
    }
    
    // TODO: REMOVE ITEM
    
    //UI Animation based in button selection
    private void OnSelectButton(GameObject target) {
        LeanTween.cancel(target);
        LeanTween.scale(target, new Vector3(1.1f, 1.1f, 1.1f), 0.5f).setEaseOutElastic();
    }
    
    //UI Animation based in button deselection
    private void OnDeselectButton(GameObject target) {
        LeanTween.cancel(target);
        LeanTween.scale(target, Vector3.one, 0.5f).setEaseOutElastic();
    }
}
