using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class SendTap : NetworkBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        Touch.OnTap += SendTouch;
        Touch.OnTap += DebugData;
    }

    void DebugData()
    {
        Debug.Log("Touched");
    }

    void SendTouch()
    {
        if (isLocalPlayer)
        {
            CmdSendTouch();
        }
    }
    [Command(requiresAuthority = false)]
    void CmdSendTouch()
    {
        Debug.Log("Received Touch from client");
        JumpAction();
    }

    [ServerCallback]
    void JumpAction()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up, ForceMode.Impulse);
    }
}
