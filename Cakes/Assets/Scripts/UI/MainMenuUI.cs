using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _settingsMenu;
    [SerializeField]
    private GameSettings _gameSettings;

    [SerializeField]
    private GameObject _darkScreen;

    [SerializeField]
    private TMP_Text _scoreField;
    [SerializeField]
    private TMP_Text _moneyField;

    private void Start()
    {
        _scoreField.text = ScoreSystem.Instance.GetMaxScore().ToString();
        _moneyField.text = ScoreSystem.Instance.GetMoney().ToString();

    }
    public void StartButton()
    {
        _darkScreen.SetActive(true);

        StartCoroutine("LoadAfterWait", 1.1f);
    }
    
    IEnumerator LoadAfterWait(float value)
    {
        yield return new WaitForSeconds(value);
        SceneManager.LoadScene("GamePlay");

    }

    public void Settings()
    {
        if (!_settingsMenu.activeSelf)
        {
            _settingsMenu.SetActive(true);
            _gameSettings.Init();
        }
        else
        {
            _settingsMenu.SetActive(false);
        }
    }

    public void OpenURL(string URL)
    {
        try
        {
            Application.OpenURL(URL);
        }
        catch(Exception e)
        {
            Debug.LogError(e.Message);
        }
        
    }

}
