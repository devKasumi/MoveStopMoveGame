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

    //private void Awake()
    //{
        
    //}

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
        //poolControl.PreLoadWeaponPool();
        poolControl.OnInit();
        poolControl.SpawnBot();
    }

    public Level CurrentLevel() => currentLevel;

    //public Vector3 RandomPos() => levelPlatform.RandomMovePos();
}
