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

    private Player player;

    private int xAxisPivot;
    private int zAxisPivot;

    public bool IsFirstQuadrant => xAxisPivot <= player.TF.position.x && zAxisPivot <= player.TF.position.z;
    public bool IsSecondQuadrant => xAxisPivot >= player.TF.position.x && zAxisPivot <= player.TF.position.z;
    public bool IsThirdQuadrant => xAxisPivot >= player.TF.position.x && zAxisPivot >= player.TF.position.z;
    public bool IsFourthQuadrant => xAxisPivot <= player.TF.position.x && zAxisPivot >= player.TF.position.z;

    [SerializeField] private float[] multiplier;

    private List<Vector3> firstQuadrantPos = new List<Vector3>();
    private List<Vector3> secondQuadrantPos = new List<Vector3>();
    private List<Vector3> thirdQuadrantPos = new List<Vector3>();   
    private List<Vector3> fourthQuadrantPos = new List<Vector3>();

    private List<List<Vector3>> listPos = new List<List<Vector3>>();

    void Start()
    {
        OnInit();
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

        firstQuadrantPos.Add(new Vector3(maxX * 0.25f, 0f, maxZ * 0.5f));
        firstQuadrantPos.Add(new Vector3(maxX * 0.5f, 0f, maxZ * 0.5f));
        firstQuadrantPos.Add(new Vector3(maxX * 0.5f, 0f, maxZ * 0.25f));
        secondQuadrantPos.Add(new Vector3(-maxX * 0.25f, 0f, maxZ * 0.5f));
        secondQuadrantPos.Add(new Vector3(-maxX * 0.5f, 0f, maxZ * 0.5f));
        secondQuadrantPos.Add(new Vector3(-maxX * 0.5f, 0f, maxZ * 0.25f));
        thirdQuadrantPos.Add(new Vector3(-maxX * 0.25f, 0f, -maxZ * 0.5f));
        thirdQuadrantPos.Add(new Vector3(-maxX * 0.5f, 0f, -maxZ * 0.5f));
        thirdQuadrantPos.Add(new Vector3(-maxX * 0.5f, 0f, -maxZ * 0.25f));
        fourthQuadrantPos.Add(new Vector3(maxX * 0.25f, 0f, -maxZ * 0.5f));
        fourthQuadrantPos.Add(new Vector3(maxX * 0.5f, 0f, -maxZ * 0.5f));
        fourthQuadrantPos.Add(new Vector3(maxX * 0.5f, 0f, -maxZ * 0.25f));

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

    //public List<Vector3> FirstQuadrantPos => firstQuadrantPos;
    //public List<Vector3> SecondQuadrantPos => secondQuadrantPos;
    //public List<Vector3> ThirdQuadrantPos => thirdQuadrantPos;
    //public List<Vector3> FourthQuadrantPos => fourthQuadrantPos;

    public List<List<Vector3>> ListPos => listPos;  

    //public Vector3 RandomSpawnPos(int quadrant)
    //{
    //    switch (quadrant)
    //    {
    //        case 1:
    //            break;
    //        case 2:
    //            break;
    //        case 3:
    //            break;
    //        case 4:
    //            break;
    //        default:
    //            break;
    //    }
    //}
}
