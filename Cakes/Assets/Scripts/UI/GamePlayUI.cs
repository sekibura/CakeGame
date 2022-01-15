using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _pauseMenu;
    private GameManagerScript _gameManager;
    [SerializeField]
    private GameSettings _gameSettings;

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

    public void HomeScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main");
    }

}
