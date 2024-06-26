using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private Rigidbody rb;

    private Vector3 moveDirection;
    private float inputX;
    private float inputZ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        inputX = joystick.Horizontal;
        inputZ = joystick.Vertical;

        moveDirection = new Vector3(inputX * MoveSpeed(), 0f, inputZ * MoveSpeed());    

        rb.velocity = moveDirection;
        TF.rotation = Quaternion.LookRotation(rb.velocity);
    }


}
