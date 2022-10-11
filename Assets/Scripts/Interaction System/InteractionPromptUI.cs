using TMPro;
using UnityEngine;

public class InteractionPromptUI : MonoBehaviour {

    // Private Variables
    private Camera mainCam;
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private TextMeshProUGUI promptText;

    // Public Variables
    public bool isDisplayed;

    private void Start() {
        promptText.text = "";
        mainCam = Camera.main;
    }

    public void ShowInteractionPrompt(string text) {
        promptText.text = text;
        uiPanel.SetActive(true);
        isDisplayed = true;
    }

    public void CloseInteractionPrompt() {
        promptText.text = "";
        uiPanel.SetActive(false);
        isDisplayed = false;
    }
}
