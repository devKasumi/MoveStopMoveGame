using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class PoolControl : MonoBehaviour
{
    private List<Weapon> weapons = new List<Weapon>();
    private List<Character> bots = new List<Character>();

    private void Awake()
    {
        bots = Resources.LoadAll<Character>("Pool/Bot/").ToList<Character>();
        weapons = Resources.LoadAll<Weapon>("Pool/Weapon/").ToList<Weapon>();
    }

    private void Start()
    {
        
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
        //SpawnBot(bots[index], index);
    }

    public void PreLoadWeaponPool(Character character)
    {
        Weapon weapon;
        weapon = weapons.Contains(character.Weapon) ? weapons[weapons.IndexOf(character.Weapon)] 
                                                    : weapons[(int)CommonEnum.WeaponType.Hammer_0];
        weapon.WeaponSkin.material = weapon.WeaponData.WeaponMaterial(0);
        GameObject pool = new GameObject(character.name + "_" + weapon.name);
        BasePool<Weapon>.PreLoad(weapon, (int)weapon.WeaponType, 4, pool.transform);
    }

    public void SpawnBot(Character bot, int index)
    {
        List<List<Vector3>> listPos = LevelManager.Instance.CurrentLevel().Platform.ListPos;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < listPos.Count; j++)
            {
                bot.Weapon = weapons[Random.Range(0, weapons.Count)];
                BasePool<Character>.Spawn(bot, index, listPos[j][i], Quaternion.identity);
                PreLoadWeaponPool(bot);
            } 
        }
    }
}
