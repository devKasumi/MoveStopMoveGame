using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
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
    //[SerializeField] private GameObject material_1_FocusImage_1;
    //[SerializeField] private GameObject material_1_FocusImage_2;
    [SerializeField] private List<GameObject> focusElement_1_1;
    [SerializeField] private List<GameObject> focusElement_1_2;
    [SerializeField] private List<Image> listMatColors1 = new List<Image>();
    [SerializeField] private GameObject materialGroup2;
    //[SerializeField] private GameObject material_2_FocusImage_1;
    //[SerializeField] private GameObject material_2_FocusImage_2;
    //[SerializeField] private GameObject material_2_FocusImage_3;
    [SerializeField] private List<GameObject> focusElement_2_1;
    [SerializeField] private List<GameObject> focusElement_2_2;
    [SerializeField] private List<GameObject> focusElement_2_3;
    [SerializeField] private List<Image> listMatColors2 = new List<Image>();
    [SerializeField] private GameObject buttonNext;
    [SerializeField] private GameObject buttonPrev;

    [SerializeField] private GameObject[] listWeaponFocus;
    private int focusIndex = 1;
    private int matColorFocusIndex = 0;
    private float noColorButtonPos = -400;
    private float haveColorButtonPos = -700;

    private void Start()
    {
        Cache.GenImage(listWeaponFocus[focusIndex]).enabled = true;
        UpdateSelectButtonPos(focusIndex);
        //RemoveAllFocusElement();
        if (focusIndex != 0)
        {
            DisableColorSelection();
        }
    }

    public void OnWeaponButtonPressed(int index)
    {
        RemoveAllFocusWeapon();
        RemoveAllFocusElement();
        DisableColorSelection();
        Cache.GenImage(listWeaponFocus[index]).enabled = true;
        InventoryManager.Instance.UpdateCurrentWeapon(index);
        UpdateSelectButtonPos(index);
        if (index == 0)
        {
            EnableColorSelection(InventoryManager.Instance.MaterialCount);
            //int groupNum = InventoryManager.Instance.MaterialCount == 2 ? 1 : 2;
            //FocusElementControl(groupNum, 1, true);
            OnMaterial_1_Ele_1_Pressed();
        }
    }

    public void RemoveAllFocusWeapon()
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

    public void UpdateSelectButtonPos(int focusIndex)
    {
        Cache.GenRectTransform(selectButton).localPosition = focusIndex == 0 ? new Vector3(0f, haveColorButtonPos, 0f)
                                                                             : new Vector3(0f, noColorButtonPos, 0f);
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

    //public void FocusElementControl(int groupNumber, int elementNumber, bool enable)
    //{
    //    switch (groupNumber)
    //    {
    //        case 1:
    //            {
    //                switch (elementNumber)
    //                {
    //                    case 1:
    //                        FocusElement(ref focusElement_1_1, enable);
    //                        break;
    //                    case 2:
    //                        FocusElement(ref focusElement_1_2, enable);
    //                        break;
    //                    default:
    //                        break;
    //                }
    //            }
    //            break;
    //        case 2:
    //            {
    //                switch(elementNumber)
    //                {
    //                    case 1:
    //                        FocusElement(ref focusElement_2_1, enable);
    //                        break;
    //                    case 2:
    //                        FocusElement(ref focusElement_2_2, enable);
    //                        break;
    //                    case 3:
    //                        FocusElement(ref focusElement_2_3, enable);
    //                        break;
    //                    default:
    //                        break;
    //                }
    //            }
    //            break;
    //        default:
    //            break;
    //    }
    //}

    public void OnMaterial_1_Ele_1_Pressed()
    {
        RemoveAllFocusElement();
        FocusElement(ref focusElement_1_1, true);
    }

    public void OnMaterial_1_Ele_2_Pressed()
    {
        RemoveAllFocusElement();
        FocusElement(ref focusElement_1_2, true);
    }

    public void OnMaterial_2_Ele_1_Pressed()
    {
        RemoveAllFocusElement();
        FocusElement(ref focusElement_2_1, true);
    }

    public void OnMaterial_2_Ele_2_Pressed()
    {
        RemoveAllFocusElement();
        FocusElement(ref focusElement_2_2, true);
    }

    public void OnMaterial_2_Ele_3_Pressed()
    {
        RemoveAllFocusElement();
        FocusElement(ref focusElement_2_3, true);
    }

    public void FocusElement(ref List<GameObject> elementList, bool enable)
    {
        for (int i = 0; i < elementList.Count; i++)
        {
            Cache.GenImage(elementList[i]).enabled = enable ? true : false; 
        }
    }

    public void RemoveAllFocusElement()
    {
        FocusElement(ref focusElement_1_1, false);
        FocusElement(ref focusElement_1_2, false);
        FocusElement(ref focusElement_2_1, false);
        FocusElement(ref focusElement_2_2, false);
        FocusElement(ref focusElement_2_3, false);
    }

    public void OnColorButton(GameObject colorEle)
    {
        Debug.LogError(Cache.GenImage(colorEle).color);
        listMatColors1[0].color = Cache.GenImage(focusElement_1_1[0]).enabled ? Cache.GenImage(colorEle).color : listMatColors1[0].color;
        listMatColors1[1].color = Cache.GenImage(focusElement_1_2[0]).enabled ? Cache.GenImage(colorEle).color : listMatColors1[1].color;
        listMatColors2[0].color = Cache.GenImage(focusElement_2_1[0]).enabled ? Cache.GenImage(colorEle).color : listMatColors2[0].color;
        listMatColors2[1].color = Cache.GenImage(focusElement_2_2[0]).enabled ? Cache.GenImage(colorEle).color : listMatColors2[1].color;
        listMatColors2[2].color = Cache.GenImage(focusElement_2_3[0]).enabled ? Cache.GenImage(colorEle).color : listMatColors2[2].color;
    }

    public void OnSelectButton()
    {

    }   

    public void OnButtonNext()
    {

    }

    public void OnButtonPrev()
    {

    }
}
