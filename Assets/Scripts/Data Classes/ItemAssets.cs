using UnityEngine;
using UnityEngine.UI;

public class ItemAssets : MonoBehaviour {
    
    // Singleton to load assets from system files
    public static ItemAssets Instance { get; private set; }
    public Sprite ring = null;
    public Sprite harp = null;
    public Sprite staff = null;
    public Sprite necklace = null;
    public Sprite goblet = null;
    public Sprite crown = null;
    public Sprite diamond = null;
    public Sprite emerald = null;
    public Sprite egg = null;
    public Sprite jar = null;
    public Sprite faceMask = null;
    public Sprite mask = null;
    public Sprite globe = null;
    public Sprite duck = null;
    public Sprite clock = null;
    public Sprite ruby = null;
    public Sprite sapphire = null;

    private void Awake() {
        Instance = this;
    }

    public void Start() {
        // Load all assets from resources
        ring = Resources.Load<Sprite>("Sprites/Items/Anillo");
        harp = Resources.Load<Sprite>("Sprites/Items/Arpa");
        staff = Resources.Load<Sprite>("Sprites/Items/Cetro");
        necklace = Resources.Load<Sprite>("Sprites/Items/Collar");
        goblet = Resources.Load<Sprite>("Sprites/Items/Copa");
        crown = Resources.Load<Sprite>("Sprites/Items/Corona");
        diamond = Resources.Load<Sprite>("Sprites/Items/Diamante");
        emerald = Resources.Load<Sprite>("Sprites/Items/Esmeralda");
        egg = Resources.Load<Sprite>("Sprites/Items/Huevo");
        jar = Resources.Load<Sprite>("Sprites/Items/Jarron");
        faceMask = Resources.Load<Sprite>("Sprites/Items/Mascara");
        mask = Resources.Load<Sprite>("Sprites/Items/Mascarilla");
        globe = Resources.Load<Sprite>("Sprites/Items/Mundo");
        duck = Resources.Load<Sprite>("Sprites/Items/Pato");
        clock = Resources.Load<Sprite>("Sprites/Items/Reloj");
        ruby = Resources.Load<Sprite>("Sprites/Items/Ruby");
        sapphire = Resources.Load<Sprite>("Sprites/Items/Zafiro");
    }
}
