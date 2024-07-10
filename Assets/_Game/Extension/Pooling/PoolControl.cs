using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolControl : MonoBehaviour
{
    private List<Weapon> weapons = new List<Weapon>();
    private List<Character> bots = new List<Character>();
    //[SerializeField] private Weapon weapon;

    private void Awake()
    {
        bots = Resources.LoadAll<Character>("Pool/Bot/").ToList<Character>();
        weapons = Resources.LoadAll<Weapon>("Pool/Weapon/").ToList<Weapon>();
    }

    public void PreLoadBotPool()
    {
        bots[0].Weapon = weapons[Random.Range(0, weapons.Count)];
        GameObject pool = new GameObject(bots[0].name + "_Pool");
        pool.transform.position = Vector3.up;
        BasePool<Character>.PreLoad(bots[0], (int)bots[0].Weapon.WeaponType, 4, pool.transform);
        PreLoadWeaponPool(bots[0]);
    }

    public void PreLoadWeaponPool(Character character)
    {
        Weapon weapon;
        weapon = weapons.Contains(character.Weapon) ? weapons[weapons.IndexOf(character.Weapon)] : null;
        GameObject pool = new GameObject(character.name + "_" + weapon.name);
        BasePool<Weapon>.PreLoad(weapon, (int)weapon.WeaponType, 4, pool.transform);
    }

    public List<Weapon> ListWeapon() => weapons;
}
