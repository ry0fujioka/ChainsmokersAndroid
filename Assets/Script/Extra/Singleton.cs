using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    // Variable of the instance
    private static T _instance;

    // Property of the instance
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    GameObject newGameObject = new GameObject();
                    _instance = newGameObject.AddComponent<T>();
                }
            }

            return _instance;
        }
    }

    // In case any Singleton class has Awake method, we need to prepare here as well 
    protected virtual void Awake()
    {
        _instance = this as T;
        //if (_instance == null)
        //    _instance = this as T;
        //else
        //    Destroy(this.gameObject);
    }
}
