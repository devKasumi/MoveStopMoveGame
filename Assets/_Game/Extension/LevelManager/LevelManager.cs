using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private Character player;
    [SerializeField] private PoolControl poolControl;
    [SerializeField] private Level[] levels;


    //private List<Character> characters = new List<Character>();
    private Level currentLevel;
    private int currentLevelIndex;  

    // Start is called before the first frame update
    void Start()
    {
        //characters.Add(player);
        currentLevel = levels[0];
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInit()
    {
        //for (int i = 0; i < currentLevel.TotalShortRangeBot; i++)
        //{
        //    poolControl.PreLoadBotPool();
        //}

        poolControl.OnInit();
        
        
        poolControl.PreLoadWeaponPool(player);
    }

    public Level CurrentLevel() => currentLevel;

    //public Vector3 RandomPos() => levelPlatform.RandomMovePos();
}
