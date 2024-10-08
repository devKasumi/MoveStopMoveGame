using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasMainMenu : UICanvas
{
    [SerializeField] private TextMeshProUGUI coin;

    [SerializeField] private GameObject vibrateOff;
    [SerializeField] private GameObject vibrateOn;

    [SerializeField] private GameObject soundOff;
    [SerializeField] private GameObject soundOn;

    private Vector3 UISkinOffset = new Vector3(0, 4, -11);

    private void Start()
    {
        UpdateMainMenuUI();
    }

    private void Update()
    {
        UpdateMainMenuUI();
    }

    public void UpdatePlayerCoin(int playerCoin)
    {
        coin.text = playerCoin.ToString();
    }

    public void PlayButton()
    {
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<CanvasGamePlay>();
        UIManager.Instance.floatingJoystick.gameObject.SetActive(true);
        GameManager.Instance.UpdateGameState(GameState.GamePlay);
        LevelManager.Instance.CurrentLevel().OnPlay();
        LevelManager.Instance.indicatorCam.gameObject.SetActive(true);
    }

    public void SettingsButton()
    {
        UIManager.Instance.OpenUI<CanvasSettings>().SetState(this);
        GameManager.Instance.UpdateGameState(GameState.Setting);
        LevelManager.Instance.indicatorCam.gameObject.SetActive(false);
    }

    public void OnWeaponButtonPressed()
    {
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<CanvasUIWeapon>();
        UIManager.Instance.floatingJoystick.gameObject.SetActive(false);
        LevelManager.Instance.indicatorCam.gameObject.SetActive(false);
    }

    public void OnSkinButtonPressed()
    {
        UIManager.Instance.CloseAll();
        Cache.GenCameraFollow(UIManager.Instance.mainCamera).CameraOffset = UISkinOffset;   
        LevelManager.Instance.Player.TF.rotation = Quaternion.Euler(0, 174, 0);
        UIManager.Instance.floatingJoystick.gameObject.SetActive(false);
        UIManager.Instance.OpenUI<CanvasUISkin>();
        LevelManager.Instance.indicatorCam.gameObject.SetActive(false);
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

    public void UpdateMainMenuUI()
    {
        UpdatePlayerCoin(InventoryManager.Instance.PlayerCoin);
        UpdateSoundUI();
        UpdateVibrationUI();
    }

}
