using System.Collections;
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
        isResetJoystick = false;
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        background.gameObject.SetActive(true);
        base.OnPointerDown(eventData);
        StartCoroutine(ChangeMoveAnim());
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        isResetJoystick = true;
        background.gameObject.SetActive(false);
        base.OnPointerUp(eventData);
        LevelManager.Instance.Player.ChangeAnimation(Constants.ANIMATION_IDLE);
    }

    public bool IsResetJoystick
    {
        get => isResetJoystick;
        set => isResetJoystick = value;
    }

    public IEnumerator ChangeMoveAnim()
    {
        yield return new WaitForSeconds(0.1f);
        LevelManager.Instance.Player.ChangeAnimation(Constants.ANIMATION_RUN);
    }

    public void OnResetJoyStick()
    {
        OnPointerUp(null);
    }
}