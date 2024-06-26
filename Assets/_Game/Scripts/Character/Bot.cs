using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bot : Character
{
    [SerializeField] private Image targetImage;

    private IState currentState;

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

    //public Image TargetImage() => targetImage;
    public void EnableTarget()
    {
        targetImage.gameObject.SetActive(true);
    }

    public void DisableTarget()
    {
        targetImage.gameObject.SetActive(false);
    }
}
