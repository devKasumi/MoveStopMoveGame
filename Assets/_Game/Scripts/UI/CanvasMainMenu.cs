using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMainMenu : UICanvas
{
    [SerializeField] private GameObject vibrateOff;
    [SerializeField] private GameObject vibrateOn;

    [SerializeField] private GameObject soundOff;
    [SerializeField] private GameObject soundOn;

    private void Start()
    {
        UpdateSoundUI();
        UpdateVibrationUI();
    }

    public void PlayButton()
    {
        Close(0);
        LevelManager.Instance.Player.OnInit();
        UIManager.Instance.floatingJoystick.gameObject.SetActive(true);
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
        UIManager.Instance.floatingJoystick.gameObject.SetActive(false);
        UIManager.Instance.CanvasWeapon.gameObject.SetActive(true);
    }

    public void OnSkinButtonPressed()
    {
        Close(0);
        UIManager.Instance.floatingJoystick.gameObject.SetActive(false);
        UIManager.Instance.OpenUI<CanvasUISkin>();
    }

    public void OnAdsButtonPressed()
    {

    }

    public void OnVibrateButtonPressed()
    {
        SoundManager.Instance.IsVibrationOn = !SoundManager.Instance.IsVibrationOn;
        UpdateVibrationUI();
        InventoryManager.Instance.SaveDataToJsonFile();
    }

    public void UpdateVibrationUI()
    {
        if (SoundManager.Instance.IsVibrationOn)
        {
            vibrateOff.SetActive(false);
            vibrateOn.SetActive(true);
            SoundManager.Instance.IsVibrationOn = true;
        }
        else
        {
            vibrateOn.SetActive(false);
            vibrateOff.SetActive(true);
            SoundManager.Instance.IsVibrationOn = false;
        }
    }

    public void OnSoundButtonPressed()
    {
        SoundManager.Instance.IsSoundOn = !SoundManager.Instance.IsSoundOn;
        UpdateSoundUI();
        InventoryManager.Instance.SaveDataToJsonFile();
    }

    public void UpdateSoundUI()
    {
        if (SoundManager.Instance.IsSoundOn)
        {
            soundOff.SetActive(false);
            soundOn.SetActive(true);
            SoundManager.Instance.IsSoundOn = true;
        }
        else
        {
            soundOn.SetActive(false);
            soundOff.SetActive(true);
            SoundManager.Instance.IsSoundOn = false;
        }
    }

}
