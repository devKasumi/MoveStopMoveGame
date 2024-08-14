using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private Character player;
    [SerializeField] private PoolControl poolControl;
    [SerializeField] private Level[] levels;
    [SerializeField] public Camera indicatorCam;

    private Level currentLevel;
    private int currentLevelIndex;

    private void Awake()
    {
        currentLevel = levels[0];
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Vector2 camPos = UIManager.Instance.mainCamera.WorldToScreenPoint(UIManager.Instance.mainCamera.transform.position);
        //    Vector2 playerPos = UIManager.Instance.mainCamera.WorldToScreenPoint(player.transform.position);
        //    Vector2 botPos = Vector2.zero;
        //    //Bot bot = new Bot();
        //    if (player.ListTarget().Count > 0)
        //    {
        //        //bot = (Bot)player.ListTarget()[0];
        //        botPos = player.ListTarget()[0].transform.position;
        //    }
        //    //Vector2 preBot = bot.transform.position;

        //    Debug.LogError(camPos + "  =>  " + playerPos + "   ||=>  " + botPos);
        //}
    }

    public void OnInit()
    {
        
    }

    public Level CurrentLevel() => currentLevel;

    public PoolControl PoolControl => poolControl;  

    public Character Player => player;

}
