using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;

public class ExploteBehaviour : NetworkBehaviour
{
    private bool CanDestroy = false;
    [SerializeField]
    private GameObject explosionEffecst;
    [SerializeField]
    private float ExplosionRadius=5f;
    [SerializeField]
    private float ExplosionForce=5f;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Player")return;
        ExplodeServerRpc();
        Instantiate(explosionEffecst,transform.position,explosionEffecst.transform.rotation);
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
        CanDestroy=true;
    }
     void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, ExplosionRadius);
    }

    void Update(){
        if(CanDestroy)
            Destroy(gameObject);
    }
}
