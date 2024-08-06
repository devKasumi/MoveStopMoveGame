using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Weapon : GameUnit
{
    //[SerializeField] private MeshRenderer meshRenderer;
    //[SerializeField] private CommonEnum.ColorType colorType;
    [SerializeField] private WeaponDataSO weaponData;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private MeshRenderer weaponSkin;
    [SerializeField] private UnityEvent OnHitCharacter /*= new UnityEvent()*/;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackSpeed;

    private CommonEnum.WeaponType weaponType;
    //public int skinIndex = 0;
    private Vector3 originPos;
    public float rotateSpeed;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        OnInit();
        //GetData();
    }

    // Update is called once per frame
    void Update()
    {
        if (weaponData.weaponType != CommonEnum.WeaponType.Knife_0)
        {
            TF.Rotate(0f, 5f, 0f, Space.Self);
        }

        if (Vector3.Distance(originPos, TF.position) > attackRange)
        {
            OnDespawn();
        }
    }

    public void OnInit()
    {
        originPos = TF.position;
        rb.velocity = TF.forward * attackSpeed;
        //GetBotWeaponData();
    }

    public void OnDespawn()
    {
        BasePool.Despawn(this);
    }

    public void BotWeaponData()
    {
        weaponSkin.material = weaponData.FullSetWeaponMaterial();
        GetData();
    }

    public void PlayerWeaponData()
    {
        weaponSkin.materials = weaponData.PlayerDefaultWeaponMaterial();
        //weaponType = CommonEnum.WeaponType.Player_Weapon;
        PoolType = PoolType.Player_Weapon;
        GetData();
    }

    public void GetData()
    {
        weaponType = weaponData.weaponType;
        attackRange = weaponData.attackRange;
        attackSpeed = weaponData.attackSpeed;
    }

    //public void UpdateBotWeaponData()
    //{
    //    weaponSkin.material = 
    //}

    public CommonEnum.WeaponType WeaponType => weaponType;

    public float AttackRange => attackRange;

    public float AttackSpeed => attackSpeed;

    public WeaponDataSO WeaponData => weaponData; 

    public MeshRenderer WeaponSkin => weaponSkin;

    public void ChangeWeaponSkin()
    {

    }

    public Rigidbody Rb
    {
        get => rb;
        set => rb = value;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TAG_BOT))
        {
            LevelManager.Instance.CurrentLevel().CurrentActiveBot--;
            OnHitCharacter.Invoke();
            Character character = Cache.GenCharacter(other);
            Bot bot = (Bot)character;
            bot.ChangeState(new IdleState());
            bot.OnDeath();
        }
        else if (other.CompareTag(Constants.TAG_PLAYER))
        {
            OnHitCharacter.Invoke();
            Character character = Cache.GenCharacter(other);
            Player player = (Player)character;
            // may add some logic -> using Player instead of Character object
            player.OnDeath();
        }
    }

    public void AddCurrentCharacterListener(Character character)
    {
        OnHitCharacter.AddListener(character.RemoveTarget);
    }
}
