using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using Newtonsoft.Json;

public class Player : Character
{
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private GameObject armature;
    [SerializeField] private GameObject initialGroup;
    [SerializeField] private GameObject pant;
    [SerializeField] private GameObject playerBody;
    [SerializeField] private WeaponDataSO weaponData;
    [SerializeField] private List<Weapon> listWeaponPrefabs;

    private Vector3 originPos = new Vector3(0, 1, -22);
    private Vector3 moveDirection;
    private float inputX;
    private float inputZ;

    private float frameRate = 0.8f;
    private float time = 0;

    private Vector3 rot = new Vector3(0, 174, 0);
    private Weapon currentWeapon;

    Coroutine c;
    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    // Update is called once per frame
    void Update()
    { 
        if (!GameManager.Instance.CurrentState(GameState.GamePlay))
        {
            return;
        }

        time += Time.deltaTime;

        if (isCharacterDeath)
        {
            ChangeAnimation(Constants.ANIMATION_DEAD);
            return;
        }

        Move();

        CheckEnemyCurrentStatus();
    }

    public override void OnInit()
    {
        base.OnInit();
        ChangeAnimation(Constants.ANIMATION_IDLE);
        TF.position = originPos;
        TF.rotation = Quaternion.identity;
        joystick.OnResetJoyStick();
        UpdateWeaponImage();
    }

    public void ChangeWeapon(CommonEnum.WeaponType weaponType)
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }

        currentWeapon = Instantiate(listWeaponPrefabs[(int)weaponType], holder);
    }

    public void Move()
    {
        inputX = joystick.Horizontal;
        inputZ = joystick.Vertical;

        moveDirection = new Vector3(inputX * MoveSpeed(), 0f, inputZ * MoveSpeed());    

        Rb.velocity = moveDirection;

        if (inputX != 0f && inputZ != 0f)
        {
            if (c != null)
            {
                StopCoroutine(c);
            }
            TF.rotation = Quaternion.LookRotation(Rb.velocity);
            SpawnPoint().rotation = Quaternion.LookRotation(Rb.velocity);
        }

        if (joystick.IsResetJoystick)
        {
            AttackEnemy();  
        }
    }

    public void AttackEnemy()
    {
        if (time >= frameRate)
        {
            time = 0;

            if (ListTarget().Count > 0 && joystick.IsResetJoystick)
            {
                FaceEnemy();
                c = StartCoroutine(PlayerAttack());
            }
        }
    }

    public IEnumerator PlayerAttack()
    {
        ChangeAnimation(Constants.ANIMATION_ATTACK);
        yield return new WaitForSeconds(0.2f);
        Attack();
    }
}


