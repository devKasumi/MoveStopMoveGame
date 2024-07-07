using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BasePool<T> where T : MonoBehaviour
{
    private static Dictionary<int, Pool<T>> poolInstance = new Dictionary<int, Pool<T>>();

    public static void PreLoad(T objectPrefab, int key, int amount, Transform parent)
    {
        if (!objectPrefab)
        {
            Debug.LogError("PREFAB IS EMPTY!");
            return;
        }

        if (!poolInstance.ContainsKey(key) || poolInstance[key] == null)
        {
            Pool<T> p = new Pool<T>();
            p.PreLoad(objectPrefab, amount, parent);
            poolInstance[key] = p;
        }
    }

    public static T Spawn(T objectPrefab, int key, Vector3 pos, Quaternion rot)
    {
        if (!poolInstance.ContainsKey(key))
        {
            Debug.LogError(objectPrefab + " IS NOT PRELOAD!");
            return null;
        }

        return poolInstance[key].Spawn(pos, rot);
    }

    public static void Despawn(T objectPrefab, int key)
    {
        if (!poolInstance.ContainsKey(key))
        {
            Debug.LogError(objectPrefab + " IS NOT PRELOAD!");
        }
        
        poolInstance[key].Despawn(objectPrefab);
    }

    public static void Collect(T objectPrefab, int key)
    {
        if (!poolInstance.ContainsKey(key))
        {
            Debug.LogError(objectPrefab + " IS NOT PRELOAD!");
        }
        
        poolInstance[key].Collect();
    }

    public static void CollectAll()
    {
        foreach(var item in poolInstance.Values)
        {
            item.Collect();
        }
    }

    public static void Release(T objectPrefab, int key)
    {
        if (!poolInstance.ContainsKey(key))
        {
            Debug.LogError(objectPrefab + " IS NOT PRELOAD!");
        }

        poolInstance[key].Release();
    }

    public static void ReleaseAll()
    {
        foreach (var item in poolInstance.Values)
        {
            item.Release();
        }
    }
}

public class Pool<T> where T : MonoBehaviour
{
    Transform parent;
    T prefab;

    Stack<T> inactiveObjects = new Stack<T>();

    List<T> activeObjects = new List<T>();

    public void PreLoad(T prefab, int amount, Transform parent)
    {
        this.parent = parent;
        this.prefab = prefab;

        for (int i = 0; i < amount; i++)
        {
            Despawn(GameObject.Instantiate(prefab, parent));
        }
    }

    public T Spawn(Vector3 pos, Quaternion rot)
    {
        T obj;

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

    public void Despawn(T objectPrefab)
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
