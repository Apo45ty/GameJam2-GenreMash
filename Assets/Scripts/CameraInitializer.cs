using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class CameraInitializer : NetworkBehaviour
{
    public static int playerCount=0;
    public override void NetworkStart()
        {   
            if(IsServer){
                this.name="Player"+(playerCount++);
            }
            if(IsOwner){
                Camera.main.gameObject.SetActive(false);
                GetComponentInChildren<Camera>().enabled=true;
            } else {
                GetComponentInChildren<Camera>().gameObject.SetActive(false);
            }
        }
}
