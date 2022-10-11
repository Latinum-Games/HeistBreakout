using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyAction : MonoBehaviour, IInteractable {
    [SerializeField] private string prompt;
    public string InteractionPrompt => prompt;
    public bool Interact(Interactor interactor) {
        Debug.Log("EXECUTING KEY ACTION");
        return true;
    }
    
}
