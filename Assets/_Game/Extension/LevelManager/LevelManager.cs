using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private Character player;
    [SerializeField] private PoolControl poolControl;
    [SerializeField] private Level[] levels;

    private Level currentLevel;
    private int currentLevelIndex;

    private void Awake()
    {
        currentLevel = levels[0];
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnInit()
    {
        
    }

    public Level CurrentLevel() => currentLevel;

    public PoolControl PoolControl => poolControl;  

    public Character Player => player;

}
