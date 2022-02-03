using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputSys : MonoBehaviour
{
    public static InputSys Instance;

    //public static bool LeftPressed = false;
    //public static bool RightPressed = false;

    public static bool LeftPressed {
        get
        {
            var a = LeftPressed;
            LeftPressed = false;
            return a;

        }
        set { LeftPressed = value; }
    }

    public static bool RightPressed
    {
        get
        {
            var a = RightPressed;
            RightPressed = false;
            return a;

        }
        set { RightPressed = value; }
    }

    public void OnLeftClick()
    {
        LeftPressed = true;
    }

    public void OnRightClick()
    {
        RightPressed = true;
    }

    private void Update()
    {
        LeftPressed = false;
        RightPressed = false;
    }
}
