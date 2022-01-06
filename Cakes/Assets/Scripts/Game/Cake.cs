using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cake : MonoBehaviour
{
    public int ID { get; set; }
    [SerializeField]
    private Texture _defaultTexture;
    [SerializeField]
    private Texture _finalTexture;

    private string _lastTag = "";


    private void OnTriggerEnter(Collider other)
    {
        if (_lastTag == null)
            _lastTag = other.gameObject.tag;
        switch (_lastTag)
        {
            case "OvenZone":
                ChangeTexture();
                break;
            case "ChooseZone":
                ChoozeZone();
                break;
            case "DestroyZone":
                DestroyCake();
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _lastTag = null;
    }

    private void DestroyCake()
    {

    }

    private void ChangeTexture()
    {

    }

    private void ChoozeZone()
    {

    }
}
