using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class SendJump : NetworkBehaviour
{
    public int strenght = 5;
    // Start is called before the first frame update
    private void OnEnable()
    {
        Jump.OnJump += SendTouch;
        Jump.OnJump += DebugData;
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
        JumpAction();
    }

    [ServerCallback]
    void JumpAction()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * strenght, ForceMode.Impulse);
    }
}
