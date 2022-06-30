using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class SendJump : NetworkBehaviour
{
    CharacterController cc;
    public int strenght = 5;
    float distToGround = 1.0f;
    // Start is called before the first frame update
    private void OnEnable()
    {
        cc = GetComponent<CharacterController>();
        ButtonManager.OnJump += SendTouch;
    }
    private void Update()
    {
        if (isLocalPlayer)
        {
            CmdSendFall();
        }
    }

    [Command(requiresAuthority = false)]
    void CmdSendFall()
    {
        FallAction();
    }
    [ServerCallback]
    void FallAction()
    { 
        if (!IsGrounded())
        {
            Vector3 down = transform.forward;
            cc.Move(9.81f * down * Time.deltaTime);
        }
    }
    void SendTouch()
    {
        if (isLocalPlayer)
        {
            Debug.Log("test");
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
        
        Debug.Log(IsGrounded());
        if (IsGrounded())
        {
            Vector3 jumpMove = -transform.forward;
            cc.Move(jumpMove * strenght * Time.deltaTime);
        }
    }
}
