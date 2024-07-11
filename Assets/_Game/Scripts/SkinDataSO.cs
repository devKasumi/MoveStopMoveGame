using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Skin Data", fileName = "Skin Data")]

public class SkinDataSO : ScriptableObject
{
    [SerializeField] private List<Material> materials = new List<Material>();   

    public Material SkinMaterial(CommonEnum.ColorType colorType) => materials[(int)colorType];

    public List<Material> Materials => materials;
}
