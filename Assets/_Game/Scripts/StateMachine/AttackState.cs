using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    public void OnEnter(Bot bot)
    {
        bot.ChangeAnimation(Constants.ANIMATION_IDLE);
    }

    public void OnExecute(Bot bot)
    {
        bot.SetDestination(bot.TF.position);
        bot.ChangeAnimation(Constants.ANIMATION_IDLE);
        bot.AttackEnemy();
    }

    public void OnExit(Bot bot)
    {

    }
}
