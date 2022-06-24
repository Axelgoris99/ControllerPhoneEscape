using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnRight : MonoBehaviour
{
    public delegate void TapAction();
    public static event TapAction OnTurnRight;

    public void OnTouch()
    {
        OnTurnRight();
    }
}
