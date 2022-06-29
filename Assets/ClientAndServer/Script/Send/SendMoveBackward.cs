using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class SendMoveBackward : NetworkBehaviour
{
    public int strenght = 5;

    // Start is called before the first frame update
    private void OnEnable()
    {
        Movement.OnBackward += SendTouch;
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
        BackwardAction();
    }

    [ServerCallback]
    void BackwardAction()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(rb.transform.up * - strenght, ForceMode.Impulse);
    }
}
