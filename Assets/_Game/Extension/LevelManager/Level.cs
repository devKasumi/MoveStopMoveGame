using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Platform levelPlatform;
    [SerializeField] private int totalBot;
    [SerializeField] private int botThreshold = 5;
    [SerializeField] public int BotActiveCount = 0;

    public int currentActiveBot = 12;
    public int currentTotalActiveBot = 60;

    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentActiveBot <= botThreshold && BotActiveCount < totalBot)
        {
            LevelManager.Instance.PoolControl.ReSpawnBot();
        }
    }

    public void OnInit()
    {
        BotActiveCount = 0;
        LevelManager.Instance.PoolControl.OnInit();
        LevelManager.Instance.PoolControl.SpawnBotAtBeginning();
        currentTotalActiveBot = totalBot;
        currentActiveBot = LevelManager.Instance.PoolControl.ListActiveBots.Count; 
        UIManager.Instance.ResetCamera();
        //LevelManager.Instance.PoolControl.ActiveBot(false);
        //LevelManager.Instance.PoolControl.ActiveIndicator(false);
        LevelManager.Instance.indicatorCam.gameObject.SetActive(false);
    }

    public Platform Platform => levelPlatform;

    public int CurrentActiveBot
    {
        get => currentActiveBot;
        set => currentActiveBot = value;
    }

    public bool IsEnoughBot() => CurrentActiveBot == totalBot;

    public void OnPlay()
    {
        for (int i = 0; i < LevelManager.Instance.PoolControl.ListActiveBots.Count; i++)
        {
            if (LevelManager.Instance.PoolControl.ListActiveBots[i].gameObject.activeInHierarchy)
            {
                LevelManager.Instance.PoolControl.ListActiveBots[i].ChangeState(new PatrolState());
            }
        }
    }

}
