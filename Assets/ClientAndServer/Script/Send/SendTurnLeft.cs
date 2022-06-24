using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class SendTurnLeft : NetworkBehaviour
{
    public int strenght = 5;

    // Start is called before the first frame update
    private void OnEnable()
    {
        TurnLeft.OnTurnLeft += SendTouch;
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
        LeftAction();
    }

    [ServerCallback]
    void LeftAction()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, 0, 45));
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}
