using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField] private Player player;
    [SerializeField] private PoolControl poolControl;
    [SerializeField] private List<GameObject> weaponObjects = new List<GameObject>();
    [SerializeField] private List<Vector3> weaponObjectPos = new List<Vector3>();
    [SerializeField] private List<Material> colorMats = new List<Material>();

    private List<List<GameObject>> weaponObjectsUIList = new List<List<GameObject>>(); // chua cac list weapon ui nho(gom 4 thang ui hien len)
    private List<GameObject> currentWeaponUIList = new List<GameObject>(); // chua cac weapon to ben duoi 
    private int currentWeaponUIIndex; // no chinh la index cua enum weapon
    //private GameObject
    private Material[] currentMats;

    private void Start()
    {
          
        for (int i = 0; i < weaponObjects.Count; i++)
        {
            List<GameObject> WeaponUIs = new List<GameObject>();
            for (int j = 0; j < weaponObjectPos.Count; j++)
            {
                GameObject weaponObjectUI = Instantiate(weaponObjects[i], weaponObjectPos[j], weaponObjects[i].transform.rotation);
                MeshRenderer weaponUIMesh = Cache.GenMeshRenderer(weaponObjectUI);
                if (i == (int)player.Weapon.WeaponType)
                {
                    weaponObjectUI.SetActive(true);
                    currentWeaponUIIndex = i;
                }
                else weaponObjectUI.SetActive(false);

                if (j == weaponObjectPos.Count - 3)
                {
                    Material[] materials = new Material[weaponUIMesh.materials.Length];
                    for (int k = 0; k < weaponUIMesh.materials.Length; k++)
                    {
                        materials[k] = Cache.GenWeapon(poolControl.ListWeaponPrefabs[i]).WeaponData.PlayerWeaponFirstSet;
                    }
                    weaponUIMesh.materials = materials;
                }
                else if (j == weaponObjectPos.Count - 2)
                {
                    Material[] materials = new Material[weaponUIMesh.materials.Length];
                    for (int k = 0; k < weaponUIMesh.materials.Length; k++)
                    {
                        materials[k] = Cache.GenWeapon(poolControl.ListWeaponPrefabs[i]).WeaponData.PlayerWeaponSecondSet();
                    }
                    weaponUIMesh.materials = materials;
                }
                else if (j == weaponObjectPos.Count - 1)
                {
                    Transform transform = Cache.GenTransform(weaponObjectUI);
                    Vector3 localScale = transform.localScale;  
                    transform.localScale = new Vector3(localScale.x*4, localScale.y*4, localScale.z*4);
                    currentWeaponUIList.Add(weaponObjectUI);
                    continue;
                }

                WeaponUIs.Add(weaponObjectUI);
            }
            weaponObjectsUIList.Add(WeaponUIs);
        }
        currentMats = new Material[MaterialCount];
    }

    private void Update()
    {
        
    }

    public void UpdateCurrentWeapon(int index)
    {
        // TODO: update weapon mesh cho thang player
        Cache.GenMeshRenderer(currentWeaponUIList[currentWeaponUIIndex]).materials = Cache.GenMeshRenderer(weaponObjectsUIList[currentWeaponUIIndex][index]).materials;
        // update player weapon khi bam select
        currentMats = Cache.GenMeshRenderer(weaponObjectsUIList[currentWeaponUIIndex][index]).materials;
        //player.Weapon.WeaponSkin.materials = Cache.GenMeshRenderer(weaponObjectsUIList[currentWeaponUIIndex][index]).materials;
    }

    public void UpdateCustomWeapon(int matIndex, int colorIndex)
    {
        Material[] materials = new Material[MaterialCount];

        switch (MaterialCount)
        {
            case 2:
                {
                    switch (matIndex)
                    {
                        case 0:
                            materials[0] = colorMats[colorIndex];
                            materials[1] = Cache.GenMeshRenderer(currentWeaponUIList[currentWeaponUIIndex]).materials[1];
                            break;
                        case 1:
                            materials[1] = colorMats[colorIndex];
                            materials[0] = Cache.GenMeshRenderer(currentWeaponUIList[currentWeaponUIIndex]).materials[0];
                            break;
                        default:
                            break;
                    }
                }
                break;
            case 3:
                {
                    switch (matIndex)
                    {
                        case 0:
                            materials[0] = colorMats[colorIndex];
                            materials[1] = Cache.GenMeshRenderer(currentWeaponUIList[currentWeaponUIIndex]).materials[1];
                            materials[2] = Cache.GenMeshRenderer(currentWeaponUIList[currentWeaponUIIndex]).materials[2];
                            break;
                        case 1:
                            materials[1] = colorMats[colorIndex];
                            materials[0] = Cache.GenMeshRenderer(currentWeaponUIList[currentWeaponUIIndex]).materials[0];
                            materials[2] = Cache.GenMeshRenderer(currentWeaponUIList[currentWeaponUIIndex]).materials[2];
                            break;
                        case 2:
                            materials[2] = colorMats[colorIndex];
                            materials[1] = Cache.GenMeshRenderer(currentWeaponUIList[currentWeaponUIIndex]).materials[1];
                            materials[0] = Cache.GenMeshRenderer(currentWeaponUIList[currentWeaponUIIndex]).materials[0];
                            break;
                    }
                }
                break;
            default:
                break;
        }
        Cache.GenMeshRenderer(currentWeaponUIList[currentWeaponUIIndex]).materials = materials;
        Cache.GenMeshRenderer(weaponObjectsUIList[currentWeaponUIIndex][0]).materials = materials;
        currentMats = materials;
        // update player weapon khi bam select
        //player.Weapon.WeaponSkin.materials = materials;
    }

    //public GameObject CurrentWeaponUI => currentWeaponUIList[currentWeaponUIIndex];

    //public List<Material> ColorMats => colorMats;

    public int MaterialCount => Cache.GenMeshRenderer(currentWeaponUIList[currentWeaponUIIndex]).materials.Length;

    public void UpdatePlayerWeapon()
    {
        player.Weapon.WeaponSkin.materials = currentMats;
        player.UpdateWeaponImage();
    }
}
