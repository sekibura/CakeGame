using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScreenScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textField;
    

    private void Start()
    {
        //_textField.text = "loh";
    }
    public void Set(List<int> order)
    {
        //Debug.Log("order count to display: " + order.Count);
        string str = "";
        foreach (var item in order)
        {
            str += item + ",";
        } 
        _textField.text = str;
        //Debug.Log("order to display: " + str);
    }
    //private void Update()
    //{
    //    _textField.text = (Random.Range(0,100).ToString());
    //}
}
