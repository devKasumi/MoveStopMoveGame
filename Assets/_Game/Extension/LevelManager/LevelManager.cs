using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private Character player;
    [SerializeField] private PoolControl poolControl;
    [SerializeField] private Level[] levels;
    [SerializeField] public Camera indicatorCam;

    private Level currentLevel;
    private int currentLevelIndex;

    private void Awake()
    {
        currentLevel = levels[0];
    }

    public void OnInit()
    {
        
    }

    public Level CurrentLevel() => currentLevel;

    public PoolControl PoolControl => poolControl;  

    public Character Player => player;

}
