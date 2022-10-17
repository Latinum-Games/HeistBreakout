using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class InteractionPromptUI : MonoBehaviour {

    // Private Variables
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private TextMeshProUGUI promptText;

    // Public Variables
    public bool isDisplayed;

    private void Start() {
        promptText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        promptText.text = "";
    }

    public void ShowInteractionPrompt(string text) {
        promptText.text = text;
        uiPanel.SetActive(true);
        isDisplayed = true;

        // Animation
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, new Vector3(1.5f, 1.5f, 1.5f), 0.5f).setEasePunch();
    }

    public void CloseInteractionPrompt() {
        promptText.text = "";
        uiPanel.SetActive(false);
        isDisplayed = false;
    }
}
