using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuseumMenu : MonoBehaviour
{
    [SerializeField] private GameObject trofeo1;
    [SerializeField] private GameObject trofeo2;
    [SerializeField] private GameObject trofeo3;
    [SerializeField] private GameObject trofeo4;
    [SerializeField] private GameObject trofeo5;
    [SerializeField] private GameObject trofeo6;
    [SerializeField] private GameObject trofeo7;
    [SerializeField] private GameObject trofeo8;
    [SerializeField] private GameObject back;
    //Returns to main menu
    public void OnClick_Back()
    {
        MenuManager.OpenMenu(Menu.MAIN_MENU, gameObject);
    }
    private void Start()
    {
        //Se ponen los objetos en sus posiciones con un rebote
        LeanTween.moveY(trofeo1.GetComponent<RectTransform>(), 235, 1.5f).setDelay(2.5f).setEase(LeanTweenType.easeOutBounce);
        LeanTween.moveY(trofeo2.GetComponent<RectTransform>(), 235, 1.5f).setDelay(2.5f).setEase(LeanTweenType.easeOutBounce);
        LeanTween.moveY(trofeo3.GetComponent<RectTransform>(), 235, 1.5f).setDelay(2.5f).setEase(LeanTweenType.easeOutBounce);
        LeanTween.moveY(trofeo4.GetComponent<RectTransform>(), 0, 1.5f).setDelay(2.5f).setEase(LeanTweenType.easeOutBounce);
        LeanTween.moveY(trofeo5.GetComponent<RectTransform>(), 0, 1.5f).setDelay(2.5f).setEase(LeanTweenType.easeOutBounce);
        LeanTween.moveY(trofeo6.GetComponent<RectTransform>(), -239, 1.5f).setDelay(2.5f).setEase(LeanTweenType.easeOutBounce);
        LeanTween.moveY(trofeo7.GetComponent<RectTransform>(), -239, 1.5f).setDelay(2.5f).setEase(LeanTweenType.easeOutBounce);
        //Se manda a llamar la funcion que contiene la animacion del boton de regresar con el ultimo objeto al caer
        LeanTween.moveY(trofeo8.GetComponent<RectTransform>(), -239, 1.5f).setDelay(2.5f).setEase(LeanTweenType.easeOutBounce).setOnComplete(buttonCall);

        //se configuran los listeners para poder saber cuando se pasa sobre un objeto
        SetTileListeners();
    }
    //funcion que llama al movimiento del boton back
    private void buttonCall()
    {
        LeanTween.moveX(back.GetComponent<RectTransform>(), 810, 1.5f).setEase(LeanTweenType.easeInOutBack);
    }

    //funcion que cuando se tiene el mouse encima se aumenta de tamaño y se sube el alpha
    private void onSelectItem(GameObject item)
    {
        LeanTween.delayedCall(item, 0.5f, () =>
        {
            LeanTween.cancel(item);
            LeanTween.scale(item, new Vector3(1.2f, 1.2f, 1.2f), .5f).setEase(LeanTweenType.easeOutElastic);
            LeanTween.alpha(item.GetComponent<Image>().rectTransform, 1f, 0.5f).setEase(LeanTweenType.easeOutQuad);
        });

    }
    //funcion que cuando no se tiene el mouse encima se disminuye de tamaño y se baja el alpha
    private void onDeSelectItem(GameObject item)
    {
        LeanTween.delayedCall(item, 0.5f, () =>
        {
            LeanTween.cancel(item);
            LeanTween.scale(item, new Vector3(1f, 1f, 1f), .5f).setEase(LeanTweenType.easeOutElastic);
            LeanTween.alpha(item.GetComponent<Image>().rectTransform, 0.5f, 0.5f).setEase(LeanTweenType.easeOutQuad);
        });
    }

    // se configiran los listeners y acciones de cada boton
    private void SetTileListeners()
    {
        var trofeo1Button = trofeo1.GetComponent<Button>();
        var trofeo1ButtonEvents = trofeo1.GetComponent<ButtonEventHandlers>();
        trofeo1ButtonEvents.onSelectAction.AddListener(delegate { onSelectItem(trofeo1); });
        trofeo1ButtonEvents.onDeselectAction.AddListener(delegate { onDeSelectItem(trofeo1); });

        var trofeo2Button = trofeo2.GetComponent<Button>();
        var trofeo2ButtonEvents = trofeo2.GetComponent<ButtonEventHandlers>();
        trofeo2ButtonEvents.onSelectAction.AddListener(delegate { onSelectItem(trofeo2); });
        trofeo2ButtonEvents.onDeselectAction.AddListener(delegate { onDeSelectItem(trofeo2); });

        var trofeo3Button = trofeo3.GetComponent<Button>();
        var trofeo3ButtonEvents = trofeo3.GetComponent<ButtonEventHandlers>();
        trofeo3ButtonEvents.onSelectAction.AddListener(delegate { onSelectItem(trofeo3); });
        trofeo3ButtonEvents.onDeselectAction.AddListener(delegate { onDeSelectItem(trofeo3); });

        var trofeo4Button = trofeo4.GetComponent<Button>();
        var trofeo4ButtonEvents = trofeo4.GetComponent<ButtonEventHandlers>();
        trofeo4ButtonEvents.onSelectAction.AddListener(delegate { onSelectItem(trofeo4); });
        trofeo4ButtonEvents.onDeselectAction.AddListener(delegate { onDeSelectItem(trofeo4); });

        var trofeo5Button = trofeo5.GetComponent<Button>();
        var trofeo5ButtonEvents = trofeo5.GetComponent<ButtonEventHandlers>();
        trofeo5ButtonEvents.onSelectAction.AddListener(delegate { onSelectItem(trofeo5); });
        trofeo5ButtonEvents.onDeselectAction.AddListener(delegate { onDeSelectItem(trofeo5); });

        var trofeo6Button = trofeo6.GetComponent<Button>();
        var trofeo6ButtonEvents = trofeo6.GetComponent<ButtonEventHandlers>();
        trofeo6ButtonEvents.onSelectAction.AddListener(delegate { onSelectItem(trofeo6); });
        trofeo6ButtonEvents.onDeselectAction.AddListener(delegate { onDeSelectItem(trofeo6); });

        var trofeo7Button = trofeo7.GetComponent<Button>();
        var trofeo7ButtonEvents = trofeo7.GetComponent<ButtonEventHandlers>();
        trofeo7ButtonEvents.onSelectAction.AddListener(delegate { onSelectItem(trofeo7); });
        trofeo7ButtonEvents.onDeselectAction.AddListener(delegate { onDeSelectItem(trofeo7); });

        var trofeo8Button = trofeo8.GetComponent<Button>();
        var trofeo8ButtonEvents = trofeo8.GetComponent<ButtonEventHandlers>();
        trofeo8ButtonEvents.onSelectAction.AddListener(delegate { onSelectItem(trofeo8); });
        trofeo8ButtonEvents.onDeselectAction.AddListener(delegate { onDeSelectItem(trofeo8); });
    }


}
