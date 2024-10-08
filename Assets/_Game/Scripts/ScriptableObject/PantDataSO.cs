using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Pant Data", fileName = "Pant Data")]

public class PantDataSO : ScriptableObject
{
    [SerializeField] private List<Material> materials = new List<Material>();

    public Material PantMaterial(CommonEnum.PantType pantType) => materials[(int)pantType];

    public List<Material> Materials => materials;
}
