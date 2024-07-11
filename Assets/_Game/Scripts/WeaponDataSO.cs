using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Weapon Data", fileName = "Weapon Data")]

public class WeaponDataSO : ScriptableObject
{
    [SerializeField] private List<Material> materials = new List<Material>();

    [SerializeField] public CommonEnum.WeaponType weaponType;
    [SerializeField] public float attackSpeed;
    [SerializeField] public float attackRange;

    public Material WeaponMaterial(int skinIndex) => materials[skinIndex];
}
