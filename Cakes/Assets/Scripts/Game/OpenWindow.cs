using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWindow : MonoBehaviour
{
    [SerializeField]
    private Animator _door;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("InDoor");
        _door.Play("Open");
    }
}
