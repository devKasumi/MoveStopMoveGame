using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TAG_BOT))
        {
            Character character = Cache.GenCharacter(other);
            Bot bot = (Bot)character;
            bot.EnableTarget();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constants.TAG_BOT))
        {
            Character character = Cache.GenCharacter(other);
            Bot bot = (Bot)character;
            bot.DisableTarget();
        }
    }
}
