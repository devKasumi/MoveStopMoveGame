using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private Character[] characters;
    [SerializeField] private PoolControl poolControl;
    [SerializeField] private Platform levelPlatform;
    private Level currentLevel;
    private int currentLevelIndex;  

    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInit()
    {
        List<Weapon> listWeapons = poolControl.ListWeapon();
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].Weapon = listWeapons[Random.Range(0, listWeapons.Count)];
            poolControl.PreLoadPool(characters[i]);
        }
    }

    public Level CurrentLevel() => currentLevel;

    public Vector3 RandomPos() => levelPlatform.RandomMovePos();
}
