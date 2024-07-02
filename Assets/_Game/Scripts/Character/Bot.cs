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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public bool IsReachTarget()
    {
        return Vector3.Distance(transform.position, currentTargetPosition) < 0.8f;
    }

    public void SetDestination(Vector3 pos)
    {
        currentTargetPosition = pos;
        navMeshAgent.destination = pos;
    }

    public Vector3 RandomMovePos() => LevelManager.Instance.CurrentLevel().Platform.RandomMovePos();
}
