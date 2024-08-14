using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PoolControl : MonoBehaviour
{
    [SerializeField] private List<GameUnit> weapons = new List<GameUnit>();
    private List<GameUnit> bots = new List<GameUnit>();
    private List<Bot> listActiveBots = new List<Bot>(); 

    private int TotalPosEachQuadrant = 3;

    private void Awake()
    {
        bots = Resources.LoadAll<GameUnit>("Pool/Bot/").ToList<GameUnit>();
    }

    private void Start()
    {
        
    }

    public void OnInit()
    {
        if (listActiveBots.Count != 0)
        {
            for (int i = 0; i < listActiveBots.Count; i++)
            {
                BasePool.Despawn(listActiveBots[i]);
            }
            listActiveBots.Clear();
        }
        for (int i = 0; i < bots.Count; i++)
        {
            PreLoadBotPool(i);
        }
        for (int i = 0; i < weapons.Count; i++)
        {
            PreLoadWeaponPool(i);
        }
    }

    public List<Bot> ListActiveBots => listActiveBots;

    public void PreLoadBotPool(int index)
    {
        GameObject pool = new GameObject(bots[index].name + "_Pool");
        pool.transform.position = Vector3.up;
        BasePool.PreLoad(bots[index], 10, pool.transform);
    }

    public void PreLoadWeaponPool(int index)
    {
        GameObject pool = new GameObject(weapons[index].name + "_Pool");
        BasePool.PreLoad(weapons[index], 4, pool.transform);
    }

    public void SpawnBot(Vector3 pos)
    {
        LevelManager.Instance.CurrentLevel().BotActiveCount++;
        int index = Random.Range(0, bots.Count);
        GameUnit gameUnit = BasePool.Spawn<GameUnit>(bots[index].PoolType, pos, Quaternion.identity);
        Bot bot = (Bot)gameUnit;
        bot.OnInit();
        bot.Weapon = (Weapon)weapons[Random.Range(0, weapons.Count)];
        bot.Weapon.BotWeaponData();
        bot.UpdateWeaponImage();
        listActiveBots.Add(bot);
    }

    public void SpawnBotAtBeginning()
    {
        List<List<Vector3>> listPos = LevelManager.Instance.CurrentLevel().Platform.ListPos;
        for (int i = 0; i < TotalPosEachQuadrant; i++)
        {
            for (int j = 0; j < listPos.Count; j++)
            {
                SpawnBot(listPos[j][i]);
            } 
        }
    }

    public void ReSpawnBot()
    {
        bool playerOnFirstQuadrant = LevelManager.Instance.CurrentLevel().Platform.IsFirstQuadrant;
        bool playerOnSecondQuadrand = LevelManager.Instance.CurrentLevel().Platform.IsSecondQuadrant;
        bool playerOnThirdQuadrant = LevelManager.Instance.CurrentLevel().Platform.IsThirdQuadrant;
        bool playerOnFourthQuadrant = LevelManager.Instance.CurrentLevel().Platform.IsFourthQuadrant;

        int activeQuadrantIndex = playerOnFirstQuadrant ? 0 :
                                  playerOnSecondQuadrand ? 1 :
                                  playerOnThirdQuadrant ? 2 :
                                  playerOnFourthQuadrant ? 3 : -1;

        List<Vector3> listPos = new List<Vector3>();
        List<List<Vector3>> listQuadrantPos = LevelManager.Instance.CurrentLevel().Platform.ListPos;

        for (int i = 0; i < listQuadrantPos.Count; i++)
        {
            if (i == activeQuadrantIndex) continue;
            listPos.Add(listQuadrantPos[i][0]);
            listPos.Add(listQuadrantPos[i][Random.Range(1,3)]); 
        }

        for (int i = 0; i< listPos.Count; i++)
        {
            LevelManager.Instance.CurrentLevel().CurrentActiveBot++;
            SpawnBot(listPos[i]);
        }
    }

    public Weapon InitPlayerWeapon(int index)
    {
        Weapon weapon = Instantiate((Weapon)weapons[index], Vector3.down, Quaternion.identity);
        weapon.PlayerWeaponData();
        GameObject pool = new GameObject("PlayerWeapon_Pool");
        BasePool.PreLoad(weapon, 4, pool.transform);
        return weapon;
    }

    public Weapon SetPlayerWeapon(int index)
    {
        Weapon weapon = Instantiate((Weapon)weapons[index], Vector3.down, Quaternion.identity);
        weapon.PlayerWeaponData();
        return weapon;
    }

    public List<GameUnit> ListWeaponPrefabs => weapons;
}

public enum PoolType
{
    Hammer_0 = 0,
    Axe_0 = 1,
    Axe_1 = 2,
    Candy_0 = 3,
    Candy_1 = 4,
    Candy_2 = 5,
    Candy_4 = 6,
    Knife_0 = 7,
    Player_Weapon = 8,
    Bot_0 = 9,
    Bot_1 = 10,
}
