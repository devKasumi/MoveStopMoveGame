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

    //[SerializeField] private List<List<Material>> mats = new List<List<Material>>();

    [SerializeField] private List<Material> colorMats = new List<Material>();

    private List<List<GameObject>> weaponObjectsUIList = new List<List<GameObject>>(); // chua cac list weapon ui nho(gom 4 thang ui hien len)
    private List<GameObject> currentWeaponUIList = new List<GameObject>(); // chua cac weapon to ben duoi 
    private int currentWeaponUIIndex; // no chinh la index cua enum weapon
    private Material[] currentMats;

    [SerializeField] private List<GameObject> headObjects = new List<GameObject>();
    [SerializeField] private List<Vector3> headObjectPos = new List<Vector3>();

    private GameObject currentHeadItem = null;
    private Material currentPantMat = null;

    private Dictionary<int, string> playerWeaponStatus = new Dictionary<int, string>();
    private Dictionary<int, string> playerHeadItemStatus = new Dictionary<int, string>();
    private Dictionary<int, string> playerPantItemStatus = new Dictionary<int, string>();

    private Dictionary<int, Material[]> CustomWeaponMats = new Dictionary<int, Material[]>();

    private void Awake()
    {
        GetDataFromJsonFile();
    }

    private void Start()
    {
        OnInitWeaponUI();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SaveDataToJsonFile();
        }
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

                Material[] materials = new Material[weaponUIMesh.materials.Length];

                if (j == 0)
                {
                    weaponUIMesh.materials = CustomWeaponMats[i];                                                 
                }
                else if (j == 2)
                {
                    for (int k = 0; k < weaponUIMesh.materials.Length; k++)
                    {
                        materials[k] = Cache.GenWeapon(poolControl.ListWeaponPrefabs[i]).WeaponData.PlayerWeaponFirstSet;
                    }
                    weaponUIMesh.materials = materials;
                }
                else if (j == 3)
                {
                    for (int k = 0; k < weaponUIMesh.materials.Length; k++)
                    {
                        materials[k] = Cache.GenWeapon(poolControl.ListWeaponPrefabs[i]).WeaponData.PlayerWeaponSecondSet;
                    }
                    weaponUIMesh.materials = materials;
                }
                else if (j == 4)
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

    public void OnInitHeadUI()
    {

    }

    public void OnInitPantUI()
    {

    }

    public Dictionary<int, Material[]> DefaultCustomWeapon()
    {
        Dictionary<int, Material[]> defaultCustomWeapon = new Dictionary<int, Material[]>();    
        for (int i =0;i < weaponObjects.Count;i++)
        {
            MeshRenderer meshRenderer = Cache.GenMeshRenderer(weaponObjects[i]);
            defaultCustomWeapon.Add(i, meshRenderer.sharedMaterials);
        }

        return defaultCustomWeapon; 
    }

    public void UpdateCurrentWeapon(int index)
    {
        Cache.GenMeshRenderer(currentWeaponUIList[currentWeaponUIIndex]).materials = Cache.GenMeshRenderer(weaponObjectsUIList[currentWeaponUIIndex][index]).materials;
        currentMats = Cache.GenMeshRenderer(weaponObjectsUIList[currentWeaponUIIndex][index]).materials;
    }

    //public List<GameObject> ListHeadItem => headObjects;
    public void UpdatePlayerHead(int index)
    {
        if (currentHeadItem != null)
        {
            Destroy(currentHeadItem.gameObject);
        }
        GameObject head = Instantiate(headObjects[index]);
        Transform headTf = Cache.GenTransform(head);
        headTf.parent = Cache.GenTransform(player.Head);
        headTf.localPosition = headObjectPos[index];
        headTf.localRotation = Quaternion.identity;
        currentHeadItem = head;
    }

    public void UpdatePlayerPant(int index)
    {
        player.PantMaterial.material = player.PantDataSO.Materials[index];
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

    public int MaterialCount => Cache.GenMeshRenderer(currentWeaponUIList[currentWeaponUIIndex]).materials.Length;

    public void UpdatePlayerWeapon()
    {
        if (currentWeaponUIIndex != (int)player.Weapon.WeaponType)
        {
            player.Weapon = poolControl.SetPlayerWeapon(currentWeaponUIIndex);
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

    public void GetDataFromJsonFile()
    {
        JsonData jsonData = JsonFileHandler.ReadFromJson<JsonData>(Constants.JSON_FILE_NAME);
        CustomWeaponMats = jsonData.CustomWeaponMats.Count != 0 ? jsonData.CustomWeaponMats : DefaultCustomWeapon();
        player.Weapon = LevelManager.Instance.PoolControl.InitPlayerWeapon((int)jsonData.PlayerWeaponType);
        player.SkinColor.material = player.SkinDataSO.SkinMaterial(jsonData.PlayerSkinColor);
        player.PantMaterial.material = player.PantDataSO.PantMaterial(jsonData.PlayerPantType);
        //Debug.LogError((CommonEnum.ColorType)(colorMats.IndexOf(player.SkinColor.material)));
        //Debug.LogError(colorMats.IndexOf(player.SkinDataSO.Materials[0]));
        Debug.LogError((CommonEnum.ColorType)(colorMats.IndexOf(player.SkinColor.sharedMaterial)));
    }

    public void SaveDataToJsonFile()
    {
        JsonData jsonData = new JsonData();
        jsonData.PlayerWeaponType = player.Weapon.WeaponType;
        jsonData.PlayerSkinColor = (CommonEnum.ColorType)(colorMats.IndexOf(player.SkinColor.sharedMaterial));
        jsonData.PlayerPantType = (CommonEnum.PantType)(player.PantDataSO.Materials.IndexOf(player.PantMaterial.sharedMaterial));
    }
}

[System.Serializable]
public class JsonData
{

    public CommonEnum.WeaponType PlayerWeaponType = CommonEnum.WeaponType.Hammer_0;
    public Material PlayerWeaponImageMat = null;
    public CommonEnum.ColorType PlayerSkinColor = CommonEnum.ColorType.Red;
    public CommonEnum.PantType PlayerPantType = CommonEnum.PantType.Chambi;

    public int coin = 0;

    public Dictionary<int, string> playerWeaponStatus = new Dictionary<int, string>();
    public Dictionary<int, string> playerHeadItemStatus = new Dictionary<int, string>();
    public Dictionary<int, string> playerPantItemStatus = new Dictionary<int, string>();

    public Dictionary<int, Material[]> CustomWeaponMats = new Dictionary<int, Material[]>();
}
