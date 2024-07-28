using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;

public class Character : GameUnit
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private CapsuleCollider capsuleCollider;
    [SerializeField] private Weapon weapon;
    [SerializeField] private SkinnedMeshRenderer skinColor;
    [SerializeField] private SkinnedMeshRenderer pantMaterial;
    [SerializeField] private SkinDataSO skinDataSO;
    [SerializeField] private PantDataSO pantDataSO;
    [SerializeField] private Canvas attackArea;

    private List<Character> listTargets = new List<Character>();
    public string currentAnimationName;
    [SerializeField] private List<GameObject> weaponImages = new List<GameObject>();
    [SerializeField] private GameObject weaponImage;

    public bool isCharacterDeath;
    //private GameObject currentWeaponImage;
    //public List<bool> bools = new List<bool>();

    public Weapon Weapon
    {
        get => weapon;
        set => weapon = value;
    }

    public Rigidbody Rb => rb;

    public virtual void OnInit()
    {
        attackArea.gameObject.SetActive(true);
        capsuleCollider.enabled = true;
        currentAnimationName = Constants.ANIMATION_IDLE;
    }

    public virtual void OnDespawn()
    {

    }

    public void UpdateWeaponImage()
    {
        Cache.GenMeshFilter(weaponImage).mesh = Cache.GenMeshFilter(Weapon.WeaponSkin.gameObject).sharedMesh;
        Cache.GenMeshRenderer(weaponImage).materials = Weapon.WeaponSkin.sharedMaterials;
    }

    public bool IsCharacterDeath() => !capsuleCollider.enabled;

    public SkinDataSO SkinDataSO => skinDataSO;

    public PantDataSO PantDataSO => pantDataSO;

    public SkinnedMeshRenderer SkinColor
    {
        get => skinColor;
        set => skinColor = value;
    }

    public SkinnedMeshRenderer PantMaterial
    {
        get => pantMaterial;
        set => pantMaterial = value;
    }

    public Transform SpawnPoint() => spawnPoint;

    public List<Character> ListTarget() => listTargets;

    public float MoveSpeed() => moveSpeed;

    public void AddTarget(Character character)
    {
        listTargets.Add(character);
        //Attack();
    }

    public void RemoveTarget()
    {
        if (listTargets.Count > 0)
        {
            listTargets.Remove(listTargets[0]);
        }
    }

    public void CheckEnemyCurrentStatus()
    {
        if (listTargets.Count > 0)
        {
            if (listTargets[0].IsCharacterDeath())
            {
                RemoveTarget();
            }
        }
    }

    public void OnDeath()
    {
        // change anim
        ChangeAnimation(Constants.ANIMATION_DEAD);

        // remove collider
        attackArea.gameObject.SetActive(false);
        capsuleCollider.enabled = false;

        // destroy char after
        StartCoroutine(CharacterDie());

        //if (this is Bot)
        //{
        //    StartCoroutine(LevelManager.Instance.PoolControl.StartSpawnBot());
        //}
    }

    public void Attack()
    {
        GameUnit gameUnit = BasePool.Spawn<GameUnit>(this.weapon.PoolType, spawnPoint.position, Quaternion.identity);
        Weapon weapon = (Weapon)gameUnit;
        weapon.AddCurrentCharacterListener(this);
        //if (this is Bot)
        //{
        //    weapon.WeaponSkin.material = Cache.GenMeshRenderer(weaponImage).material;
        //}
        weapon.WeaponSkin.material = Cache.GenMeshRenderer(weaponImage).material;
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
        //animator.ResetTrigger(currentAnimationName);
        //currentAnimationName = animationName;
        //animator.SetTrigger(currentAnimationName);
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
        if (this is Bot)
        {
            yield return new WaitForSeconds(1f);
            BasePool.Despawn(this);
        }
        else if (this is Player)
        {
            // TODO add logic player die
            ChangeAnimation(Constants.ANIMATION_DEAD);
            isCharacterDeath = true;
            rb.isKinematic = true;
        }
    }

    
}
