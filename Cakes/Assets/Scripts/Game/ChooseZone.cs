using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseZone : MonoBehaviour
{
    private Cake _currentCake = null;
    [SerializeField]
    private Image _currentCakeIconFace;
    [SerializeField]
    private Image _currentCakeIconBack;

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

    [Header("Calc % of path")]
    [SerializeField]
    private Axis _axisMovement;
    [SerializeField]
    private Transform _pointStart;
    [SerializeField]
    private Transform _pointFinish;


    private void Start()
    {
        _currentCakeIconFace.enabled = false;
        _currentCakeIconBack.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        var cake = other.GetComponent<Cake>();
        if (cake != null)
        {
            _currentCake = cake;
            _currentCakeIconFace.sprite = cake.GetSprite();
            _currentCakeIconFace.enabled = true;
            _currentCakeIconBack.sprite = cake.GetSprite();
            _currentCakeIconBack.enabled = true;
            //_currentCakeIconFace.fillAmount = 100 - CalculatePercentDistance(cake.gameObject);

            //if (_isBorderOn)
            //{
              //  __borderIn.material = _green;
                //__borderOut.material = _green;
            //}
            
        }
    }
    private void OnTriggerStay(Collider other)
    {
        _currentCakeIconFace.fillAmount = 1- CalculatePercentDistance(other.gameObject)/100;
    }

    private float CalculatePercentDistance(GameObject currentCake)
    {
        float fullDistance = 0f;
        float currentDistance = 0f;
        float result = 0f;
        switch (_axisMovement)
        {
            case Axis.X:
                {
                    fullDistance = _pointFinish.position.x - _pointStart.position.x;
                    currentDistance = currentCake.transform.position.x - _pointStart.position.x;
                    result = currentDistance/(fullDistance/100); 
                    
                }
                break;
            case Axis.Y:
                {
                    fullDistance = _pointFinish.position.y - _pointStart.position.y;
                    currentDistance = currentCake.transform.position.y - _pointStart.position.y;
                    result = currentDistance / (fullDistance / 100);
                }
                break;
            case Axis.Z:
                {
                    fullDistance = _pointFinish.position.z - _pointStart.position.z;
                    currentDistance = currentCake.transform.position.z - _pointStart.position.z;
                    result = currentDistance / (fullDistance / 100);
                }
                break;
        }
        Debug.Log("reult = " + result);
        return result;

    }

    private void OnTriggerExit(Collider other)
    {
        var cake = other.GetComponent<Cake>();
        if (cake != null)
        {
            _currentCake = null;
            _currentCakeIconFace.enabled = false;
            _currentCakeIconBack.enabled = false;

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

public enum Axis
{
    X,
    Y,
    Z
}