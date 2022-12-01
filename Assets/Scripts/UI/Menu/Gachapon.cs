using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gachapon : MonoBehaviour {
    
    //Pity counter targets
    [Header("Gacha Pity")]
    [SerializeField] private int epicPity = 10;
    [SerializeField] private int legendaryPity = 20;
    // TODO: SAVE PITY COUNTER GLOBALLY

    [Header("Rewards")] 
    [SerializeField] private List<Item> rewards;

    [Header("Gacha UI")] 
    [SerializeField] private GameObject rewardText;
    [SerializeField] private GameObject rewardTextPositionIn;
    [SerializeField] private GameObject rewardTextPositionOut;
    
    [SerializeField] private GameObject gachaBox;
    [SerializeField] private GameObject gachaBoxPositionIn;
    [SerializeField] private GameObject gachaBoxPositionOut;

    //Pity counters
    private int epicCounter = 1;
    private int legendaryCounter = 1;
    private string reward;

    //Reward text for UI
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

    // ANIMACIÓN DE GACHAPON
    public void GachaAnimation() {
        LeanTween.cancel(gachaBox);
        // Gacha scale animation
        LeanTween.scale(gachaBox, new Vector3(1.2f, 1.2f, 1.2f), 0.2f).setLoopPingPong(10).setOnComplete(() => {
            
            // Gacha reward label animation in
            LeanTween.cancel(rewardText);
            LeanTween.move(rewardText, gachaBoxPositionIn.transform.position, 0.7f).setEaseOutExpo().setOnComplete(() => {
                
                // Gacha reward label animation out
                LeanTween.delayedCall(rewardText, 2f, () => {
                    LeanTween.move(rewardText, gachaBoxPositionOut.transform.position, 0.7f).setEaseOutExpo();
                });
            
                GachaPull();
            });
        });
    }
}