using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackArea : MonoBehaviour
{
    [SerializeField] private Character player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TAG_BOT))
        {
            Character character = Cache.GenCharacter(other);
            Bot bot = (Bot)character;
            bot.EnableTarget();
            bot.ChangeState(new IdleState());
            player.AddTarget(bot);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constants.TAG_BOT))
        {
            Character character = Cache.GenCharacter(other);
            Bot bot = (Bot)character;
            bot.DisableTarget();
            bot.ChangeState(new PatrolState());
            player.RemoveTarget();
        }
    }
}
