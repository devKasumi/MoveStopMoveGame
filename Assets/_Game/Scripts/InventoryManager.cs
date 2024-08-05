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
    private Material[] currentMats;

    [SerializeField] private List<GameObject> headObjects = new List<GameObject>();
    [SerializeField] private List<Vector3> headObjectPos = new List<Vector3>(); 

    private void Start()
    {
        OnInitWeaponUI();
    }

    private void Update()
    {
        
    }

    public void OnInitWeaponUI()
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
                        materials[k] = Cache.GenWeapon(poolControl.ListWeaponPrefabs[i]).WeaponData.PlayerWeaponSecondSet;
                    }
                    weaponUIMesh.materials = materials;
                }
                else if (j == weaponObjectPos.Count - 1)
                {
                    Transform transform = Cache.GenTransform(weaponObjectUI);
                    Vector3 localScale = transform.localScale;
                    transform.localScale = new Vector3(localScale.x * 4, localScale.y * 4, localScale.z * 4);
                    currentWeaponUIList.Add(weaponObjectUI);
                    continue;
                }

                WeaponUIs.Add(weaponObjectUI);
            }
            weaponObjectsUIList.Add(WeaponUIs);
        }
        currentMats = new Material[MaterialCount];
    }

    public void UpdateCurrentWeapon(int index)
    {
        Cache.GenMeshRenderer(currentWeaponUIList[currentWeaponUIIndex]).materials = Cache.GenMeshRenderer(weaponObjectsUIList[currentWeaponUIIndex][index]).materials;
        currentMats = Cache.GenMeshRenderer(weaponObjectsUIList[currentWeaponUIIndex][index]).materials;
    }

    //public List<GameObject> ListHeadItem => headObjects;
    public void UpdatePlayerHead(int index)
    {

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
    }

    //public GameObject CurrentWeaponUI => currentWeaponUIList[currentWeaponUIIndex];

    //public List<Material> ColorMats => colorMats;

    public int MaterialCount => Cache.GenMeshRenderer(currentWeaponUIList[currentWeaponUIIndex]).materials.Length;

    public void UpdatePlayerWeapon()
    {
        if (currentWeaponUIIndex != (int)player.Weapon.WeaponType)
        {
            player.Weapon = poolControl.PlayerWeapon(currentWeaponUIIndex);
        }
        player.Weapon.WeaponSkin.materials = currentMats;
        player.UpdateWeaponImage();
    }

    public void HideCurrentWeaponUI()
    {
        currentWeaponUIList[currentWeaponUIIndex].SetActive(false);
        for (int i = 0; i < weaponObjectsUIList[currentWeaponUIIndex].Count; i++)
        {
            weaponObjectsUIList[currentWeaponUIIndex][i].SetActive(false);
        }
    }

    public void ShowNextWeaponUI()
    {
        if (currentWeaponUIIndex < currentWeaponUIList.Count - 1)
        {
            HideCurrentWeaponUI();
            currentWeaponUIIndex++;
            currentWeaponUIList[currentWeaponUIIndex].SetActive(true);
            for (int i = 0; i < weaponObjectsUIList[currentWeaponUIIndex].Count; i++)
            {
                weaponObjectsUIList[currentWeaponUIIndex][i].SetActive(true);
            }
        } 
    }

    public void ShowPrevWeaponUI()
    {
        if (currentWeaponUIIndex > 0)
        {
            HideCurrentWeaponUI();
            currentWeaponUIIndex--;
            currentWeaponUIList[currentWeaponUIIndex].SetActive(true);
            for (int i = 0; i < weaponObjectsUIList[currentWeaponUIIndex].Count; i++)
            {
                weaponObjectsUIList[currentWeaponUIIndex][i].SetActive(true);
            }
        }
    }

    public int CurrentWeaponUIIndex => currentWeaponUIIndex;
}
