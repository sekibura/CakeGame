using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance = null;

    private void Start()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public T Load<T>(string name)
    {
        if (typeof(T) == typeof(string))
        {
            return (T)(object)PlayerPrefs.GetString(name);
        }
        else if (typeof(T) == typeof(int))
        {
            return (T)(object)PlayerPrefs.GetInt(name);
        }
        else if(typeof(T) == typeof(float))
        {
            return (T)(object)PlayerPrefs.GetFloat(name);
        }
        else
        {
            throw new Exception();
        }
        
    }

    public void Save<T>(string name, T value)
    {
        if (typeof(T) == typeof(string))
        {
             PlayerPrefs.SetString(name, (string)(object)value);
        }
        else if (typeof(T) == typeof(int))
        {
             PlayerPrefs.SetInt(name, (int)(object)value);
        }
        else if (typeof(T) == typeof(float))
        {
             PlayerPrefs.SetFloat(name, (float)(object)value);
        }
        PlayerPrefs.Save();
    }
}
