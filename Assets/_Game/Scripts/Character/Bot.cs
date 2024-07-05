using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Bot : Character
{
    [SerializeField] private Image targetImage;
    [SerializeField] private NavMeshAgent navMeshAgent;

    private IState currentState;
    private Vector3 currentTargetPosition;

    private float frameRate = 1f;
    private float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }

        //if (ListTarget().Count > 0)
        //{
        //    ChangeState(new AttackState());
        //}
        //else ChangeState(new PatrolState());
    }

    public override void OnInit()
    {
        base.OnInit();

        ChangeState(new PatrolState());
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
        time += Time.deltaTime;

        if (time >= frameRate)
        {
            time = 0;
            if (ListTarget().Count > 0)
            {
                Attack();
            }
        }
    }
}
