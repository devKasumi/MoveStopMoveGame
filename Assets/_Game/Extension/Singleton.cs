using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (!instance)
            {
                instance = GameObject.FindObjectOfType<T>();

                if (!instance)
                {
                    instance = new GameObject(nameof(T)).AddComponent<T>();
                }
            }

            return instance;
        }
    }
}
