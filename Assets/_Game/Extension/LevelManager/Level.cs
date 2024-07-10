using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Platform levelPlatform;
    [SerializeField] private int TotalBot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Platform Platform => levelPlatform;

    //public Bot Bot => bot;

    public int LevelTotalBot => this.TotalBot;
}
