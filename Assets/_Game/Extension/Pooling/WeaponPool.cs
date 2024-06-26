using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class WeaponPool
{
    private static Dictionary<CommonEnum.WeaponType, Pool> poolInstance = new Dictionary<CommonEnum.WeaponType, Pool>();

    public static void PreLoad(Weapon weaponPrefab, int amount, Transform parent)
    {
        if (!weaponPrefab)
        {
            Debug.LogError("PREFAB IS EMPTY!");
            return;
        }

        if (!poolInstance.ContainsKey(weaponPrefab.WeaponType()) || poolInstance[weaponPrefab.WeaponType()] == null)
        {
            Pool p = new Pool();    
            p.PreLoad(weaponPrefab, amount, parent);
            poolInstance[weaponPrefab.WeaponType()] = p;
        }
    }

    public static T Spawn<T>(CommonEnum.WeaponType weaponType, Vector3 pos, Quaternion rot) where T : Weapon
    {
        if (!poolInstance.ContainsKey(weaponType))
        {
            Debug.LogError(weaponType + " IS NOT PRELOAD!");
            return null;
        }

        return poolInstance[weaponType].Spawn(pos, rot) as T;
    }

    public static void Despawn(Weapon weapon)
    {
        if (!poolInstance.ContainsKey(weapon.WeaponType()))
        {
            Debug.LogError(weapon.WeaponType() + " IS NOT PRELOAD!");
        }

        poolInstance[weapon.WeaponType()].Despawn(weapon);
    }

    public static void Collect(CommonEnum.WeaponType weaponType)
    {
        if (!poolInstance.ContainsKey(weaponType))
        {
            Debug.LogError(weaponType + " IS NOT PRELOAD!");
        }

        poolInstance[weaponType].Collect();
    }

    public static void CollectAll()
    {
        foreach(var item in poolInstance.Values)
        {
            item.Collect();
        }
    }

    public static void Release(CommonEnum.WeaponType weaponType)
    {
        if (!poolInstance.ContainsKey(weaponType))
        {
            Debug.LogError(weaponType + " IS NOT PRELOAD!");
        }

        poolInstance[weaponType].Release();
    }

    public static void ReleaseAll()
    {
        foreach (var item in poolInstance.Values)
        {
            item.Release();
        }
    }
}

public class Pool
{
    Transform parent;
    Weapon weaponPrefab;

    Stack<Weapon> inactiveWeapons = new Stack<Weapon>();

    List<Weapon> activeWeapons = new List<Weapon>();    

    public void PreLoad(Weapon weaponPrefab, int amount, Transform parent)
    {
        this.parent = parent;
        this.weaponPrefab = weaponPrefab;

        for (int i = 0; i < amount; i++)
        {
            Despawn(GameObject.Instantiate(weaponPrefab, parent));
        }
    }

    public Weapon Spawn(Vector3 pos, Quaternion rot)
    {
        Weapon weapon;

        if (inactiveWeapons.Count <= 0)
        {
            weapon = GameObject.Instantiate(weaponPrefab, parent);
        }
        else
        {
            weapon = inactiveWeapons.Peek();
            inactiveWeapons.Pop();
        }

        weapon.TF.SetPositionAndRotation(pos, rot);
        activeWeapons.Add(weapon);
        weapon.gameObject.SetActive(true);

        return weapon;  
    }

    public void Despawn(Weapon weapon)
    {
        if (weapon != null && weapon.gameObject.activeSelf)
        {
            activeWeapons.Remove(weapon);
            inactiveWeapons.Push(weapon);
            weapon.gameObject.SetActive(false);
        }
    }

    public void Collect()
    {
        while (activeWeapons.Count > 0)
        {
            Despawn(activeWeapons[0]);
        }
    }

    public void Release()
    {
        Collect();

        while (inactiveWeapons.Count > 0)
        {
            GameObject.Destroy(inactiveWeapons.Peek().gameObject);
            inactiveWeapons.Pop();
        }
        inactiveWeapons.Clear();
    }
}
