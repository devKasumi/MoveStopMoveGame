using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class PoolControl : MonoBehaviour
{
    private List<GameUnit> weapons = new List<GameUnit>();
    private List<GameUnit> bots = new List<GameUnit>();
    [SerializeField] private Player player;

    private int TotalPosEachQuadrant = 3;
    private int offset = 4;

    private void Awake()
    {
        bots = Resources.LoadAll<GameUnit>("Pool/Bot/").ToList<GameUnit>();
        weapons = Resources.LoadAll<GameUnit>("Pool/Weapon/").ToList<GameUnit>();
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
        for (int i = 0; i < weapons.Count; i++)
        {
            PreLoadWeaponPool(i);
        }
    }

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
        bot.UpdateWeaponImage();
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

    public Weapon PlayerDefaultWeapon => (Weapon)weapons[(int)CommonEnum.WeaponType.Hammer_0];
}

public enum PoolType
{
    Axe_0 = 0,
    Axe_1 = 1,
    Candy_0 = 2,
    Candy_1 = 3,
    Candy_2 = 4,
    Candy_4 = 5,
    Hammer_0 = 6,
    Knife_0 = 7,
    Bot_0 = 8,
    Bot_1 = 9,
}
