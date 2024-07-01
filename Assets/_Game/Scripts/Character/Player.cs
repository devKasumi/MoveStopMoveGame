using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private Rigidbody rb;
    

    private Vector3 moveDirection;
    private float inputX;
    private float inputZ;

    private float frameRate = 1f;
    private float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();

        time += Time.deltaTime;

        if (time >= frameRate)
        {
            time = 0;
            if (ListTarget().Count > 0 && joystick.IsResetJoystick())
            {
                //StartCoroutine(Attack());
                Debug.LogError("attack!!!!  " + ListTarget().Count);
                Attack();
            }
        }

        //Debug.LogError("count:  " + ListTarget().Count);
    }

    public void Move()
    {
        inputX = joystick.Horizontal;
        inputZ = joystick.Vertical;

        moveDirection = new Vector3(inputX * MoveSpeed(), 0f, inputZ * MoveSpeed());    

        rb.velocity = moveDirection;

        if (inputX != 0f && inputZ != 0f)
        {
            TF.rotation = Quaternion.LookRotation(rb.velocity);
        }
    }

    public void Attack()
    {
        //Debug.LogError("attacking!!!  " + attackCount);
        //yield return new WaitForSeconds(0.1f);
        Weapon weapon = WeaponPool.Spawn<Weapon>(Weapon.WeaponType, SpawnPoint().transform.position, Weapon.TF.rotation);
        weapon.AddCurrentCharacterListener(this);
        if (ListTarget().Count > 0)
        {
            Vector3 direction = ListTarget()[0].TF.position - SpawnPoint().transform.position;
            weapon.transform.forward = direction;
        }
    }
}
