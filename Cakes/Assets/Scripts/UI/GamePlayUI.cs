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

    #region GameOverScreen
    [Header("GameOver Screen")]
    [SerializeField]
    private TMP_Text _maxScore;
    [SerializeField]
    private TMP_Text _currentScore;
    [SerializeField]
    private TMP_Text _addMoney;
    #endregion


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
        try
        {
            _score.text = ScoreSystem.CurrentScore.ToString();
            //_Maxscore.text = ScoreSystem.Instance.GetMaxScore().ToString();
            _money.text = ScoreSystem.Instance.GetMoney().ToString();
        }
        catch { }
        
    }

    public void GameOver(int currentScore, int maxScore, int coins)
    {
        _gameOverMenu.SetActive(true);
        _maxScore.text = maxScore.ToString();
        _currentScore.text = currentScore.ToString();
        _addMoney.text = "+" + coins.ToString();
    }

    public void ToDark()
    {
        Debug.Log("toDark");
        _toDark.SetActive(true);
    }
}
