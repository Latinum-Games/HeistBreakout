using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionesUI_Minijuegos : MonoBehaviour
{
    //Asignación de variables
    [SerializeField] private GameObject startButton;

    [SerializeField] private GameObject panelBotones;
    [SerializeField] private GameObject panelLuces;
    [SerializeField] private GameObject leftLights;
    [SerializeField] private GameObject rightLights;

    [SerializeField] private GameObject exitButton;

    //Al comenzar la escena despues de 1.5 segundos se movera el boton de "Start" hacia la posición -650 en el canvas en un tiempo de 1.5 segundos con una curva de animacion "easeOutBounce".
    private void Start(){
        LeanTween.moveX(startButton.GetComponent<RectTransform>(), -650, 1.5f).setDelay(1.5f).setEase(LeanTweenType.easeOutBounce);
    }
    
    //Al precionar el boton de start se acomodaran los paneles en sus posiciones originales usando escalas y movimientos en el eje X.
    public void clickOnStart(){
        LeanTween.scale(panelBotones.GetComponent<RectTransform>(), new Vector3(1f, 1f, 1f), 1.5f).setDelay(1.5f).setEase(LeanTweenType.easeInOutExpo).setOnComplete(moveExit);
        LeanTween.moveX(panelLuces.GetComponent<RectTransform>(), 0, 1.5f).setDelay(1.5f).setEase(LeanTweenType.easeInOutExpo);
        LeanTween.moveX(leftLights.GetComponent<RectTransform>(), 0, 1.5f).setDelay(1.5f).setEase(LeanTweenType.easeInOutExpo);
        LeanTween.scale(rightLights.GetComponent<RectTransform>(), new Vector3(1f, 1f, 1f), 1.5f).setDelay(1.5f).setEase(LeanTweenType.easeInOutExpo);
    }

    //Una vez que el panel de botones haya terminado la animación esta función tomara efecto debido al "setOnComplete" de la linea 24 de codigo. El botón de "Exit" se movera en el eje Y.
    public void moveExit(){
        LeanTween.moveY(exitButton.GetComponent<RectTransform>(), 0, 1.5f).setEase(LeanTweenType.easeOutBounce);
    }
}
