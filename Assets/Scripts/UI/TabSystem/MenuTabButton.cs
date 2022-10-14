using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class MenuTabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler {

    [SerializeField] private MenuTabGroup menuTabGroup;
    public Image background;
    
    public void OnPointerEnter(PointerEventData eventData) {
        menuTabGroup.OnTabEnter(this);
    }

    public void OnPointerClick(PointerEventData eventData) {
        menuTabGroup.OnTabSelected(this);
    }

    public void OnPointerExit(PointerEventData eventData) {
        menuTabGroup.OnTabExit(this);
    }

    void Start() {
        background = GetComponent<Image>();
        menuTabGroup.Subscribe(this);
    }
}
