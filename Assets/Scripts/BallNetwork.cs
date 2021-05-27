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
    public int interpolationFramesCount = 10; // Number of frames to completely interpolate between the 2 positions
    int elapsedFrames = 0;
    private bool networkStarted=false;
    public override void NetworkStart(){
        networkStarted=true;
    }
    // Update is called once per frame
    void Update()
    {
        if(!networkStarted) return;
        if(!IsServer){
            float interpolationRatio = (float)elapsedFrames / interpolationFramesCount;
            Vector3 interpolatedPosition = Vector3.Lerp(transform.position, Position.Value, interpolationRatio);
            elapsedFrames = (elapsedFrames + 1) % (interpolationFramesCount + 1);  // reset elapsedFrames to zero after it reached (interpolationFramesCount + 1)

            transform.position = interpolatedPosition;
            transform.rotation = rotation.Value;
        } else {
            Position.Value = transform.position;
            rotation.Value =  transform.rotation;
        }
    }
}
