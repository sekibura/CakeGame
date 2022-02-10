using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnePeople : MonoBehaviour
{
    private string _lastTag = null;
    [SerializeField]
    private Follower _follower;
    private FadeGameObjects _fader;
    [SerializeField]
    private float _distanceTravelled;
    [SerializeField]
    private SpriteRenderer[] _spriteRenderer;

    private void Start()
    {
        ChangeColor();
        _fader = FindObjectOfType<FadeGameObjects>();
        //_follower.SetSpeed(0);
        _follower.SetDistance(_distanceTravelled);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (_lastTag == null)
        {
            _lastTag = other.gameObject.tag;
            switch (_lastTag)
            {
                case "StopZone":
                    OnStopPlace();
                    break;
                case "FadeInZone":
                    FadeIn();
                    break;
                case "ChangeColorZone":
                    ChangeColor();
                    break;
                case "FadeOutZone":
                    FadeOut();
                    break;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        _lastTag = null;
    }

    private void OnStopPlace()
    {
        _follower.SetSpeed(0);
    } 

    public void StartMove()
    {
        //Debug.Log("ResetSpeed");
        _follower.ResetSpeed();
    }

    private void FadeIn()
    {
        _fader.FadeIn(gameObject, 0.3f);
    }

    private void ChangeColor()
    {
        foreach (var item in _spriteRenderer)
        {
            item.color = new Color(Random.Range(0F, 1F), Random.Range(0, 1F), Random.Range(0, 1F));
        }
        
    }

    private void FadeOut()
    {
        _fader.FadeOut(gameObject, 0.3f);
    }
}
