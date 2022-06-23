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
        for(int i=0; i < transform.childCount; i++)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }

    }

}
