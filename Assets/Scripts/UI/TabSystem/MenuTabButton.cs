using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class MenuTabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler {

    [SerializeField] private MenuTabGroup menuTabGroup;
    public Image background;
    public UnityEvent onTabSelected;
    public UnityEvent onTabDeselected;
    
    public void OnPointerEnter(PointerEventData eventData) {
        menuTabGroup.OnTabEnter(this);
    }

    public void OnPointerClick(PointerEventData eventData) {
        menuTabGroup.OnTabSelected(this);
    }

    public void OnPointerExit(PointerEventData eventData) {
        menuTabGroup.OnTabExit(this);
    }

    public void Select() {
        onTabSelected?.Invoke();
    }

    public void Deselect() {
        onTabDeselected?.Invoke();
    }

    private void Start() {
        background = GetComponent<Image>();
        menuTabGroup.Subscribe(this);
    }
}
