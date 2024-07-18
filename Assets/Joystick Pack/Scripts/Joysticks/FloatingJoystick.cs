﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FloatingJoystick : Joystick
{
    private bool isResetJoystick = true;

    protected override void Start()
    {
        base.Start();
        background.gameObject.SetActive(false);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        //LevelManager.Instance.Player.ChangeAnimation(Constants.ANIMATION_RUN);
        isResetJoystick = false;
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        background.gameObject.SetActive(true);
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        LevelManager.Instance.Player.ChangeAnimation(Constants.ANIMATION_IDLE);
        //Player player = (Player)LevelManager.Instance.Player;
        //player.AttackEnemy();
        isResetJoystick = true;
        background.gameObject.SetActive(false);
        base.OnPointerUp(eventData);
    }

    public bool IsResetJoystick() => isResetJoystick;
}