using System;
using TMPro;
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

    public void Start() {
        // TODO ADD CHECKER TO LOAD ALL THE RESOURCES BEFOREHAND, check if not null
        RefreshInventoryItems();
    }

    public void SetInventory(Inventory inventory) {
        this.inventory = inventory;
        RefreshInventoryItems();
    }

    public void OnInventoryChange() {
        RefreshInventoryItems();
    }
    
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
    
    private void OnSelectButton(GameObject target) {
        LeanTween.cancel(target);
        LeanTween.scale(target, new Vector3(1.1f, 1.1f, 1.1f), 0.5f).setEaseOutElastic();
    }
    
    private void OnDeselectButton(GameObject target) {
        LeanTween.cancel(target);
        LeanTween.scale(target, Vector3.one, 0.5f).setEaseOutElastic();
    }
}
