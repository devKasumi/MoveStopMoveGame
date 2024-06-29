using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform bulletPoint;
    [SerializeField] private Transform tf;
    [SerializeField] private Transform spawnPoint;

    private Weapon weapon;
    private List<Character> listTargets = new List<Character>();

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

    public List<Character> ListTarget() => listTargets;

    public float MoveSpeed() => moveSpeed;

    public Transform SpawnPoint() => spawnPoint;

    public void AddTarget(Character character)
    {
        listTargets.Add(character);
    }

    //public void RemoveTarget(Character character)
    //{
    //    Debug.LogError("remove target!!!");
    //    listTargets.Remove(character.TF.position);
    //}

    public void RemoveTarget()
    {
        //Debug.LogError("remove 1ts target!");
        if (listTargets.Count > 0)
        {
            listTargets.Remove(listTargets[0]);
        }
    }
}
