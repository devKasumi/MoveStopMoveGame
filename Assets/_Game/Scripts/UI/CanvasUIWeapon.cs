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
    [SerializeField] private List<GameObject> listColorButtons = new List<GameObject>();
    [SerializeField] private GameObject materialGroup1;
    [SerializeField] private List<Image> listMatColors1 = new List<Image>();
    [SerializeField] private GameObject materialGroudp2;
    [SerializeField] private List<Image> listMatColors2 = new List<Image>();
    [SerializeField] private GameObject buttonNext;
    [SerializeField] private GameObject buttonPrev;

    public void OnWeaponCustomButton()
    {

    }

    public void OnFirstWeaponButton()
    {

    }

    public void OnSecondWeaponButton()
    {

    }

    public void OnThirdWeaponButton()
    {

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
