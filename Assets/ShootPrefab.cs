using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkVariable;
using MLAPI.Messaging;

public class ShootPrefab : NetworkBehaviour
{
    public NetworkVariableBool isShooting = new NetworkVariableBool(new NetworkVariableSettings
        {
            WritePermission = NetworkVariablePermission.OwnerOnly,
            ReadPermission = NetworkVariablePermission.Everyone
        });
    [SerializeField]
    private GUNS CurrentlyEquipedWeapon=GUNS.RocketLauncher;
    [SerializeField]
    private GameObject shootPrefab;
    [SerializeField]
    private GameObject shieldPrefab;
    [SerializeField]
    private float ShootVelocity;
    private bool hasShot = false;

    private bool previousIsShooting = false;

    void Update(){
        if(IsOwner){
            if(Input.GetMouseButtonDown(0)){
               isShooting.Value=true;
            } else {
                isShooting.Value = false;
            }
        }
        if(!previousIsShooting&&isShooting.Value){
            hasShot = false;
        }
        if(isShooting.Value&&!hasShot){
            hasShot = true;
            Shoot(transform.forward,transform.parent.position,CurrentlyEquipedWeapon);
        }
        previousIsShooting = isShooting.Value;
    }
    
    void Shoot(Vector3 dirrection,Vector3 position,GUNS gun){
        switch(gun){
            case GUNS.RocketLauncher:
                position += dirrection;
                var gObj = Instantiate(shootPrefab,position,shootPrefab.transform.rotation);
                gObj.GetComponent<NetworkObject>().Spawn();
                gObj.transform.position = position;
                var rb = gObj.AddComponent<Rigidbody>();
                rb.AddForce(dirrection*ShootVelocity);
                break;
            case GUNS.ShieldGun:
                position += dirrection;
                var gObj2 = Instantiate(shieldPrefab,position,transform.rotation);
                gObj2.GetComponent<NetworkObject>().Spawn();
                
                break;
        }
    }
}
enum GUNS{
    RocketLauncher,
    ShieldGun
}
