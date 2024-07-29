using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Weapon Data", fileName = "Weapon Data")]

public class WeaponDataSO : ScriptableObject
{
    [SerializeField] private List<Material> defaultMaterials = new List<Material>();
    [SerializeField] private List<Material> materials = new List<Material>();

    [SerializeField] public CommonEnum.WeaponType weaponType;
    [SerializeField] public float attackSpeed;
    [SerializeField] public float attackRange;

    public Material FullSetWeaponMaterial() => materials[Random.Range(0, materials.Count)];

    public Material[] PlayerDefaultWeaponMaterial()
    {
        Material[] materials = new Material[2];
        materials[0] = defaultMaterials[0];
        materials[1] = defaultMaterials[1];
        return materials;
    }

    public Material PlayerWeaponFirstSet => materials[0];

    public Material PlayerWeaponSecondSet() 
    {
        //if (weaponType == CommonEnum.WeaponType.Axe_0) return null;
        return materials[1];
    }
}
