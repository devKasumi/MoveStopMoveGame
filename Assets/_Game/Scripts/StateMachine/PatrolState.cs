using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    public void OnEnter(Bot bot)
    {
        bot.ChangeAnimation(Constants.ANIMATION_RUN);
        bot.SetDestination(bot.RandomMovePos());
    }

    public void OnExecute(Bot bot)
    {
        if (bot.IsReachTarget())
        {
            bot.SetDestination(bot.RandomMovePos());
        }
    }

    public void OnExit(Bot bot)
    {

    }
}
