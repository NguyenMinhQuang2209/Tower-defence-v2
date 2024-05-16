using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogManager : MonoBehaviour
{
    public static LogManager instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public void Log(string txt)
    {
        Debug.Log(txt);
    }
    public void Log(string txt, GameObject obj)
    {
        Debug.Log(txt, obj);
    }
}
