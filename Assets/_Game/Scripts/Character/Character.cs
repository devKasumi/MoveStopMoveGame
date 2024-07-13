using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Character : GameUnit
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private CapsuleCollider capsuleCollider;
    [SerializeField] private Weapon weapon;
    [SerializeField] private SkinnedMeshRenderer skinColor;
    [SerializeField] private SkinnedMeshRenderer pantMaterial;
    [SerializeField] private SkinDataSO skinDataSO;
    [SerializeField] private PantDataSO pantDataSO;

    private List<Character> listTargets = new List<Character>();
    private string currentAnimationName;
    [SerializeField] private List<GameObject> weaponImages = new List<GameObject>();
    [SerializeField] private GameObject weaponImage;

    //private GameObject currentWeaponImage;

    public Weapon Weapon
    {
        get => weapon;
        set => weapon = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        
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

    public void UpdateWeaponImage()
    {
        weaponImage.GetComponent<MeshFilter>().mesh = Weapon.WeaponSkin.gameObject.GetComponent<MeshFilter>().sharedMesh;
        weaponImage.GetComponent<MeshRenderer>().materials = Weapon.WeaponSkin.sharedMaterials;
    }

    public SkinDataSO SkinDataSO => skinDataSO;

    public PantDataSO PantDataSO => pantDataSO;

    public SkinnedMeshRenderer SkinColor => skinColor;

    public SkinnedMeshRenderer PantMaterial => pantMaterial;

    public Transform SpawnPoint() => spawnPoint;

    public List<Character> ListTarget() => listTargets;

    public float MoveSpeed() => moveSpeed;

    public void AddTarget(Character character)
    {
        //Debug.LogError("add target!!!");
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
        GameUnit gameUnit = BasePool.Spawn<GameUnit>(this.weapon.PoolType, spawnPoint.position, Quaternion.identity);
        Weapon weapon = (Weapon)gameUnit;
        weapon.AddCurrentCharacterListener(this);
        if (listTargets.Count > 0)
        {
            Vector3 direction = listTargets[0].TF.position - spawnPoint.position;
            weapon.TF.forward = direction;
            weapon.OnInit();
            weapon.GetData();
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

    public void FaceEnemy()
    {
        if (listTargets.Count > 0)
        {
            Vector3 direction = listTargets[0].TF.position - TF.position;
            TF.forward = direction;
            spawnPoint.transform.rotation = TF.rotation;
        }
    }

    public IEnumerator CharacterDie()
    {
        yield return new WaitForSeconds(1f);
        if (this is Bot)
        {
            BasePool.Despawn(this);
        }
        else if (this is Player)
        {
            // TODO add logic player die
        }
    }
}
