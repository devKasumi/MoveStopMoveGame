using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
//using UnityEngine.UIElements;
//using UnityEngine.UIElements;

public class Bot : Character
{
    [SerializeField] public CommonEnum.BotType botType;
    [SerializeField] private Image targetImage;
    [SerializeField] private NavMeshAgent navMeshAgent;

    [SerializeField] public Texture Icon = null;
    [SerializeField] public Texture Arrow = null;
    [SerializeField] public Color markerColor = Color.white;
    

    private IState currentState;
    private Vector3 currentTargetPosition;

    private float frameRate = 1.2f;
    private float time = 0;

    void Start()
    {
        OnInit();
    }

    void Update()
    {
        if (!GameManager.Instance.CurrentState(GameState.GamePlay))
        {
            SetDestination(TF.position);
            ChangeState(new IdleState());
            return;
        }

        time += Time.deltaTime;

        if (LevelManager.Instance.Player.isCharacterDeath)
        {
            ChangeState(new IdleState());
        }

        if (currentState != null)
        {
            currentState.OnExecute(this);
        }

        CheckEnemyCurrentStatus();
    }

    public override void OnInit()
    {
        base.OnInit();
        
        

        ChangeState(new PatrolState());

        SkinColor.material = SkinDataSO.Materials[Random.Range(1, SkinDataSO.Materials.Count)];
        PantMaterial.material = PantDataSO.Materials[Random.Range(0, PantDataSO.Materials.Count)];
        markerColor = SkinColor.material.color;
        OffscreenMarkersCameraScript instance = LevelManager.Instance.indicatorCam.GetComponent<OffscreenMarkersCameraScript>();
        if (instance)
        {
            instance.Register(this);
        }


        DisableTarget();
    }

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public void EnableTarget()
    {
        targetImage.gameObject.SetActive(true);
    }

    public void DisableTarget()
    {
        targetImage.gameObject.SetActive(false);
    }

    public bool IsReachTarget() => Vector3.Distance(transform.position, currentTargetPosition) < 1.3f;

    public void SetDestination(Vector3 pos)
    {
        currentTargetPosition = pos;
        navMeshAgent.destination = pos;
    }

    public Vector3 RandomMovePos() => LevelManager.Instance.CurrentLevel().Platform.RandomMovePos();

    public void AttackEnemy()
    {
        

        if (time >= frameRate)
        {
            time = 0;
            if (ListTarget().Count > 0)
            {
                FaceEnemy();
                ChangeAnimation(Constants.ANIMATION_ATTACK);
                Attack();
            }
        }
    }
}
