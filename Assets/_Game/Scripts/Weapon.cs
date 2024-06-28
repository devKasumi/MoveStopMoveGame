using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [SerializeField] private CommonEnum.WeaponType weaponType;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackSpeed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] public UnityEvent OnHitCharacter;


    private Image weaponSkin;

    private Transform tf;

    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        //tf.Translate(tf.forward * attackSpeed * Time.deltaTime);
    }

    public void OnInit()
    {
        rb.velocity = transform.forward * 5f;
        //Invoke(nameof(OnDespawn), 1f);
    }

    public void OnDespawn()
    {
        WeaponPool.Despawn(this);
    }

    public CommonEnum.WeaponType WeaponType => weaponType;

    public float AttackRange => attackRange;

    public float AttackSpeed => attackSpeed;

    public void ChangeWeaponSkin()
    {

    }

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

    public void RemoveCharacterTarget(Character character)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TAG_BOT))
        {
            //StartCoroutine(DespawnWeapon());
            //WeaponPool.Despawn(this);
            OnHitCharacter.Invoke();
        }
    }

    public void DespawnWeapon()
    {
        //yield return new WaitForSeconds(0.5f);
        WeaponPool.Despawn(this);
        //Destroy(this.gameObject);
    }
}
