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
    private int currentHeadIndex = -1;
    private int currentPantIndex = -1;

    private Vector3 originOffset = new Vector3(0, 11, -11);
    

    private void Start()
    {
        headImage.color = Color.cyan;
        InitHeadItemStatus();
    }

    public void InitHeadItemStatus()
    {
        for (int i = 0; i < headButtons.Length; i++)
        {
            UpdateHeadItemStatus(i, InventoryManager.Instance.InvenHeadItemStatus[i]);
        }
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
        InitHeadItemStatus();
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
        PurchasedItem();
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
            NoPurchasedItem();
        }
        else PurchasedItem();
        currentHeadIndex = index;
    }

    public void PurchasedItem()
    {
        coinButton.gameObject.SetActive(false);
        watchVideoButton.gameObject.SetActive(false);
        selectButton.gameObject.SetActive(true);
    }

    public void NoPurchasedItem()
    {
        coinButton.gameObject.SetActive(true);
        watchVideoButton.gameObject.SetActive(true);
        selectButton.gameObject.SetActive(false);
    }

    public void OnPantButtonPressed(int index)
    {
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
        if (InventoryManager.Instance.PlayerCoin < 50) return;
        switch (currentItemUIIndex)
        {
            case 0:
                InventoryManager.Instance.PlayerCoin -= 50;
                if (InventoryManager.Instance.InvenHeadItemStatus.ContainsKey(currentHeadIndex))
                {
                    InventoryManager.Instance.InvenHeadItemStatus[currentHeadIndex] = 1;
                }
                else InventoryManager.Instance.InvenHeadItemStatus.Add(currentHeadIndex, 1);
                InventoryManager.Instance.SaveDataToJsonFile();
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
                if (currentHeadIndex != -1)
                {
                    if (InventoryManager.Instance.InvenHeadItemStatus.ContainsKey(currentHeadIndex))
                    {
                        InventoryManager.Instance.InvenHeadItemStatus[currentHeadIndex] = 1;
                    }
                    else InventoryManager.Instance.InvenHeadItemStatus.Add(currentHeadIndex, 1);

                    InventoryManager.Instance.SaveDataToJsonFile();
                }
                break;
            default:
                break;
        }  
    }

    public void OnSelectButtonPressed()
    {
        InventoryManager.Instance.SaveDataToJsonFile();
    }

    public void OnCloseButtonPressed()
    {
        UIManager.Instance.CloseAll();
        InventoryManager.Instance.UpdatePlayerSkin();
        Cache.GenCameraFollow(UIManager.Instance.mainCamera).CameraOffset = originOffset;
        UIManager.Instance.OpenUI<CanvasMainMenu>();
        GameManager.Instance.UpdateGameState(GameState.MainMenu);
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
