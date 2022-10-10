using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyAction : MonoBehaviour, IInteractable {
    
    public string InteractionPrompt { get; }
    public void Interact(Interactor interactor) {
        throw new System.NotImplementedException();
    }
    
}
