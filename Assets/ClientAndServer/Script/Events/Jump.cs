using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Jump : MonoBehaviour
{
    public delegate void TapAction();
    public static event TapAction OnJump;

    public void OnTouch()
    {
        OnJump();
    }
}
