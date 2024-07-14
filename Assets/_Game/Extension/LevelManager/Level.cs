using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Platform levelPlatform;
    [SerializeField] private int totalBot;

    private int currentActiveBot = 0;

    // Start is called before the first frame update
    void Start()
    {
        //TotalBot = totalShortRangeBot + totalLongRangeBot;
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInit()
    {
        LevelManager.Instance.PoolControl.OnInit();
        LevelManager.Instance.PoolControl.SpawnBotAtBeginning();
    }

    public Platform Platform => levelPlatform;

    //public int TotalBot => totalBot;

    public int CurrentActiveBot
    {
        get => currentActiveBot;
        set => currentActiveBot = value;
    }

    public bool IsEnoughBot() => CurrentActiveBot == totalBot;

}
