using System;
using UnityEngine;

/// <summary>
/// Singleton Interface
/// </summary>
public interface ISingleton
{
    void Init();
    void Release();
}

/// <summary>
/// SingletonBase Class
/// </summary>
public abstract class SingletonBase<T> : ISingleton where T : class
{
    private static T _instance = null;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = CreateInstance();
            }

            return _instance;
        }
    }

    private static T CreateInstance()
    {
        return Activator.CreateInstance(typeof(T), true) as T;
    }

    public abstract void Init();

    public abstract void Release();
}

/// <summary>
/// MonoSingletonBase Class
/// </summary>
public abstract class MonoSingletonBase<T> : MonoBehaviour, ISingleton where T : MonoBehaviour
{
    private static T _instance = null;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<T>();
                if (_instance == null)
                {
                    GameObject instanceObj = new GameObject();
                    instanceObj.name = "(Singleton)" + typeof(T).ToString();
                    _instance = instanceObj.AddComponent<T>();
                }
            }

            return _instance;
        }
    }

    public abstract void Init();

    public abstract void Release();
}