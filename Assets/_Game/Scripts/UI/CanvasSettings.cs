using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasSettings : UICanvas
{
    //[SerializeField] private GameObject[] buttons;
    [SerializeField] private GameObject mainMenuButton;
    [SerializeField] private GameObject continueButton;
    //[SerializeField] private GameObject closeButton;
    [SerializeField] private GameObject soundButton;
    [SerializeField] private GameObject vibrationButton;
    [SerializeField] private TextMeshProUGUI offSoundText;
    [SerializeField] private TextMeshProUGUI offVibrationText;
    private Vector3 offEffectPos = new Vector3(-50, 0, 0);
    private Vector3 onEffectPos = new Vector3(50, 0, 0);

    private void Start()
    {
        UpdateSoundUI();    
        UpdateVibrationUI();
    }

    public void SetState(UICanvas canvas)
    {
        mainMenuButton.SetActive(false);
        continueButton.SetActive(false);
        //closeButton.SetActive(false);

        //if (canvas is CanvasMainMenu)
        //{
        //    closeButton.SetActive(true);
        //}
        if (canvas is CanvasGamePlay)
        {
            mainMenuButton.gameObject.SetActive(true);
            continueButton.gameObject.SetActive(true);
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
            Cache.GenImage(soundButton).color = Color.green;
            Cache.GenRectTransform(soundButton).localPosition = onEffectPos;
            offSoundText.gameObject.SetActive(false);
        }
        else
        {
            Cache.GenImage(soundButton).color = Color.white;
            Cache.GenRectTransform(soundButton).localPosition = offEffectPos;
            offSoundText.gameObject.SetActive(true);
        }
    }

    public void OnVibrationButtonPressed()
    {
        SoundManager.Instance.IsVibrationOn = !SoundManager.Instance.IsVibrationOn;
        UpdateVibrationUI();
        InventoryManager.Instance.SaveDataToJsonFile();
    }

    public void UpdateVibrationUI()
    {
        if (SoundManager.Instance.IsVibrationOn)
        {
            Cache.GenImage(vibrationButton).color = Color.green;
            Cache.GenRectTransform(vibrationButton).localPosition = onEffectPos;
            offVibrationText.gameObject.SetActive(false);
        }
        else
        {
            Cache.GenImage(vibrationButton).color = Color.white;
            Cache.GenRectTransform(vibrationButton).localPosition = offEffectPos;
            offVibrationText.gameObject.SetActive(true);
        }
    }

    public void MainMenuButton()
    {
        Close(0);
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<CanvasMainMenu>();
        GameManager.Instance.UpdateGameState(GameState.MainMenu);
    }

    public void ContinueButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasGamePlay>();
        GameManager.Instance.UpdateGameState(GameState.GamePlay);
        LevelManager.Instance.CurrentLevel().OnPlay();
    }

    public void CloseButton()
    {
        Close(0);
        GameManager.Instance.UpdateGameState(GameState.MainMenu);
    }
}
