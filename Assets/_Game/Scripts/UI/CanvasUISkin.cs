using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [SerializeField] private Image headImage;
    [SerializeField] private Image pantImage;
    [SerializeField] private Image armImage;
    [SerializeField] private Image fullSetImage;

    [SerializeField] private TextMeshProUGUI bonusText;


    public void OnHeadsUIButton()
    {
        DisableAllSkinUI();
        headsUI.gameObject.SetActive(true);
        scrollRect.content = headsUI.GetComponent<RectTransform>();
        DisableItemFocus();
        headImage.color = Color.cyan;
        bonusText.text = Constants.HEAD_BONUS;
    }

    public void OnPantsUIButton()
    {
        DisableAllSkinUI();
        pantsUI.gameObject.SetActive(true);
        scrollRect.content = pantsUI.GetComponent<RectTransform>();
        DisableItemFocus();
        pantImage.color = Color.cyan;
        bonusText.text = Constants.PANT_BONUS;
    }

    public void OnArmsUIButton()
    {
        DisableAllSkinUI();
        armsUI.gameObject.SetActive(true);
        scrollRect.content = armsUI.GetComponent<RectTransform>();
        DisableItemFocus();
        armImage.color = Color.cyan;
        bonusText.text = Constants.SHIELD_BONUS;
    }

    public void OnFullSetUIButton()
    {
        DisableAllSkinUI();
        fullSetUI.gameObject.SetActive(true);
        scrollRect.content = fullSetUI.GetComponent<RectTransform>();
        DisableItemFocus();
        fullSetImage.color = Color.cyan;
        bonusText.text = Constants.FULL_SET_BONUS;
    }

    public void DisableItemFocus()
    {
        headImage.color = Color.white;
        pantImage.color = Color.white;
        armImage.color = Color.white;
        fullSetImage.color = Color.white;
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
