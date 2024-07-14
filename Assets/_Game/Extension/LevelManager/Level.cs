using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Platform levelPlatform;
    [SerializeField] private int totalBot;

    private int currentActiveBot = 12;
    private int botThreshold = 5;

    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentActiveBot == botThreshold)
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

}
