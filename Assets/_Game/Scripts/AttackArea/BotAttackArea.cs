using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAttackArea : MonoBehaviour
{
    [SerializeField] private Character bot;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TAG_BOT) || other.CompareTag(Constants.TAG_PLAYER))
        {
            bot.AddTarget(Cache.GenCharacter(other));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
