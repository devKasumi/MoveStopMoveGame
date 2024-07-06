using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolControl : MonoBehaviour
{
    private List<Weapon> weapons = new List<Weapon>();
    //[SerializeField] private Weapon weapon;

    private void Awake()
    {
        weapons = Resources.LoadAll<Weapon>("Pool/").ToList<Weapon>();
    }

    public void PreLoadPool(Character character)
    {
        Weapon weapon;
        if (weapons.Contains(character.Weapon))
        {
            weapon = weapons[weapons.IndexOf(character.Weapon)];
        }
        else
        {
            weapon = null;
        }
        GameObject pool = new GameObject(character.name + "_" + weapon.name);
        //WeaponPool.PreLoad(weapon, 4, pool.transform);
        BasePool<Weapon>.PreLoad(weapon, 4, pool.transform);
    }

    public List<Weapon> ListWeapon() => weapons;
}
