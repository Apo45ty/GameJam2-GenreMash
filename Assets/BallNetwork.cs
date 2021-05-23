using System.Collections;
using System.Collections.Generic;
using MLAPI.NetworkVariable;
using MLAPI;
using UnityEngine;

public class BallNetwork : NetworkBehaviour
{
    public NetworkVariableVector3 Position = new NetworkVariableVector3(new NetworkVariableSettings
        {
            WritePermission = NetworkVariablePermission.ServerOnly,
            ReadPermission = NetworkVariablePermission.Everyone
        });
        public NetworkVariableQuaternion rotation = new NetworkVariableQuaternion(new NetworkVariableSettings
        {
            WritePermission = NetworkVariablePermission.ServerOnly,
            ReadPermission = NetworkVariablePermission.Everyone
        });

    // Update is called once per frame
    void Update()
    {
        if(!IsServer){
            transform.position = Position.Value;
            transform.rotation = rotation.Value;
        } else {
            Position.Value = transform.position;
            rotation.Value =  transform.rotation;
        }
    }
}
