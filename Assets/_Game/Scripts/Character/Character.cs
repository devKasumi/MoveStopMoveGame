using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    //[SerializeField] private Transform bulletPoint;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform tf;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private CapsuleCollider capsuleCollider;

    [SerializeField] private Weapon weapon;
    private List<Character> listTargets = new List<Character>();
    private string currentAnimationName = Constants.ANIMATION_IDLE;

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
        Debug.LogError("add target!!!");
        listTargets.Add(character);
        //listTargets.Push(character);
    }

    //public void RemoveTarget(Character character)
    //{
    //    Debug.LogError("remove target!!!");
    //    listTargets.Remove(character.TF.position);
    //}

    public void RemoveTarget()
    {
        //Debug.LogError("count:  " + listTargets.Count);
        if (listTargets.Count > 0)
        {
            Debug.LogError("remove target!!!");
            listTargets.Remove(listTargets[0]);
        }
    }

    public void OnDeath()
    {
        // change anim

        // remove collider
        capsuleCollider.enabled = false;

        // destroy char after
        if (listTargets.Count > 0)
        {
            StartCoroutine(CharacterDie());
        }
    }

    public void InitList()
    {
        listTargets = new List<Character>();
    }

    public void ChangeAnimation(string animationName)
    {
        if (currentAnimationName != animationName)
        {
            animator.ResetTrigger(currentAnimationName);
            currentAnimationName = animationName;
            animator.SetTrigger(currentAnimationName);
        }
    }

    public IEnumerator CharacterDie()
    {
        yield return new WaitForSeconds(1f);
        Debug.LogError("destroy bot!!!!");
        Destroy(gameObject);
    }
}
