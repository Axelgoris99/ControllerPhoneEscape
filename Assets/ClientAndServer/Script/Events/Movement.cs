using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Joystick joystick;
    public delegate void TapAction();
    public static event TapAction OnForward;
    public static event TapAction OnBackward;
    public static event TapAction OnLeft;
    public static event TapAction OnRight;

    void FixedUpdate()
    {
        if(joystick.Horizontal >= .2f)
        {
            OnRight();
        }   
        else if(joystick.Vertical <=  -0.2f)
        {
            OnLeft();
        }
        if(joystick.Vertical >= .2f)
        {
            OnForward();
        }
        else if(joystick.Vertical <= -.2f)
        {
            OnBackward();
        }
    }

}
