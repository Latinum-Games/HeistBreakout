using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour {
    
    // Buttons
    [SerializeField] private GameObject exitButton;
    
    private void Start() {
        //Gets button and adds listener to exit to main menu
        exitButton.GetComponent<Button>().onClick.AddListener(ExitToMainMenu);
    }

    //Loads main menu 
    private void ExitToMainMenu() {
        SceneManager.LoadScene("UI Heist Breakout");
    }
}
