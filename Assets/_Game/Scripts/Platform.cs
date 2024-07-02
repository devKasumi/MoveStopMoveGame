using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Platform : MonoBehaviour
{
    //[SerializeField] private float xAxisPivot;
    //[SerializeField] private float zAxisPivot;
    [SerializeField] private int minX;
    [SerializeField] private int minZ;
    [SerializeField] private int maxX;
    [SerializeField] private int maxZ;

    private Player player;

    private int xAxisPivot;
    private int zAxisPivot;

    public bool IsFirstQuadrant => xAxisPivot <= player.TF.position.x && zAxisPivot <= player.TF.position.z;
    public bool IsSecondQuadrant => xAxisPivot >= player.TF.position.x && zAxisPivot <= player.TF.position.z;
    public bool IsThirdQuadrant => xAxisPivot >= player.TF.position.x && zAxisPivot >= player.TF.position.z;
    public bool IsFourthQuadrant => xAxisPivot <= player.TF.position.x && zAxisPivot >= player.TF.position.z;

    //private Vector3 randomMovePos;

    [SerializeField] private float[] multiplier;
    [SerializeField] private GameObject cube;

    // Start is called before the first frame update
    void Start()
    {
        if (minX >= 0 && minZ >= 0)
        {
            xAxisPivot = (maxX - minX) / 2;
            zAxisPivot = (maxZ - minZ) / 2;
        }
        else
        {
            xAxisPivot = 0;
            zAxisPivot = 0;
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Instantiate(cube, RandomMovePos(), Quaternion.identity);
        }
        //Instantiate(cube, RandomMovePos(), Quaternion.identity);
    }

    public Vector3 RandomMovePos()
    {
        //int axisValue = Random.Range(-1, 1);
        //float randomMultiplier = multiplier[Random.Range(0, 3)];
        Vector3 randomMovePos = new Vector3(maxX * Random.Range(-1, 2) * multiplier[Random.Range(0, 3)], 
                                            0f, 
                                            maxZ * Random.Range(-1, 2) * multiplier[Random.Range(0, 3)]);
        Debug.LogError(randomMovePos);
        return randomMovePos;
    }
}
