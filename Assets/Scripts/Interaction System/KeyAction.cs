using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyAction : MonoBehaviour, IInteractable {
    [SerializeField] private string prompt;
    //Interface initializer
    public string InteractionPrompt => prompt;
    //Interaction key
    public bool Interact(Interactor interactor) {
        Debug.Log("EXECUTING KEY ACTION");
        return true;
    }
    
}
