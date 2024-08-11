using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMainMenu : UICanvas
{
    [SerializeField] private GameObject vibrateOff;
    [SerializeField] private GameObject vibrateOn;

    [SerializeField] private GameObject soundOff;
    [SerializeField] private GameObject soundOn;


    public void PlayButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasGamePlay>();
        GameManager.Instance.UpdateGameState(GameState.GamePlay);
        LevelManager.Instance.CurrentLevel().OnPlay();
    }

    public void SettingsButton()
    {
        UIManager.Instance.OpenUI<CanvasSettings>().SetState(this);
        GameManager.Instance.UpdateGameState(GameState.Setting);
    }

    public void OnWeaponButtonPressed()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasUIWeapon>();
    }

    public void OnSkinButtonPressed()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasUISkin>();
    }

    public void OnAdsButtonPressed()
    {

    }

    public void OnVibrateButtonPressed()
    {
        if (vibrateOff.activeSelf)
        {
            vibrateOff.SetActive(false);
            vibrateOn.SetActive(true);
        }
        else
        {
            vibrateOn.SetActive(false);
            vibrateOff.SetActive(true);
        }
    }

    public void OnSoundButtonPressed()
    {
        if (soundOff.activeSelf)
        {
            soundOff.SetActive(false);
            soundOn.SetActive(true);
        }
        else
        {
            soundOn.SetActive(false);
            soundOff.SetActive(true);
        }
    }

}
