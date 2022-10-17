using System;
using UnityEngine;

public class ItemAssets : MonoBehaviour {
    
    // Singleton to load assets from system files
    public static ItemAssets Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    public Sprite duck = Resources.Load<Sprite>("Assets/Images/Pato de hule.png");
    
}
