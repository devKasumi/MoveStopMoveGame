using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadButton : MonoBehaviour
{
    [SerializeField] private GameObject lockImage;
    [SerializeField] private GameObject unlockImage;

    public GameObject LockImage => lockImage;
    public GameObject UnlockImage => unlockImage;
}
