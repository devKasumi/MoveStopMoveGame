using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasFail : UICanvas
{

    public void RetryButton()
    {
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<CanvasMainMenu>();
        LevelManager.Instance.Player.OnInit();
        LevelManager.Instance.CurrentLevel().OnInit();
        GameManager.Instance.UpdateGameState(GameState.MainMenu);
    }
}
