using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private Character player;
    [SerializeField] private PoolControl poolControl;
    [SerializeField] private Level[] levels;

    private Level currentLevel;
    private int currentLevelIndex;

    private void Awake()
    {
        currentLevel = levels[0];
        //GetDataFromJsonFile();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnInit()
    {
        
    }

    public Level CurrentLevel() => currentLevel;

    public PoolControl PoolControl => poolControl;  

    public Character Player => player;

    //public void GetDataFromJsonFile()
    //{
    //    DataFromJson dataFromJson = JsonFileHandler.ReadFromJson<DataFromJson>(Constants.JSON_FILE_NAME);
    //    player.Weapon = dataFromJson.weapon != null ? dataFromJson.weapon : LevelManager.Instance.PoolControl.PlayerDefaultWeapon();
    //    player.SkinColor.material = dataFromJson.skinColor != null ? dataFromJson.skinColor.material : player.SkinDataSO.SkinMaterial(CommonEnum.ColorType.Red);
    //    player.PantMaterial.material = dataFromJson.pantMaterial != null ? dataFromJson.pantMaterial.material : player.PantDataSO.PantMaterial(CommonEnum.PantType.Batman);
    //}
}
