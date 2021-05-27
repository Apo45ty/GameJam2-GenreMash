using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class DestroyIfNotOwner : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(!IsOwner)
        Destroy(gameObject) ;  
    }
}
