using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnLeft : MonoBehaviour
{
    public delegate void TapAction();
    public static event TapAction OnTurnLeft;

    public void OnTouch()
    {
        OnTurnLeft();
    }
}
