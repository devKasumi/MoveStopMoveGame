using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CanvasUISkin : UICanvas
{
    [SerializeField] private GameObject headsUI;
    [SerializeField] private GameObject pantsUI;
    [SerializeField] private GameObject armsUI;
    [SerializeField] private GameObject fullSetUI;
    [SerializeField] private ScrollRect scrollRect;

    [SerializeField] private Image headImage;
    [SerializeField] private Image pantImage;
    [SerializeField] private Image armImage;
    [SerializeField] private Image fullSetImage;

    [SerializeField] private TextMeshProUGUI bonusText;

    [SerializeField] private GameObject coinButton;
    [SerializeField] private GameObject watchVideoButton;
    [SerializeField] private GameObject selectButton;

    [SerializeField] private HeadButton[] headButtons;

    private int currentItemUIIndex = 0;
    //private int selectedHeadIndex;
    private int currentHeadIndex = -1;
    //private int selectedPantIndex;
    private int currentPantIndex = -1;

    private void Start()
    {
        headImage.color = Color.cyan;
    }

    public void OnHeadsUIButton()
    {
        DisableAllSkinUI();
        headsUI.gameObject.SetActive(true);
        scrollRect.content = Cache.GenRectTransform(headsUI);
        DisableItemFocus();
        headImage.color = Color.cyan;
        bonusText.text = Constants.HEAD_BONUS;
        currentItemUIIndex = 0;
        for (int i = 0; i < headButtons.Length; i++)
        {
            // TODO: sua lai logic 
            UpdateHeadItemStatus(i, InventoryManager.Instance.InvenHeadItemStatus[i]); 
        }
    }

    public void OnPantsUIButton()
    {
        DisableAllSkinUI();
        pantsUI.gameObject.SetActive(true);
        scrollRect.content = Cache.GenRectTransform(pantsUI);
        DisableItemFocus();
        pantImage.color = Color.cyan;
        bonusText.text = Constants.PANT_BONUS;
        currentItemUIIndex = 1;
    }

    public void OnArmsUIButton()
    {
        DisableAllSkinUI();
        armsUI.gameObject.SetActive(true);
        scrollRect.content = Cache.GenRectTransform(armsUI);
        DisableItemFocus();
        armImage.color = Color.cyan;
        bonusText.text = Constants.SHIELD_BONUS;
        currentItemUIIndex = 2;
    }

    public void OnFullSetUIButton()
    {
        DisableAllSkinUI();
        fullSetUI.gameObject.SetActive(true);
        scrollRect.content = Cache.GenRectTransform(fullSetUI);
        DisableItemFocus();
        fullSetImage.color = Color.cyan;
        bonusText.text = Constants.FULL_SET_BONUS;
        currentItemUIIndex = 3;
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

    public void OnHeadButtonPressed(int index)
    {
        InventoryManager.Instance.UpdatePlayerHead(index);
        if (InventoryManager.Instance.InvenHeadItemStatus[index] == 0)
        {
            NoPurchasedHeadItem();
        }
        else PurchasedHeadItem();
        currentHeadIndex = index;
    }

    public void PurchasedHeadItem()
    {
        coinButton.gameObject.SetActive(false);
        watchVideoButton.gameObject.SetActive(false);
        selectButton.gameObject.SetActive(true);
    }

    public void NoPurchasedHeadItem()
    {
        coinButton.gameObject.SetActive(true);
        watchVideoButton.gameObject.SetActive(true);
        selectButton.gameObject.SetActive(false);
    }

    public void OnPantButtonPressed(int index)
    {
        //Debug.LogError("on pant button pressed!!!    " + index);
        InventoryManager.Instance.UpdatePlayerPant(index);
        currentPantIndex = index;
    }

    public void OnArmButtonPressed(int index)
    {

    }

    public void OnFullSetButtonPressed(int index)
    {

    }

    public void OnCoinButtonPressed()
    {
        switch (currentItemUIIndex)
        {
            case 0:
                //selectedHeadIndex = currentHeadIndex;
                //InventoryManager.Instance.UpdatePlayerHead(currentHeadIndex);
                break;
            case 1:
                //selectedPantIndex = currentPantIndex;
                //InventoryManager.Instance.UpdatePlayerPant(currentPantIndex);
                break;
            case 2:
                break;
            case 3:
                break;
            default:
                break;
        }
    }

    public void OnWatchVideoButtonPressed()
    {
        switch (currentItemUIIndex)
        {
            case 0:
                if (InventoryManager.Instance.InvenHeadItemStatus.ContainsKey(currentHeadIndex))
                {
                    InventoryManager.Instance.InvenHeadItemStatus[currentHeadIndex] = 1;
                }
                else InventoryManager.Instance.InvenHeadItemStatus.Add(currentHeadIndex, 1);
                break;
            default:
                break;
        }  
    }

    public void OnSelectButtonPressed()
    {

    }

    public void OnCloseButtonPressed()
    {
        //InventoryManager.Instance.UpdatePlayerHead(currentHeadIndex);
        //InventoryManager.Instance.UpdatePlayerPant(currentPantIndex);
    }

    public void UpdateHeadItemStatus(int headItemIndex, int currentStatus)
    {
        if (currentStatus == 0)
        {
            headButtons[headItemIndex].LockImage.SetActive(true);
            headButtons[headItemIndex].UnlockImage.SetActive(false);
        }
        else
        {
            headButtons[headItemIndex].LockImage.SetActive(false);
            headButtons[headItemIndex].UnlockImage.SetActive(true);
        }
    }
    
}
