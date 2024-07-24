using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField] private Player player;
    [SerializeField] private List<GameObject> weaponObjects = new List<GameObject>();
    [SerializeField] private List<Vector3> weaponObjectPos = new List<Vector3>();


}
