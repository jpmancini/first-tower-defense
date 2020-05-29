using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//use this class to create global varaibles (like the Tiles dictionary)
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
            }

            return instance;
        }
    }
}
