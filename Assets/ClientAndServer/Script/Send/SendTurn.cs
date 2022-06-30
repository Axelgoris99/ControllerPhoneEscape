using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SendTurn : NetworkBehaviour
{
    public int strenght = 5;

    // Start is called before the first frame update
    private void OnEnable()
    {
        Movement.OnTurn += SendTouch;
    }

    void SendTouch(float str)
    {
        if (isLocalPlayer)
        {
            CmdSendTouch(str);
        }
    }
    [Command(requiresAuthority = false)]
    void CmdSendTouch(float str)
    {
        TurnAction(str);
    }

    [ServerCallback]
    void TurnAction(float str)
    {
        Vector3 turnMove = transform.forward;
        transform.RotateAround(transform.position, turnMove, str * strenght * Time.deltaTime);
        //rb.AddForce(rb.transform.up * strenght, ForceMode.Impulse);
    }
}
