using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform bulletPoint;
    [SerializeField] private Transform tf;

    private Weapon weapon;
    private List<Vector3> listTargets = new List<Vector3>();

    public Transform TF
    {
        get => tf;
        set => tf = value;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Weapon Weapon
    {
        get => weapon;
        set => weapon = value;
    }

    public List<Vector3> ListTarget() => listTargets;

    public float MoveSpeed() => moveSpeed;

    public void AddTarget(Character character)
    {
        listTargets.Add(character.TF.position);
    }

    public void RemoveTarget(Character character)
    {
        Debug.LogError("remove target!!!");
        listTargets.Remove(character.TF.position);
    }
}
