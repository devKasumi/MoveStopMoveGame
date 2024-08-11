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
        //UpdateCoin(0);
    }

    public void UpdateBotActive(int botCount)
    {
        //coinText.text = coin.ToString();
        totalBot.text = botCount.ToString();
    }

    public void SettingsButton()
    {
        UIManager.Instance.OpenUI<CanvasSettings>().SetState(this);
        GameManager.Instance.UpdateGameState(GameState.Setting);
    }
}
