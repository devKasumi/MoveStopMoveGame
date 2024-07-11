using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class PoolControl : MonoBehaviour
{
    private List<Weapon> weapons = new List<Weapon>();
    private List<Character> bots = new List<Character>();
    [SerializeField] private Player player;

    private void Awake()
    {
        bots = Resources.LoadAll<Character>("Pool/Bot/").ToList<Character>();
        weapons = Resources.LoadAll<Weapon>("Pool/Weapon/").ToList<Weapon>();
    }

    private void Start()
    {
        //OnInit();
        //SpawnBot();
        //PreLoadWeaponPool(player);
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

    public void PreLoadWeaponPool(Character character, int weaponIndex)
    {
        //Weapon weapon;
        //weapon = weapons.Contains(character.Weapon) ? weapons[weapons.IndexOf(character.Weapon)] 
        //                                            : weapons[(int)CommonEnum.WeaponType.Hammer_0];
        //Debug.LogError(weapons[weapons.IndexOf(character.Weapon)] + "   with:   " /*+ (int)weapon.WeaponType*/);
        Weapon weaponPrefab = Instantiate(weapons.Contains(character.Weapon) ? weapons[weapons.IndexOf(character.Weapon)]
                                                                             : weapons[(int)CommonEnum.WeaponType.Hammer_0]);
        weaponPrefab.WeaponSkin.material = weaponPrefab.WeaponData.WeaponMaterial(0);
        weaponPrefab.GetData();
        GameObject pool = new GameObject(character.name + "_" + weaponPrefab.name);
        pool.transform.parent = character.TF;
        BasePool<Weapon>.PreLoad(weaponPrefab, /*(int)weaponPrefab.WeaponType*/weaponIndex, 4, pool.transform);
        //weaponPrefab.gameObject.SetActive(false);
    }

    public void SpawnBot()
    {
        List<List<Vector3>> listPos = LevelManager.Instance.CurrentLevel().Platform.ListPos;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < listPos.Count; j++)
            {
                int index = Random.Range(0, bots.Count);
                int weaponIndex = Random.Range(0, weapons.Count);
                //bots[index].Weapon = weapons[Random.Range(0, weapons.Count)];
                Character bot = BasePool<Character>.Spawn(index, listPos[j][i], Quaternion.identity);
                bot.Weapon = weapons[Random.Range(0, weapons.Count)];
                bot.Weapon.GetData();
                //int weaponIndex = (int)bot.Weapon.WeaponType;
                //Debug.LogError(bot.Weapon.WeaponType);
                PreLoadWeaponPool(bot, weaponIndex);
            } 
        }
    }
}
