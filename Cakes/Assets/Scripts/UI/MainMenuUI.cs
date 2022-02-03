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
    private GameObject _shopMenu;
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

        if(UnityEngine.Random.Range(0, 3)==0)
            InterstitialAd.S.ShowAd(()=> { });

    }
    public void StartButton()
    {
        _darkScreen.SetActive(true);

        StartCoroutine("LoadAfterWait", 1.1f);
    }
    
    IEnumerator LoadAfterWait(float value)
    {
        yield return new WaitForSeconds(value);
        SceneManager.LoadScene("UpdatedGamePlay");

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
        ShowInterstitial();
    }

    public void Shop()
    {
        if (!_settingsMenu.activeSelf)
        {
            _shopMenu.SetActive(true);
            
        }
        else
        {
            _shopMenu.SetActive(false);
        }
        ShowInterstitial();
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

    private void ShowInterstitial()
    {
        bool toShow = UnityEngine.Random.Range(0, 3) == 0 ? true : false;
        if (toShow)
        {
            //var volume = _gameSettings.GetVolume();
            //_gameSettings.SetVolume(-80);

            InterstitialAd.S.ShowAd(() => {
                //_gameSettings.SetVolume(volume);
            });
        }       
    }

    

}
