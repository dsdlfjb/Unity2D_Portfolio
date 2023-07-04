﻿using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static object syncObject = new object();

    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {

                lock (syncObject)
                {
                    instance = FindObjectOfType<T>();

                    if (instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = typeof(T).Name;
                        instance = obj.AddComponent<T>();
                    }
                }
            }

            return instance;
        }
    }


    protected virtual void Awake()
    {


        if (instance == null)
        {
            instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void OnDestroy()
    {
        if (instance != this)
            return;

        instance = null;
    }

    public static bool HasInstance()
    {
        return instance ? true : false;
    }
}
