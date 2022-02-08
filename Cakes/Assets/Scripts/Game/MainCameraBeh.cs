using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraBeh : MonoBehaviour
{
    [SerializeField]
    private GameObject _faceWalls;

    void Start()
    {
        Invoke("StartAnimation", 1);
    }

    private void StartAnimation()
    {
        FadeGameObjects fader = new FadeGameObjects();
        fader.FadeIn(_faceWalls);
        gameObject.GetComponent<Animator>().Play("Start");
        Invoke("TurnOff", 3);
    }

    private void TurnOff()
    {
        _faceWalls.SetActive(false);
    }
}
