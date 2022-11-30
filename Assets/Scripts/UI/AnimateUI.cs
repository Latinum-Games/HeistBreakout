using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateUI : MonoBehaviour
{   //Si
    [SerializeField] private GameObject DuendePJ;
    [SerializeField] private GameObject GatoPJ;
    [SerializeField] private GameObject SlimePJ;
    [SerializeField] private GameObject RobotPJ;
    [SerializeField] private GameObject MagaPJ;
    [SerializeField] private GameObject MachapePJ;
    [SerializeField] private GameObject pullButtonParent;
    [SerializeField] private Image PJJugable;
    [SerializeField] private GameObject Entrada;
    [SerializeField] private GameObject Salida;
    
    void Start()
    {
      //Entradas de Inicio
      PJJugable.gameObject.transform.position = Salida.transform.position;
      LeanTween.move(PJJugable.gameObject, Entrada.transform.position, 0.5f).setDelay (1.0f).setEaseOutExpo();
      
      LeanTween.moveY(DuendePJ.GetComponent<RectTransform>(), 320, 1f)
      .setDelay(1f).setEase(LeanTweenType.easeOutBounce);

      LeanTween.moveY(GatoPJ.GetComponent<RectTransform>(), 320, 1.1f)
      .setDelay(1f).setEase(LeanTweenType.easeOutBounce);

      LeanTween.moveY(SlimePJ.GetComponent<RectTransform>(), 270, 1.2f)
      .setDelay(1f).setEase(LeanTweenType.easeOutBounce);

      LeanTween.moveY(RobotPJ.GetComponent<RectTransform>(), -150, 1.3f)
      .setDelay(1f).setEase(LeanTweenType.easeOutBounce);

      LeanTween.moveY(MagaPJ.GetComponent<RectTransform>(), -150, 1.4f)
      .setDelay(1f).setEase(LeanTweenType.easeOutBounce);

      LeanTween.moveY(MachapePJ.GetComponent<RectTransform>(), -150, 1.5f)
      .setDelay(1f).setEase(LeanTweenType.easeOutBounce);


      //Set Listeners
      ButtonAnimation(DuendePJ);
      ButtonAnimation(GatoPJ);
      ButtonAnimation(SlimePJ);
      ButtonAnimation(RobotPJ);
      ButtonAnimation(MagaPJ);
      ButtonAnimation(MachapePJ);
    }

    void ButtonAnimation(GameObject obj)
    {
      //Conseguir componentes
      var objEventHandler = obj.GetComponent<ButtonEventHandlers>();
      var objButton = obj.GetComponent<Button>();
      var objImage = obj.GetComponent<Image>();

      //Actualizar el personaje del jugador
      objButton.onClick.AddListener( () => {   
        LeanTween.cancel(PJJugable.gameObject);
        LeanTween.move(PJJugable.gameObject, Salida.transform.position, 0.5f).setEaseOutExpo();
        LeanTween.delayedCall(PJJugable.gameObject, 0.5f, () => {
            LeanTween.move(PJJugable.gameObject, Entrada.transform.position, 0.5f).setEaseOutExpo();
            PJJugable.sprite = objImage.sprite;
        });
      });

      //Hover sobre los personajes

      //Hover de entrada
      objEventHandler.onSelectAction.AddListener( () => {

        LeanTween.scale(obj, new Vector3(1.2f, 1.2f, 1.2f), 0.2f).setEaseOutExpo();
      });

      //Hover de salida
      objEventHandler.onDeselectAction.AddListener( () => {
        
        LeanTween.scale(obj, new Vector3(1.0f, 1.0f, 1.0f), 0.2f).setEaseOutExpo();
      });
    }
}
