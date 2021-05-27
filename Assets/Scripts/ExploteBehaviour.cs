using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;

public class ExploteBehaviour : NetworkBehaviour
{
    public NetworkVariableBool CanDestroy = new NetworkVariableBool(new NetworkVariableSettings
        {
            WritePermission = NetworkVariablePermission.OwnerOnly,
            ReadPermission = NetworkVariablePermission.Everyone
        },false);
    [SerializeField]
    private GameObject explosionEffecst;
    [SerializeField]
    private float ExplosionRadius=5f;
    [SerializeField]
    private float ExplosionForce=5f;
    private bool ran=false;

    void OnCollisionEnter(Collision collision)
    {
        if(ran)return;
        if(collision.gameObject.tag=="Player")return;
        ExplodeServerRpc();
        Instantiate(explosionEffecst,transform.position,explosionEffecst.transform.rotation);
        ran=true;
    }
    [ServerRpc]
    void ExplodeServerRpc(){
        Collider[] colliders = Physics.OverlapSphere(transform.position,ExplosionRadius);
        foreach(Collider nearbyObject in colliders){
           // print(nearbyObject.name);
            var rb = nearbyObject.GetComponent<Rigidbody>();
            if(rb!=null){
                rb.AddExplosionForce(ExplosionForce, transform.position,ExplosionRadius);
            }
        }
        GetComponent<NetworkObject>().Despawn();
        CanDestroy.Value=true;
    }
     void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, ExplosionRadius);
    }

    void Update(){
        if(CanDestroy.Value)
            Destroy(gameObject);
    }
}
