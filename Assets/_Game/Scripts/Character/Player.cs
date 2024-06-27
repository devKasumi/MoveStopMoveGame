using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform spawnPoint;

    private Vector3 moveDirection;
    private float inputX;
    private float inputZ;

    private int attackCount = 0;
    float frameRate = 1f;
    float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        time += Time.deltaTime;

        if (joystick.IsResetJoystick() && ListTarget().Count > 0 && attackCount == 0)
        {
            if (time >= frameRate)
            {
                time = 0;
                StartCoroutine(Attack());
                attackCount++;
            }
        }
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

        attackCount = 0;
    }

    public IEnumerator Attack()
    {
        Debug.LogError("attacking!!!  " + attackCount);
        yield return new WaitForSeconds(0.1f);
        Weapon weapon = WeaponPool.Spawn<Weapon>(Weapon.WeaponType, spawnPoint.transform.position, Weapon.TF.rotation);
    }
}
