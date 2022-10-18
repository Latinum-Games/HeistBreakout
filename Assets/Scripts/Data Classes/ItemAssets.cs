using UnityEngine;
using UnityEngine.UI;

public class ItemAssets : MonoBehaviour {
    
    // Singleton to load assets from system files
    public static ItemAssets Instance { get; private set; }
    public Sprite duck = null;

    private void Awake() {
        Instance = this;
    }

    public void Start() {
        // Load all assets from resources
        duck = Resources.Load<Sprite>("Images/RubberDuck");
    }
}
