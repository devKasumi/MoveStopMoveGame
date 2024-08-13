using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

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
    private int HeadItemIndex = -1;

    public GameObject currentHeadItem = null;
    public Material currentPantMat = null;

    public Dictionary<int, string> InvenCustomWeaponMats = new Dictionary<int, string>();   // chua mats cua cac custom weapon
    public Dictionary<int, int> InvenHeadItemStatus = new Dictionary<int, int>();           // chua status cua tung head item

    public List<Color> customWeaponColors = new List<Color>();
    public int matsCount = 0;
     

    public int PlayerCoin;

    private void Awake()
    {
        GetDataFromJsonFile();
    }

    private void Start()
    {
        //Debug.LogError("inven start");
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

                    // update player weapon 
                    string matData = InvenCustomWeaponMats[i];
                    currentMats = new Material[InvenCustomWeaponMats[i].Length];
                    for (int k = 0; k < InvenCustomWeaponMats[i].Length; k++)
                    {
                        int index = (int)Char.GetNumericValue(InvenCustomWeaponMats[i][k]);
                        currentMats[k] = colorMats[index];
                    }
                    UpdatePlayerWeapon();
                }
                else weaponObjectUI.SetActive(false);

                Material[] materials = new Material[weaponUIMesh.materials.Length];

                if (j == 0) // la index cua custom weapon UI
                {
                    Material[] mats = new Material[InvenCustomWeaponMats[i].Length];
                    for (int k = 0; k < InvenCustomWeaponMats[i].Length; k++)
                    {
                        int index = (int)Char.GetNumericValue(InvenCustomWeaponMats[i][k]);
                        mats[k] = colorMats[index];
                    }
                    weaponUIMesh.materials = mats;
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

    public Dictionary<int, string> DefaultCustomWeapon()
    {
        Dictionary<int, string> defaultCustomWeapon = new Dictionary<int, string>();    
        for (int i =0;i < weaponObjects.Count;i++)
        {
            MeshRenderer meshRenderer = Cache.GenMeshRenderer(weaponObjects[i]);
            string mats = "";
            for (int j = 0; j < meshRenderer.sharedMaterials.Length; j++)
            {
                mats += colorMats.IndexOf(meshRenderer.sharedMaterials[j]).ToString();
            }

            defaultCustomWeapon.Add(i, mats);
        }

        return defaultCustomWeapon; 
    }

    public Dictionary<int, int> DefaultHeadItemStatus()
    {
        Dictionary<int, int> defaultHeadItem = new Dictionary<int, int>();
        for (int i = 0; i < headObjects.Count; i++)
        {
            defaultHeadItem.Add(i, 0);
        }

        return defaultHeadItem;
    }

    public void UpdateCurrentWeapon(int index)
    {
        Cache.GenMeshRenderer(currentWeaponUIList[currentWeaponUIIndex]).materials = Cache.GenMeshRenderer(weaponObjectsUIList[currentWeaponUIIndex][index]).materials;
        currentMats = Cache.GenMeshRenderer(weaponObjectsUIList[currentWeaponUIIndex][index]).materials;
    }

    public void UpdatePlayerHead(int index)
    {
        if (index == -1) return;
        if (currentHeadItem != null)
        {
            Destroy(currentHeadItem.gameObject);
        }
        if (HeadItemIndex == index)
        {
            Destroy(player.Head.transform.GetChild(0).gameObject);
            HeadItemIndex = -1;
            return;
        }
        GameObject head = Instantiate(headObjects[index]);
        Transform headTf = Cache.GenTransform(head);
        headTf.parent = Cache.GenTransform(player.Head);
        headTf.localPosition = headObjectPos[index];
        headTf.localRotation = Quaternion.identity;
        currentHeadItem = head;
        HeadItemIndex = index;
    }

    public void UpdatePlayerSkin()
    {
        if (player.Head.transform.childCount == 0) return;
        if (InvenHeadItemStatus[HeadItemIndex] != 1)
        {
            Destroy(player.Head.transform.GetChild(0).gameObject);
        }
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
        Cache.GenMeshRenderer(currentWeaponUIList[currentWeaponUIIndex]).sharedMaterials = materials;
        Cache.GenMeshRenderer(weaponObjectsUIList[currentWeaponUIIndex][0]).sharedMaterials = materials;
        currentMats = materials;
        string mats = "";
        for (int i = 0; i < materials.Length; i++)
        {
            mats += GetColorIndex(currentMats[i]);
        }
        InvenCustomWeaponMats[currentWeaponUIIndex] = mats;
    }

    public string GetColorIndex(Material mat)
    {
        int resIndex = 0;
        for (int i = 0; i < colorMats.Count; i++)
        {
            if (colorMats[i].color == mat.color) resIndex = i;
        }
        return resIndex.ToString();
    }

    public int MaterialCount => Cache.GenMeshRenderer(currentWeaponUIList[currentWeaponUIIndex]).materials.Length;

    public void UpdatePlayerWeapon()
    {
        if (currentWeaponUIIndex != (int)player.Weapon.WeaponType)
        {
            player.Weapon = poolControl.SetPlayerWeapon(currentWeaponUIIndex);
        }

        if (currentMats[0] == null)
        {
            return;
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
        InvenCustomWeaponMats = jsonData.CustomWeaponMats.Count != 0 ? jsonData.CustomWeaponMats : DefaultCustomWeapon();
        InvenHeadItemStatus = jsonData.HeadItemStatus.Count != 0 ? jsonData.HeadItemStatus : DefaultHeadItemStatus();
        PlayerCoin = jsonData.playerCoin;
        player.Weapon = LevelManager.Instance.PoolControl.InitPlayerWeapon((int)jsonData.PlayerWeaponType);
        player.SkinColor.material = player.SkinDataSO.SkinMaterial(jsonData.PlayerSkinColor);
        player.PantMaterial.material = player.PantDataSO.PantMaterial(jsonData.PlayerPantType);
        UpdatePlayerHead(jsonData.HeadItemIndex);
        SoundManager.Instance.IsSoundOn = jsonData.isSoundOn;
        SoundManager.Instance.IsVibrationOn = jsonData.isVibrationOn;

        string mats = InvenCustomWeaponMats[(int)jsonData.PlayerWeaponType];
        matsCount = mats.Length;
        for (int i = 0; i < matsCount; i++)
        {
            int index = (int)Char.GetNumericValue(mats[i]);
            customWeaponColors.Add(colorMats[index].color);
        }
    }

    public void SaveDataToJsonFile()
    {
        JsonData jsonData = new JsonData();
        jsonData.PlayerWeaponType = player.Weapon.WeaponType;
        jsonData.PlayerSkinColor = (CommonEnum.ColorType)(colorMats.IndexOf(player.SkinColor.sharedMaterial));
        jsonData.PlayerPantType = (CommonEnum.PantType)(player.PantDataSO.Materials.IndexOf(player.PantMaterial.sharedMaterial));
        jsonData.CustomWeaponMats = InvenCustomWeaponMats;
        jsonData.HeadItemStatus = InvenHeadItemStatus;
        jsonData.HeadItemIndex = this.HeadItemIndex;
        jsonData.isSoundOn = SoundManager.Instance.IsSoundOn;
        jsonData.isVibrationOn = SoundManager.Instance.IsVibrationOn;
        jsonData.playerCoin = PlayerCoin;
        JsonFileHandler.SaveToJson<JsonData>(jsonData, Constants.JSON_FILE_NAME);
    }
}

[System.Serializable]
public class JsonData
{

    public CommonEnum.WeaponType PlayerWeaponType = CommonEnum.WeaponType.Hammer_0;
    public Material PlayerWeaponImageMat = null;
    public CommonEnum.ColorType PlayerSkinColor = CommonEnum.ColorType.Red;
    public CommonEnum.PantType PlayerPantType = CommonEnum.PantType.Chambi;
    public int HeadItemIndex = -1;

    public int playerCoin = 50;

    public Dictionary<int, string> CustomWeaponMats = new Dictionary<int, string>();
    public Dictionary<int, int> HeadItemStatus = new Dictionary<int, int>();

    public bool isSoundOn = true;
    public bool isVibrationOn = true;
}
