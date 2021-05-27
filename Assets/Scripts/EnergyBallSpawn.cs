using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkVariable;

public class EnergyBallSpawn : NetworkBehaviour
{
     public NetworkVariableBool hasSpawned = new NetworkVariableBool(new NetworkVariableSettings
        {
            WritePermission = NetworkVariablePermission.Everyone,
            ReadPermission = NetworkVariablePermission.Everyone
        },true);
    [SerializeField]
    public float energyBoost=40;
    [SerializeField]
    public float maxRespawnTimeout = 5;
    private MeshRenderer meshRen;
    private Collider coll;
    private float currTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        meshRen = GetComponent<MeshRenderer>();
        coll = GetComponent<Collider>();
        currTime=maxRespawnTimeout;
    }

    // Update is called once per frame
    void Update()
    {
        if(hasSpawned.Value)return;
        meshRen.enabled=false;
        coll.enabled=false;
        currTime+=Time.deltaTime;
        if(currTime>maxRespawnTimeout){
            currTime=0;
            hasSpawned.Value=true;
            meshRen.enabled=true;
            coll.enabled=true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag!="Player")
        return;
        hasSpawned.Value=false;
        other.GetComponent<stats>().addCurrentEnergy(energyBoost);
        
    }
}
