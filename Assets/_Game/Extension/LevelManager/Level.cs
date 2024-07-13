using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Platform levelPlatform;

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
}
