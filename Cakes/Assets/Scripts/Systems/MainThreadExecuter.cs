using System;
using System.Collections.Generic;
using UnityEngine;

public class MainThreadExecuter : MonoBehaviour
{
    public static MainThreadExecuter Instance;
    private Queue<Action> _events = new Queue<Action>();
    void Start()
    {
        if (Instance == null)
            Instance = this; 
        else if (Instance == this)
            Destroy(gameObject); 

        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        if(_events.Count>0)
        {
            var a = _events.Dequeue();
            a.Invoke();
        }
    }
    public void DoInMainThread(Action act)
    {
        _events.Enqueue(act);
    }
}
