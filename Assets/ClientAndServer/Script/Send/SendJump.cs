using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class SendJump : NetworkBehaviour
{
    public int strenght = 5;
    float distToGround = 1f;
    // Start is called before the first frame update
    private void OnEnable()
    {
        ButtonManager.OnJump += SendTouch;
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

    bool IsGrounded() {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }
    [ServerCallback]
    void JumpAction()
    {
        if (IsGrounded())
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up * strenght, ForceMode.Impulse);
        }
    }
}
