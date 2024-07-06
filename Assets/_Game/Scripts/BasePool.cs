using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CommonEnum;

//public enum PoolType
//{
//    CharacterPool = 0,
//    WeaponPool = 1,
//}

//public class ObjectPool : MonoBehaviour
//{
//    public PoolType PoolType;
//}

public static class BasePool<T> where T : MonoBehaviour
{
    private static Dictionary<T, Pool<T>> poolInstance = new Dictionary<T, Pool<T>>();

    public static void PreLoad(T objectPrefab, int amount, Transform parent)
    {
        if (!objectPrefab)
        {
            Debug.LogError("PREFAB IS EMPTY!");
            return;
        }

        //switch (objectPrefab.tag)
        //{
        //    case Constants.TAG_BOT:
        //        if (!poolInstance.ContainsKey(objectPrefab) || poolInstance[objectPrefab] == null)
        //        {
        //            Pool<T> p = new Pool<T>();
        //            p.PreLoad(objectPrefab, amount, parent);
        //            poolInstance[objectPrefab] = p;
        //        }
        //        break;
        //    case Constants.TAG_WEAPON:

        //        break;
        //    default:
        //        break;
        //}

        if (!poolInstance.ContainsKey(objectPrefab) || poolInstance[objectPrefab] == null)
        {
            Debug.LogError("preload weapon prefab!!!!");
            Pool<T> p = new Pool<T>();
            p.PreLoad(objectPrefab, amount, parent);
            poolInstance[objectPrefab] = p;
        }

        Debug.LogError("pokemon:  " + poolInstance.Count);

        //Debug.LogError("preload weapon prefab!!!!");
        //Pool<T> p = new Pool<T>();
        //p.PreLoad(objectPrefab, amount, parent);
        //poolInstance[objectPrefab.tag] = p;
    }

    public static T Spawn(T objectPrefab, Vector3 pos, Quaternion rot)
    {
        if (!poolInstance.ContainsKey(objectPrefab))
        {
            Debug.LogError(objectPrefab + " IS NOT PRELOAD!");
            return null;
        }

        Debug.LogError("pokemon:  " + poolInstance.Count);

        return poolInstance[objectPrefab].Spawn(pos, rot);
    }

    public static void Despawn(T objectPrefab)
    {
        if (!poolInstance.ContainsKey(objectPrefab))
        {
            Debug.LogError(objectPrefab + " IS NOT PRELOAD!");
        }

        poolInstance[objectPrefab].Despawn(objectPrefab);
    }

    public static void Collect(T objectPrefab)
    {
        if (!poolInstance.ContainsKey(objectPrefab))
        {
            Debug.LogError(objectPrefab + " IS NOT PRELOAD!");
        }

        poolInstance[objectPrefab].Collect();
    }

    public static void CollectAll()
    {
        foreach(var item in poolInstance.Values)
        {
            item.Collect();
        }
    }

    public static void Release(T objectPrefab)
    {
        if (!poolInstance.ContainsKey(objectPrefab))
        {
            Debug.LogError(objectPrefab + " IS NOT PRELOAD!");
        }

        poolInstance[objectPrefab].Release();
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
