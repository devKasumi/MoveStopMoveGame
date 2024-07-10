using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Character : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform tf;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private CapsuleCollider capsuleCollider;
    [SerializeField] private Weapon weapon;
    [SerializeField] private SkinnedMeshRenderer skinColor;
    [SerializeField] private SkinnedMeshRenderer pantColor;

    private List<Character> listTargets = new List<Character>();
    private string currentAnimationName;

    public Transform TF
    {
        get => tf;
        set => tf = value;
    }

    public Weapon Weapon
    {
        get => weapon;
        set => weapon = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        //PoolType = PoolType.CharacterPool;
    }

    // Update is called once per frame
    void Update()
    {
        spawnPoint.transform.rotation = transform.rotation; 
    }

    public virtual void OnInit()
    {

    }

    public virtual void OnDespawn()
    {

    }

    public Transform SpawnPoint() => spawnPoint;

    public List<Character> ListTarget() => listTargets;

    public float MoveSpeed() => moveSpeed;

    public void AddTarget(Character character)
    {
        Debug.LogError("add target!!!");
        listTargets.Add(character);
        //listTargets.Push(character);
    }

    public void RemoveTarget()
    {
        //Debug.LogError("count:  " + listTargets.Count);
        if (listTargets.Count > 0)
        {
            //Debug.LogError("remove target!!!");
            listTargets.Remove(listTargets[0]);
        }
    }

    public void OnDeath()
    {
        // change anim

        // remove collider
        capsuleCollider.enabled = false;

        // destroy char after
        StartCoroutine(CharacterDie());
    }

    public void Attack()
    {
        //Weapon weapon = WeaponPool.Spawn<Weapon>(this.weapon.WeaponType, spawnPoint.position, Quaternion.identity);
        Weapon weapon = BasePool<Weapon>.Spawn(this.weapon, (int)this.weapon.WeaponType, spawnPoint.position, Quaternion.identity);
        weapon.AddCurrentCharacterListener(this);
        if (listTargets.Count > 0)
        {
            Vector3 direction = listTargets[0].TF.position - spawnPoint.position;
            weapon.TF.forward = direction;
            weapon.OnInit();
        }
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
        Destroy(gameObject);
    }
}
