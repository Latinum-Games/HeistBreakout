using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonEventHandlers : MonoBehaviour, ISelectHandler, IDeselectHandler,  IPointerEnterHandler, IPointerExitHandler {
    
    // Event Handlers
    public UnityEvent onSelectAction;
    public UnityEvent onDeselectAction;
    
    public void OnSelect(BaseEventData eventData) {
        onSelectAction.Invoke();
    }
    
    public void OnDeselect(BaseEventData eventData) {
        onDeselectAction.Invoke();
    }
    
    public void OnPointerEnter(PointerEventData eventData) {
        onSelectAction.Invoke();
    }
    
    public void OnPointerExit(PointerEventData eventData) {
        onDeselectAction.Invoke();
    }
}
