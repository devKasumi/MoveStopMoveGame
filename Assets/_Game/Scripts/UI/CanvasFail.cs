using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasFail : UICanvas
{
    //[SerializeField] private TextMeshProUGUI scoreText;

    public void SetBestScore(int score)
    {
        //scoreText.text = score.ToString();
    }

    public void RetryButton()
    {
        UIManager.Instance.CloseAll();
        //LevelManager.Instance.OnRetryLevel();
        UIManager.Instance.OpenUI<CanvasMainMenu>();
        GameManager.Instance.UpdateGameState(GameState.MainMenu);
    }
}
