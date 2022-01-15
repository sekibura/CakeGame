using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class ScoreSystem: MonoBehaviour
{
    public static ScoreSystem Instance = null;

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

    public int GetMoney()
    {
        return PlayerPrefs.GetInt("Money");
    }
    public int GetMaxScore()
    {
        return PlayerPrefs.GetInt("MaxScore");
    }

    public bool MinusMoney(int value)
    {
        var money = PlayerPrefs.GetInt("Money"); ;
        if (money >= value)
        {
            PlayerPrefs.SetInt("Money",money-value);
            PlayerPrefs.Save();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddMoney(int value)
    {
        var money = PlayerPrefs.GetInt("Money"); ;
        PlayerPrefs.SetInt("Money", money + value);
        PlayerPrefs.Save();
    }

    public bool SetMaxScore(int value)
    {
        var score = PlayerPrefs.GetInt("MaxScore");
        if (score < value)
        {
            PlayerPrefs.SetInt("MaxScore", value);
            PlayerPrefs.Save();
            return true;
        }
        else
        {
            return false;
        }
    }

}
