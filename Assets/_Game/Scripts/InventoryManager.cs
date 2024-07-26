using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField] private Player player;
    [SerializeField] private PoolControl poolControl;
    [SerializeField] private List<GameObject> weaponObjects = new List<GameObject>();
    [SerializeField] private List<Vector3> weaponObjectPos = new List<Vector3>();

    private void Start()
    {

        for (int i = 0; i < weaponObjects.Count; i++)
        {
            for (int j = 0; j < weaponObjectPos.Count; j++)
            {
                GameObject weaponObjectUI = Instantiate(weaponObjects[i], weaponObjectPos[j], weaponObjects[i].transform.rotation);
                MeshRenderer weaponUIMesh = Cache.GenMeshRenderer(weaponObjectUI);
                if (i == (int)player.Weapon.WeaponType)
                {
                    weaponObjectUI.SetActive(true);
                }
                else weaponObjectUI.SetActive(false);

                if (j == weaponObjectPos.Count - 3)
                {
                    weaponUIMesh.material = Cache.GenWeapon(poolControl.ListWeaponPrefabs[i]).WeaponData.PlayerWeaponFirstSet;
                }
                else if (j == weaponObjectPos.Count - 2)
                {
                    weaponUIMesh.material = Cache.GenWeapon(poolControl.ListWeaponPrefabs[i]).WeaponData.PlayerWeaponSecondSet;
                }

            }
        }
    }
}
