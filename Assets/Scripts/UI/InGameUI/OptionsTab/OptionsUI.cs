using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour {
    
    // Buttons
    [SerializeField] private GameObject exitButton;
    
    private void Start() {
        exitButton.GetComponent<Button>().onClick.AddListener(ExitToMainMenu);
    }

    private void ExitToMainMenu() {
        SceneManager.LoadScene("UI Heist Breakout");
    }
}
