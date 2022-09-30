using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gachapon : MonoBehaviour {
    //PullCounter, PullPity, Prize (PullProbability?)
    private int ContEpic = 1;
    private int ContLegndary = 1;
    private int PityEpic = 10;
    private int PityLegendary = 20;
    private string Prize;

    //Prototipo print de premio
    public TextMeshProUGUI Premio;

    private void Start(){
        Premio.text = " ";
    }
    //Fin prototipo

    public void GachaPull() {
        //Check Pity
        if (ContLegndary == PityLegendary) {
            // Legendario Asegurado
            Premio.text = "Legendary";
            Debug.Log("Legendary");
            ContLegndary = 1;
            // Dar premio y terminar funciona
        }
        else if (ContEpic == PityEpic) {
            // Epico Asegurado
            Premio.text = "Epic";
            Debug.Log("Epic");
            ContEpic = 1;
            // Dar premio y terminar funciona
        }
        else {
            // Normal Pull
            int pull = Random.Range(1, 100);

            if (pull <= 60) {
                Premio.text = "Common";
                Debug.Log("Common");
                // Common Prize
            } else if (pull >= 61 && pull <= 90){
                Premio.text = "Rare";
                Debug.Log("Rare");
                // Rare Prize
            } else if (pull >= 91 && pull <= 98){
                Premio.text = "Epic";
                Debug.Log("Epic");
                // Epic Prize
                ContEpic = 1;
            } else if (pull >= 99 && pull <= 100){
                Premio.text = "Legendary";
                Debug.Log("Legendary");
                // Legendary Prize
                ContLegndary = 1;
            }
        }

        // Sumarle el contador de pity a todo
        ContEpic ++;
        ContLegndary ++;
    }
}