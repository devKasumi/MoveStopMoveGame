using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Platform levelPlatform;
    //[SerializeField] private int totalShortRangeBot;
    //[SerializeField] private int totalLongRangeBot;
    //private int TotalBot;

    // Start is called before the first frame update
    void Start()
    {
        //TotalBot = totalShortRangeBot + totalLongRangeBot;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Platform Platform => levelPlatform;

    //public Bot Bot => bot;

    //public int CurrentLevelTotalBot => this.TotalBot;

    //public int TotalShortRangeBot => this.TotalShortRangeBot;

    //public int TotalLongRangeBot => this.TotalLongRangeBot;
}
