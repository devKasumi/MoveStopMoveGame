using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasVictory : UICanvas
{
    //[SerializeField] private TextMeshProUGUI scoreText;

    public void SetBestScore(int score)
    {
        //scoreText.text = score.ToString();
    }

    public void NextButton()
    {
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<CanvasMainMenu>();
        //LevelManager.GetInstance.OnLoadNextLevel();
        GameManager.Instance.UpdateGameState(GameState.MainMenu);
    }

    public void RetryButton()
    {
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<CanvasMainMenu>();
        //LevelManager.GetInstance.OnRetryLevel();
        GameManager.Instance.UpdateGameState(GameState.MainMenu);
    }
}
