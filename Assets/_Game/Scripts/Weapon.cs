using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    //[SerializeField] private MeshRenderer meshRenderer;
    //[SerializeField] private CommonEnum.ColorType colorType;
    [SerializeField] private DataSO skinData;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private MeshRenderer weaponSkin;
    [SerializeField] private UnityEvent OnHitCharacter = new UnityEvent();

    private CommonEnum.WeaponType weaponType;
    private float attackRange;
    private float attackSpeed;
    //private Image weaponSkin;
    private int skinIndex = 0;

    private Transform tf;

    private Vector3 originPos;

    private void Awake()
    {
        //PoolType = PoolType.WeaponPool;
    }

    // Start is called before the first frame update
    void Start()
    {
        //OnInit();
        
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
        rb.velocity = TF.forward * attackSpeed;
    }

    public void OnDespawn()
    {
        //WeaponPool.Despawn(this);
        BasePool<Weapon>.Despawn(this, (int)weaponType);
    }

    public CommonEnum.WeaponType WeaponType => weaponType;

    public float AttackRange => attackRange;

    public float AttackSpeed => attackSpeed;

    public void ChangeWeaponSkin()
    {

    }

    //public void ChangeColor(CommonEnum.ColorType color)
    //{
    //    colorType = color;
    //}

    //public void ChangeMaterial(Material material)
    //{
    //    meshRenderer.material = material;   
    //}

    public Transform TF
    {
        get
        {
            if (!tf)
            {
                tf = transform;
            }

            return tf;
        }
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
            //character.OnDeath();
        }
    }

    public void AddCurrentCharacterListener(Character character)
    {
        OnHitCharacter.AddListener(character.RemoveTarget);
    }
}
