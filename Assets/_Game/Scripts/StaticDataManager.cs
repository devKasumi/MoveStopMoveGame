using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticDataManager : Singleton<StaticDataManager>
{
    [SerializeField] private List<WeaponDataSO> weaponDataSOs = new List<WeaponDataSO>();
    [SerializeField] private PantDataSO pantDataSO;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<WeaponDataSO> ListWeaponDataSO => weaponDataSOs;

    public PantDataSO PantDataSO => pantDataSO;
}
