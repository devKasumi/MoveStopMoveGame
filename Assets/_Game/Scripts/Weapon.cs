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
    [SerializeField] private UnityEvent OnHitCharacter = new UnityEvent();

    private CommonEnum.WeaponType weaponType;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackSpeed;
    //private Image weaponSkin;
    public int skinIndex = 0;

    private Transform tf;

    private Vector3 originPos;
    //public Quaternion originRot;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        //originRot = tf.rotation;
        OnInit();
        GetData();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(originPos, TF.position) > attackRange)
        {
            OnDespawn();
        }
    }

    public void OnInit()
    {
        originPos = TF.position;
        //TF.rotation = originRot;
        rb.velocity = TF.forward * attackSpeed;
    }

    public void OnDespawn()
    {
        BasePool.Despawn(this);
    }

    public void GetData()
    {
        weaponSkin.material = weaponData.WeaponMaterial(skinIndex);
        weaponType = weaponData.weaponType;
        attackRange = weaponData.attackRange;
        attackSpeed = weaponData.attackSpeed;
        //tf.rotation = originRot;    
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
        if (other.CompareTag(Constants.TAG_BOT) || other.CompareTag(Constants.TAG_PLAYER))
        {
            OnHitCharacter.Invoke();
            Character character = Cache.GenCharacter(other);
            character.OnDeath();
        }
    }

    public void AddCurrentCharacterListener(Character character)
    {
        OnHitCharacter.AddListener(character.RemoveTarget);
    }
}
