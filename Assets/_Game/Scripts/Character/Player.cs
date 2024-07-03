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

        AttackEnemy();

        if (Input.GetKeyDown(KeyCode.C))
        {
            // bao loi vi thang player inherit monobehavior
            Debug.LogError((int)JsonFileHandler.ReadFromJson<DataFromJson>(Constants.JSON_FILE_NAME).weapon.WeaponType);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // TODO: xep lai logic vao UI chon weapon va skin!!!
            JsonFileHandler.SaveToJson<Character>(this, Constants.JSON_FILE_NAME);
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
    }

    public void AttackEnemy()
    {
        time += Time.deltaTime;

        if (time >= frameRate)
        {
            time = 0;
            if (ListTarget().Count > 0 && joystick.IsResetJoystick())
            {
                Debug.LogError("attack!!!!  " + ListTarget().Count);
                Attack();
            }
        }
    }
}

[System.Serializable]
public class DataFromJson
{
    public Weapon weapon;
    // them 1 meshrender cho skin nua 
}
