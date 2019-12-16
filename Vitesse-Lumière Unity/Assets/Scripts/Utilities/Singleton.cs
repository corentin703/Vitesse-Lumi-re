using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T Instance { get; private set; } = null;
    
    protected virtual void Awake()
    {
        if (Instance == null)
            Instance = (T)this;
        else if (Instance != null)
        {
            throw new System.Exception("[Singleton] Trying to instantiate a second instance of a singleton class.");
        }
    }
    
    protected virtual void OnDestroy()
    {
        if (Instance == (T)this)
            Instance = null;
    }
}


//public class Singleton<T1, T2> where T1 : Singleton<T1, T2> where T2 : MonoBehaviour 
//{
//    public static T1 Instance { get; private set; } = null;
//    
//    protected virtual void Awake()
//    {
//        if (Instance == null)
//            Instance = (T1)this;
//        else if (Instance != this)
//        {
//            throw new System.Exception("[Singleton] Trying to instantiate a second instance of a singleton class.");
//        }
//            
//    }
//    
//    protected virtual void OnDestroy()
//    {
//        if (Instance == (T1)this)
//            Instance = null;
//    }
//}
