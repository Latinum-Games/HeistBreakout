using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable {
    //Interaction prompt getter
    public string InteractionPrompt { get; }
    //Determines if an item is interactuable or not 
    public bool Interact(Interactor interactor);
}
