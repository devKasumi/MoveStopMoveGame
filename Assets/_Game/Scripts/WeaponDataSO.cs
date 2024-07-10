using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Weapon Data", fileName = "Weapon Data")]

public class WeaponDataSO : ScriptableObject
{
    [SerializeField] private List<Material> materials = new List<Material>();
    //[SerializeField] public List<Material> Hammer_0_Mats = new List<Material>();
    //[SerializeField] private List<Material> Axe_0_Mats = new List<Material>();
    //[SerializeField] private List<Material> Axe_1_Mats = new List<Material>();
    //[SerializeField] private List<Material> Candy_0_Mats = new List<Material>();
    //[SerializeField] private List<Material> Candy_1_Mats = new List<Material>();
    //[SerializeField] private List<Material> Candy_2_Mats = new List<Material>();
    //[SerializeField] private List<Material> Candy_4_Mats = new List<Material>();
    //[SerializeField] private List<Material> Knife_0_Mats = new List<Material>();

    [SerializeField] private CommonEnum.WeaponType weaponType;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackRange;

    public Material WeaponMaterial(int skinIndex) => materials[skinIndex];
}
