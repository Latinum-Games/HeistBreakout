using System;
using System.Security.Cryptography;
using UnityEngine;

public class  Interactor : MonoBehaviour {

    // Private
    //Interaction variables
    [Header("Player Interaction Area")]
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask interactableMask;

    [Header("Interactable")]
    [SerializeField] private int numOfInteractablesFound;
    [SerializeField] public InteractionPromptUI interactionPromptUI;
    private readonly Collider2D[] colliders = new Collider2D[3];
    private IInteractable interactable;

    private void Start() {
        try {
            if (interactionPromptUI == null) {
                Destroy(this.GetComponent<Interactor>());
            }
        }
        catch (Exception e) {
            Debug.Log("ERROR DE UWU");
            Console.WriteLine(e);
            throw;
        }
    }

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
        
        //Interaction enabled if items are detected near player
        if (numOfInteractablesFound > 0) {
            interactable = colliders[0].GetComponent<IInteractable>();

            if (interactable != null) {
                if (!interactionPromptUI.isDisplayed) {
                    interactionPromptUI.ShowInteractionPrompt(interactable.InteractionPrompt);
                }
                
                if (keyPress) { // ->
                    Debug.Log("Interactuable tecla");
                    interactable.Interact(this);
                }
            }
        } else {
            interactable = null;
            if (interactionPromptUI.isDisplayed) interactionPromptUI.CloseInteractionPrompt();
        }
    }

    private void OnDrawGizmos() {
        //Red circle representation for interactable items in map
         Gizmos.color = Color.red;
         Gizmos.DrawWireSphere(interactionPoint.position, interactionPointRadius);
    }
}
