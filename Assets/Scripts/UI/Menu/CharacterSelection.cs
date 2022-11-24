using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;

public class CharacterSelection : MonoBehaviourPunCallbacks {

    public static CharacterSelection instance;

    [SerializeField] private RawImage characterImage;
    public string characterController = "CatController";

    
    private void Awake() {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    

    public void CatSelection() {
        characterImage.texture = Resources.Load<Texture2D>("Sprites/Gato Outline");
        characterController = "CatController";
    }
    
    public void DuendeSelection() {
        characterImage.texture = Resources.Load<Texture2D>("Sprites/Duende Outline");
        characterController = "DuendeController";
    }



}
