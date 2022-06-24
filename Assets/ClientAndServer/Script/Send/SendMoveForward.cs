using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class SendMoveForward : NetworkBehaviour
{
    public int strenght = 5;

    // Start is called before the first frame update
    private void OnEnable()
    {
        MoveForward.OnForward += SendTouch;
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
        ForwardAction();
    }

    [ServerCallback]
    void ForwardAction()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(rb.transform.up * strenght, ForceMode.Impulse);
    }
}
