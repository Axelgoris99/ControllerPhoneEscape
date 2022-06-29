using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class SendTurnRight : NetworkBehaviour
{
    public int strenght = 5;

    // Start is called before the first frame update
    private void OnEnable()
    {
        Movement.OnRight += SendTouch;
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
        RightAction();
    }

    [ServerCallback]
    void RightAction()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, 0, - strenght));
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}
