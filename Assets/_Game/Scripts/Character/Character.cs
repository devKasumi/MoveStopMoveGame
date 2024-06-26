using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform bulletPoint;
    [SerializeField] private Transform tf;

    public Transform TF
    {
        get => tf;
        set => tf = value;
    }
    private List<Vector3> listTargets = new List<Vector3>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float MoveSpeed() => moveSpeed;

    public void AddTarget(Character character)
    {
        listTargets.Add(character.TF.position);
    }

    public void RemoveTarget(Character character)
    {
        listTargets.Remove(character.TF.position);
    }
}
