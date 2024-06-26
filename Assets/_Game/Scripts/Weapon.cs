using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    private float attackRange;
    private float attackSpeed;
    private Image weaponSkin;
    private CommonEnum.WeaponType weaponType;

    private Transform tf;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public CommonEnum.WeaponType WeaponType() => weaponType;

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
}
