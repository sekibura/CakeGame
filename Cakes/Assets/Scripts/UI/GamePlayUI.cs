using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GamePlayUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _pauseMenu;
    private GameManagerScript _gameManager;
    [SerializeField]
    private GameSettings _gameSettings;
    [SerializeField]
    private GameObject _gameOverMenu;

    [SerializeField]
    private TMP_Text _score;
    [SerializeField]
    private TMP_Text _Maxscore;
    [SerializeField]
    private TMP_Text _money;
    [SerializeField]
    private GameObject _toDark;


    private void Start()
    {
        _gameManager = FindObjectOfType<GameManagerScript>();
    }

    public void SetPause()
    {
        if (!_pauseMenu.activeSelf)
        {
            _gameManager.SetPause(true);
            _pauseMenu.SetActive(true);
            _gameSettings.Init();
        }
        else
        {
            _gameManager.SetPause(false);
            _pauseMenu.SetActive(false);
        }
    }

    public void BtnHomeScene()
    {
        _gameManager.GoHome();
    }

    public void UpdateScoreAndMoney()
    {
        _score.text = ScoreSystem.CurrentScore.ToString();
        _Maxscore.text = ScoreSystem.Instance.GetMaxScore().ToString();
        _money.text = ScoreSystem.Instance.GetMoney().ToString();
    }

    public void GameOver()
    {
        _gameOverMenu.SetActive(true);
    }

    public void ToDark()
    {
        Debug.Log("toDark");
        _toDark.SetActive(true);
    }
}
