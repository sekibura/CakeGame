using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peoples : MonoBehaviour
{

    [SerializeField]
    private PathCreator _leftSidePath;
    [SerializeField]
    private PathCreator _rightSidePath;

    [SerializeField]
    private OnePeople[] _peoplesLeft;
    [SerializeField]
    private OnePeople[] _peoplesRight;

     


    public void Shift(Sides side)
    {
        Debug.Log("Shift");
        OnePeople[] _ppls = null;

        if (side == Sides.Left)
            _ppls = _peoplesLeft;
        else if(side == Sides.Right)
            _ppls = _peoplesRight;

        foreach (var man in _ppls)
        {
            man.StartMove(); 
        }
    }

    
   
}
