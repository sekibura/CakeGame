using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseZone : MonoBehaviour
{
    private Cake _currentCake = null;
    [SerializeField]
    private Image _currentCakeIcon;

    [SerializeField]
    private MeshRenderer __borderIn;
    [SerializeField]
    private MeshRenderer __borderOut;
    [SerializeField]
    private Material _red;
    [SerializeField]
    private Material _green;
    [SerializeField]
    private bool _isBorderOn = false;


    private void Start()
    {
        _currentCakeIcon.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        var cake = other.GetComponent<Cake>();
        if (cake != null)
        {
            _currentCake = cake;
            _currentCakeIcon.sprite = cake.GetSprite();
            _currentCakeIcon.enabled = true;
            if (_isBorderOn)
            {
                __borderIn.material = _green;
                __borderOut.material = _green;
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var cake = other.GetComponent<Cake>();
        if (cake != null)
        {
            _currentCake = null;
            _currentCakeIcon.enabled = false;

            if (_isBorderOn)
            {
                __borderIn.material = _red;
                __borderOut.material = _red;
            }
        }
    }

    public void Button(string side)
    {
        if (_currentCake == null)
            return;

        if(side == "Left")
        {
            _currentCake.MoveToOrderInZone(Sides.Left);
        }
        else if (side == "Right")
        {
            _currentCake.MoveToOrderInZone(Sides.Right);
        }
    }

}
