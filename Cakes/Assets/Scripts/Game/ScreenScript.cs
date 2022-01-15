using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScreenScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textField;
    [SerializeField]
    private Image[] _images;
    


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
    public void Set(List<Sprite> order)
    {
        for (int i = 0; i < _images.Length; i++)
        {
            if(i < order.Count)
            {
                _images[i].enabled = true;
                _images[i].sprite = order[i];

            }
            else
            {
                _images[i].enabled = false;
            }
        }
    }
    
}
