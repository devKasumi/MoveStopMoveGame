using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Weapon : GameUnit
{
    [SerializeField] private WeaponDataSO weaponData;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private MeshRenderer weaponSkin;
    [SerializeField] private UnityEvent OnHitCharacter;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackSpeed;

    private CommonEnum.WeaponType weaponType;
    private Vector3 originPos;
    public float rotateSpeed;

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
        if (weaponData.weaponType != CommonEnum.WeaponType.Knife_0)
        {
            TF.Rotate(0f, 10f, 0f, Space.Self);
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
        PoolType = PoolType.Player_Weapon;
        GetData();
    }

    public void GetData()
    {
        weaponType = weaponData.weaponType;
        attackRange = weaponData.attackRange;
        attackSpeed = weaponData.attackSpeed;
    }

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
            bot.OnDeath(this);
        }
        else if (other.CompareTag(Constants.TAG_PLAYER))
        {
            OnHitCharacter.Invoke();
            Character character = Cache.GenCharacter(other);
            Player player = (Player)character;
            player.OnDeath(this);
        }
    }

    public void AddCurrentCharacterListener(Character character)
    {
        OnHitCharacter.AddListener(character.RemoveTarget);
    }
}
