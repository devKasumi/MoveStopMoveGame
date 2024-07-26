using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private FloatingJoystick joystick;

    private Vector3 moveDirection;
    private float inputX;
    private float inputZ;

    private float frameRate = 1f;
    private float time = 0;

    private void Awake()
    {
        GetDataFromJsonFile();
    }

    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (isCharacterDeath)
        {
            ChangeAnimation(Constants.ANIMATION_DEAD);
            return;
        }

        Move();

        //AttackEnemy();

        CheckEnemyCurrentStatus();

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.C))
        {
            // bao loi vi thang player inherit monobehavior
            Debug.LogError((int)JsonFileHandler.ReadFromJson<DataFromJson>(Constants.JSON_FILE_NAME).weapon.WeaponType);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // TODO: xep lai logic vao UI chon weapon va skin!!!
            JsonFileHandler.SaveToJson<Character>(this, Constants.JSON_FILE_NAME);
        }
#endif
    }

    public override void OnInit()
    {
        base.OnInit();

        UpdateWeaponImage();
    }

    public void GetDataFromJsonFile()
    {
        DataFromJson dataFromJson = JsonFileHandler.ReadFromJson<DataFromJson>(Constants.JSON_FILE_NAME);
        Weapon = dataFromJson.weapon != null ? dataFromJson.weapon : LevelManager.Instance.PoolControl.PlayerDefaultWeapon();
        SkinColor.material = dataFromJson.skinColor != null ? dataFromJson.skinColor.material : SkinDataSO.SkinMaterial(CommonEnum.ColorType.Red);
        PantMaterial.material = dataFromJson.pantMaterial != null ? dataFromJson.pantMaterial.material : PantDataSO.PantMaterial(CommonEnum.PantType.Batman);
    }

    public void Move()
    {
        inputX = joystick.Horizontal;
        inputZ = joystick.Vertical;

        moveDirection = new Vector3(inputX * MoveSpeed(), 0f, inputZ * MoveSpeed());    

        Rb.velocity = moveDirection;

        if (inputX != 0f && inputZ != 0f)
        {
            TF.rotation = Quaternion.LookRotation(Rb.velocity);
            SpawnPoint().rotation = Quaternion.LookRotation(Rb.velocity);
        }

        if (!joystick.IsResetJoystick())
        {
            ChangeAnimation(Constants.ANIMATION_RUN);
        }
        else
        {
            AttackEnemy();
        }
    }

    public void AttackEnemy()
    {
        if (time >= frameRate)
        {
            time = 0;

            if (ListTarget().Count > 0 && joystick.IsResetJoystick())
            {
                FaceEnemy();
                ChangeAnimation(Constants.ANIMATION_ATTACK);
                Attack();
            }
        }
    }
}

[System.Serializable]
public class DataFromJson
{
    public Weapon weapon;
    
    public SkinnedMeshRenderer skinColor;

    public SkinnedMeshRenderer pantMaterial;
}
