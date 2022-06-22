using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Touch : MonoBehaviour
{
    public delegate void TapAction();
    public static event TapAction OnTap;

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0 && OnTap!= null)
        {
            OnTap();
        } 
    }
}
