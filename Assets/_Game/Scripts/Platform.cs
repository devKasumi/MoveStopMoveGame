using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private int minX;
    [SerializeField] private int minZ;
    [SerializeField] private int maxX;
    [SerializeField] private int maxZ;
    [SerializeField] private float[] multiplier;

    private Character player;

    private int xAxisPivot;
    private int zAxisPivot;

    public bool IsFirstQuadrant => xAxisPivot <= player.TF.position.x && zAxisPivot <= player.TF.position.z;
    public bool IsSecondQuadrant => xAxisPivot >= player.TF.position.x && zAxisPivot <= player.TF.position.z;
    public bool IsThirdQuadrant => xAxisPivot >= player.TF.position.x && zAxisPivot >= player.TF.position.z;
    public bool IsFourthQuadrant => xAxisPivot <= player.TF.position.x && zAxisPivot >= player.TF.position.z;


    public List<Vector3> firstQuadrantPos = new List<Vector3>();
    public List<Vector3> secondQuadrantPos = new List<Vector3>();
    public List<Vector3> thirdQuadrantPos = new List<Vector3>();   
    public List<Vector3> fourthQuadrantPos = new List<Vector3>();

    private List<List<Vector3>> listPos = new List<List<Vector3>>();

    private void Awake()
    {
        OnInit();
    }

    void Start()
    {
        player = LevelManager.Instance.Player;
    }

    public void OnInit()
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

        firstQuadrantPos.Add(new Vector3(maxX * multiplier[0], 0f, maxZ * multiplier[1] + 9));
        firstQuadrantPos.Add(new Vector3(maxX * multiplier[1] + 9, 0f, maxZ * multiplier[1] + 9));
        firstQuadrantPos.Add(new Vector3(maxX * multiplier[1] + 9, 0f, maxZ * multiplier[0]));
        secondQuadrantPos.Add(new Vector3(-maxX * multiplier[0], 0f, maxZ * multiplier[1] + 9));
        secondQuadrantPos.Add(new Vector3(-maxX * multiplier[1] - 9, 0f, maxZ * multiplier[1] + 9));
        secondQuadrantPos.Add(new Vector3(-maxX * multiplier[1] - 9, 0f, maxZ * multiplier[0]));
        thirdQuadrantPos.Add(new Vector3(-maxX * multiplier[0], 0f, -maxZ * multiplier[1] - 9));
        thirdQuadrantPos.Add(new Vector3(-maxX * multiplier[1] - 9, 0f, -maxZ * multiplier[1] - 9));
        thirdQuadrantPos.Add(new Vector3(-maxX * multiplier[1] - 9, 0f, -maxZ * multiplier[0]));
        fourthQuadrantPos.Add(new Vector3(maxX * multiplier[0], 0f, -maxZ * multiplier[1] - 9));
        fourthQuadrantPos.Add(new Vector3(maxX * multiplier[1] + 9, 0f, -maxZ * multiplier[1] - 9));
        fourthQuadrantPos.Add(new Vector3(maxX * multiplier[1] + 9, 0f, -maxZ * multiplier[0]));

        listPos.Add(firstQuadrantPos);
        listPos.Add(secondQuadrantPos);
        listPos.Add(thirdQuadrantPos);
        listPos.Add(fourthQuadrantPos);
    }

    public Vector3 RandomMovePos()
    {
        Vector3 randomMovePos = new Vector3(maxX * Random.Range(-1, 2) * multiplier[Random.Range(0, 3)], 
                                            0f, 
                                            maxZ * Random.Range(-1, 2) * multiplier[Random.Range(0, 3)]);
        return randomMovePos;
    }

    public List<List<Vector3>> ListPos => listPos;  

}
