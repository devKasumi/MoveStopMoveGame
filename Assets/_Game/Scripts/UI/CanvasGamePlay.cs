using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasGamePlay : UICanvas
{
    [SerializeField] private TextMeshProUGUI totalBot;


    private void Update()
    {
        UpdateBotActive(LevelManager.Instance.CurrentLevel().currentTotalActiveBot);
    }

    public override void Setup()
    {
        base.Setup();
    }

    public void UpdateBotActive(int botCount)
    {
        totalBot.text = botCount.ToString();
    }

    public void SettingsButton()
    {
        UIManager.Instance.OpenUI<CanvasSettings>().SetState(this);
        LevelManager.Instance.indicatorCam.gameObject.SetActive(false);
        GameManager.Instance.UpdateGameState(GameState.Setting);
    }
}
