using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class ScoreSystem: MonoBehaviour
{
    public static ScoreSystem Instance = null;
    public static int CurrentScore=0;

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
#if DEBUG
        //SetMoney(9999999);
#endif

    }

    public int GetMoney()
    {
        return SaveManager.Instance.Load<int>("Money");
    }
    public int GetMaxScore()
    {
        return SaveManager.Instance.Load<int>("MaxScore");
    }

    public bool MinusMoney(int value)
    {
        var money = SaveManager.Instance.Load<int>("Money");
        if (money >= value)
        {
            SaveManager.Instance.Save<int>("Money", money - value);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddMoney(int value)
    {
        var money = SaveManager.Instance.Load<int>("Money");
        SaveManager.Instance.Save<int>("Money", money + value);
        Debug.Log("Money: " + money + "+"+value+"="+ SaveManager.Instance.Load<int>("Money").ToString());
    }

    public bool SetMaxScore(int value)
    {
        var score = SaveManager.Instance.Load<int>("MaxScore");
        if (score < value)
        {
            SaveManager.Instance.Save<int>("MaxScore", value);
            return true;
        }
        else
        {
            return false;
        }
    }

    private void SetMoney(int value)
    {
        SaveManager.Instance.Save<int>("Money", value);
    }

    public void Reset()
    {
        SaveManager.Instance.Save<int>("Money", 0);
        SaveManager.Instance.Save<int>("MaxScore", 0);
        CurrentScore = 0;
    }

}
