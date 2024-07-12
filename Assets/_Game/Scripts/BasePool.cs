using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BasePool
{
    private static Dictionary<PoolType, Pool> poolInstance = new Dictionary<PoolType, Pool>();

    public static void PreLoad(GameUnit objectPrefab, int amount, Transform parent)
    {
        if (!objectPrefab)
        {
            Debug.LogError("PREFAB IS EMPTY!");
            return;
        }

        if (!poolInstance.ContainsKey(objectPrefab.PoolType) || poolInstance[objectPrefab.PoolType] == null)
        {
            Pool p = new Pool();
            p.PreLoad(objectPrefab, amount, parent);
            poolInstance[objectPrefab.PoolType] = p;
        }
    }

    public static GameUnit Spawn<T>(PoolType poolType, Vector3 pos, Quaternion rot) where T : GameUnit
    {
        if (!poolInstance.ContainsKey(poolType))
        {
            Debug.LogError(" IS NOT PRELOAD!");
            return null;
        }

        return poolInstance[poolType].Spawn(pos, rot) as T;
    }

    public static void Despawn(GameUnit objectPrefab)
    {
        if (!poolInstance.ContainsKey(objectPrefab.PoolType))
        {
            Debug.LogError(objectPrefab + " IS NOT PRELOAD!");
        }
        
        poolInstance[objectPrefab.PoolType].Despawn(objectPrefab);
    }

    public static void Collect(PoolType poolType)
    {
        if (!poolInstance.ContainsKey(poolType))
        {
            Debug.LogError(poolType + " IS NOT PRELOAD!");
        }
        
        poolInstance[poolType].Collect();
    }

    public static void CollectAll()
    {
        foreach(var item in poolInstance.Values)
        {
            item.Collect();
        }
    }

    public static void Release(PoolType poolType)
    {
        if (!poolInstance.ContainsKey(poolType))
        {
            Debug.LogError(poolType + " IS NOT PRELOAD!");
        }

        poolInstance[poolType].Release();
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
    GameUnit prefab;

    Stack<GameUnit> inactiveObjects = new Stack<GameUnit>();

    List<GameUnit> activeObjects = new List<GameUnit>();

    public void PreLoad(GameUnit prefab, int amount, Transform parent)
    {
        this.parent = parent;
        this.prefab = prefab;

        for (int i = 0; i < amount; i++)
        {
            Despawn(GameObject.Instantiate(prefab, parent));
        }
    }

    public GameUnit Spawn(Vector3 pos, Quaternion rot)
    {
        GameUnit obj;

        if (inactiveObjects.Count <= 0)
        {
            obj = GameObject.Instantiate(prefab, parent);
        }
        else
        {
            obj = inactiveObjects.Peek();
            inactiveObjects.Pop();
        }

        obj.transform.SetPositionAndRotation(pos, rot);
        activeObjects.Add(obj);
        obj.gameObject.SetActive(true);

        return obj; 
    }

    public void Despawn(GameUnit objectPrefab)
    {
        if (objectPrefab != null && objectPrefab.gameObject.activeSelf)
        {
            activeObjects.Remove(objectPrefab);
            inactiveObjects.Push(objectPrefab);
            objectPrefab.gameObject.SetActive(false);
        }
    }

    public void Collect()
    {
        while(activeObjects.Count > 0)
        {
            Despawn(activeObjects[0]);
        }
    }

    public void Release()
    {
        Collect();

        while(inactiveObjects.Count > 0)
        {
            GameObject.Destroy(inactiveObjects.Peek().gameObject);
        }
        inactiveObjects.Clear();
    }
}
