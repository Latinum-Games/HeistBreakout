using UnityEngine;

public class  Interactor : MonoBehaviour {

    // Private
    [Header("Player Interaction Area")]
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask interactableMask;

    [Header("Interactable")]
    [SerializeField] private int numOfInteractablesFound;
    [SerializeField] private InteractionPromptUI interactionPromptUI;
    private readonly Collider2D[] colliders = new Collider2D[3];
    private IInteractable interactable;

    private void Update() {
        numOfInteractablesFound = Physics2D.OverlapCircleNonAlloc(
            interactionPoint.position, 
            interactionPointRadius, 
            colliders,
            interactableMask
        );

        InteractAction();
    }
    
    // Public Functions
    public void InteractAction(bool keyPress = false) {
        if (numOfInteractablesFound > 0) {
            interactable = colliders[0].GetComponent<IInteractable>();

            if (interactable != null) {
                if (!interactionPromptUI.isDisplayed) {
                    interactionPromptUI.ShowInteractionPrompt(interactable.InteractionPrompt);
                }
                
                if (keyPress) { // ->
                    interactable.Interact(this);
                }
            }
        } else {
            interactable = null;
            if (interactionPromptUI.isDisplayed) interactionPromptUI.CloseInteractionPrompt();
        }
    }

    private void OnDrawGizmos() {
         Gizmos.color = Color.red;
         Gizmos.DrawWireSphere(interactionPoint.position, interactionPointRadius);
    }
}
