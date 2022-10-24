using UnityEngine;
using TMPro;

public class Gachapon : MonoBehaviour {
    [Header("Gacha Pity")]
    [SerializeField] private int epicPity = 10;
    [SerializeField] private int legendaryPity = 20;
    // TODO: SAVE PITY COUNTER GLOBALLY
    
    private int epicCounter = 1;
    private int legendaryCounter = 1;
    private string reward;

    public TextMeshProUGUI rewardLabel;

    // Initialize Label
    private void Start(){
        rewardLabel.text = "";
    }

    public void GachaPull() {
        //Check Pity
        if (legendaryCounter == legendaryPity) {
            rewardLabel.text = "Legendary";
            legendaryCounter = 1;
        }
        else if (epicCounter == epicPity) {
            rewardLabel.text = "Epic";
            Debug.Log("Epic");
            epicCounter = 1;
        }
        else {
            // Normal Pull
            int pull = Random.Range(1, 100);

            if (pull <= 60) { // Common Prize
                rewardLabel.text = "Common";

            } else if (pull <= 90) { // Rare Prize
                rewardLabel.text = "Rare";
                
            } else if (pull <= 98) { // Epic Prize
                rewardLabel.text = "Epic";
                epicCounter = 1;
                
            } else if (pull <= 100) { // Legendary Prize
                rewardLabel.text = "Legendary";
                legendaryCounter = 1;
                
            }
        }

        // Continue Counter of Gacha
        epicCounter ++;
        legendaryCounter ++;
    }
}