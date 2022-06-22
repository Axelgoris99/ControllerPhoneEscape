using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class ShowOrNot : NetworkBehaviour
{
    void Start()
    {
        hideObject();     
    }

    [ClientCallback]
    void hideObject()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

}
