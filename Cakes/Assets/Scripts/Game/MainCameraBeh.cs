using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraBeh : MonoBehaviour
{
    [SerializeField]
    private GameObject _faceWalls;
    FadeGameObjects fader;

    void Start()
    {
        fader = FindObjectOfType<FadeGameObjects>();
        Invoke("StartAnimation", 1);
    }

    private void StartAnimation()
    {
        fader.FadeIn(_faceWalls, 1);
        gameObject.GetComponent<Animator>().Play("Start");
        Invoke("TurnOff", 1);
    }

    private void TurnOff()
    {
        _faceWalls.SetActive(false);
    }
}
