using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPlayerVisual : MonoBehaviour {

    [SerializeField] private Animator _animator;
    // Start is called before the first frame update
    void Start() {
        //CharacterSelection.instance.characterController

        _animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Sprites/CharacterControllers/" + CharacterSelection.instance.characterController);
        _animator.SetInteger("Movement", 2);
    }
}
