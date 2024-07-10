using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

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

    private void Start()
    {
        //OnInit();
    }

    public void OnInit()
    {
        for (int i = 0; i < bots.Count; i++)
        {
            PreLoadBotPool(i);
        }
    }

    public void PreLoadBotPool(int index)
    {
        GameObject pool = new GameObject(bots[index].name + "_Pool");
        pool.transform.position = Vector3.up;
        BasePool<Character>.PreLoad(bots[index], index, 10, pool.transform);
    }

    public void PreLoadWeaponPool(Character character)
    {
        Weapon weapon;
        weapon = weapons.Contains(character.Weapon) ? weapons[weapons.IndexOf(character.Weapon)] 
                                                    : weapons[(int)CommonEnum.WeaponType.Hammer_0];
        GameObject pool = new GameObject(character.name + "_" + weapon.name);
        BasePool<Weapon>.PreLoad(weapon, (int)weapon.WeaponType, 4, pool.transform);
    }

    public List<Weapon> ListWeapon() => weapons;
}
