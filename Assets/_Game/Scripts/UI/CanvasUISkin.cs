using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CanvasSkin : UICanvas
{
    [SerializeField] private Image headsUI;
    [SerializeField] private Image pantsUI;
    [SerializeField] private Image armsUI;
    [SerializeField] private Image fullSetUI;
    [SerializeField] private ScrollRect scrollRect;


    public void OnHeadsUIButton()
    {
        DisableAllSkinUI();
        headsUI.gameObject.SetActive(true);
        scrollRect.content = headsUI.GetComponent<RectTransform>();
    }

    public void OnPantsUIButton()
    {
        DisableAllSkinUI();
        pantsUI.gameObject.SetActive(true);
        scrollRect.content = pantsUI.GetComponent<RectTransform>();
    }

    public void OnArmsUIButton()
    {
        DisableAllSkinUI();
        armsUI.gameObject.SetActive(true);
        scrollRect.content = armsUI.GetComponent<RectTransform>();
    }

    public void OnFullSetUIButton()
    {
        DisableAllSkinUI();
        fullSetUI.gameObject.SetActive(true);
        scrollRect.content = fullSetUI.GetComponent<RectTransform>();
    }

    public void DisableAllSkinUI()
    {
        headsUI.gameObject.SetActive(false);
        pantsUI.gameObject.SetActive(false);
        armsUI.gameObject.SetActive(false);
        fullSetUI.gameObject.SetActive(false);
    }

    public void OnPantButtonPressed(int index)
    {
        //Debug.LogError("on pant button pressed!!!    " + index);
    }

    public void OnArmButtonPressed(int index)
    {

    }

    public void OnFullSetButtonPressed(int index)
    {

    }
}
