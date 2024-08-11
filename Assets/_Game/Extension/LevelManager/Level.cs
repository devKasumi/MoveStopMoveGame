using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Platform levelPlatform;
    [SerializeField] private int totalBot;
    [SerializeField] private int botThreshold = 5;
    [SerializeField] public int BotActiveCount = 0;

    private int currentActiveBot = 12;

    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentActiveBot == botThreshold && BotActiveCount < totalBot)
        {
            LevelManager.Instance.PoolControl.ReSpawnBot();
        }
    }

    public void OnInit()
    {
        LevelManager.Instance.PoolControl.OnInit();
        LevelManager.Instance.PoolControl.SpawnBotAtBeginning();
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
            if (LevelManager.Instance.PoolControl.ListActiveBots[i] != null)
            {
                LevelManager.Instance.PoolControl.ListActiveBots[i].ChangeState(new PatrolState());
            }
        }
    }

}
