using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasUIWeapon : UICanvas
{
    [SerializeField] private GameObject customWeaponButton;
    [SerializeField] private GameObject weapon1Button;
    [SerializeField] private GameObject weapon2Button;
    [SerializeField] private GameObject weapon3Button;
    [SerializeField] private TextMeshProUGUI weaponBonusText;
    [SerializeField] private GameObject selectButton;
    [SerializeField] private TextMeshProUGUI selectButtonText;
    [SerializeField] private GameObject colorGrid;
    [SerializeField] private List<GameObject> listColorButtons = new List<GameObject>();
    [SerializeField] private GameObject materialGroup1;
    [SerializeField] private List<Image> listMatColors1 = new List<Image>();
    [SerializeField] private GameObject materialGroup2;
    [SerializeField] private List<Image> listMatColors2 = new List<Image>();
    [SerializeField] private GameObject buttonNext;
    [SerializeField] private GameObject buttonPrev;

    [SerializeField] private GameObject[] listWeaponFocus;
    private int focusIndex = 1;

    private void Start()
    {
        Cache.GenImage(listWeaponFocus[focusIndex]).enabled = true;
        if (focusIndex != 0) DisableColorSelection();
    }

    public void OnWeaponButtonPressed(int index)
    {
        RemoveAllFocus();
        DisableColorSelection();
        Cache.GenImage(listWeaponFocus[index]).enabled = true;
        InventoryManager.Instance.UpdateCurrentWeapon(index);
        if (index == 0)
        {
            EnableColorSelection(InventoryManager.Instance.MaterialCount);
        }
    }

    public void RemoveAllFocus()
    {
        for (int i = 0; i < listWeaponFocus.Length; i++)
        {
            Cache.GenImage(listWeaponFocus[i]).enabled = false;
        }
    }

    public void DisableColorSelection()
    {
        colorGrid.SetActive(false);
        materialGroup1.SetActive(false);
        materialGroup2.SetActive(false);
    }

    public void EnableColorSelection(int matCount)
    {
        colorGrid.SetActive(true);
        switch (matCount)
        {
            case 2:
                materialGroup1.SetActive(true);
                break;
            case 3:
                materialGroup2.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void OnSelectButton()
    {

    }

    public void OnColorButton(int colorIndex)
    {

    }

    public void OnButtonNext()
    {

    }

    public void OnButtonPrev()
    {

    }
}
