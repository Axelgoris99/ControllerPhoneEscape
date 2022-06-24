using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public delegate void TapAction();
    public static event TapAction OnForward;

    public void OnTouch()
    {
        OnForward();
    }
}
