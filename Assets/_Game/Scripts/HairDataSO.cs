using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Hair Data", fileName = "Hair Data")]

public class HairDataSO : ScriptableObject
{
    [SerializeField] private List<Material> materials = new List<Material>();

    
}
