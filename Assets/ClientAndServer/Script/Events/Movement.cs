using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Joystick joystick;
    public delegate void TapAction(float str);
    public static event TapAction OnForward;
    public static event TapAction OnTurn;

    void Update()
    {
        if(joystick.Horizontal >= .2f || joystick.Horizontal <= -0.2f)
        {
            OnTurn(-joystick.Horizontal);
        }   
        if(joystick.Vertical >= .2f || joystick.Vertical <= -.2f)
        {
            OnForward(joystick.Vertical);
        }
    }

}
