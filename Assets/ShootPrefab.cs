using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkVariable;
using MLAPI.Messaging;
using System;

public class ShootPrefab : NetworkBehaviour
{
    public NetworkVariableBool isShooting = new NetworkVariableBool(new NetworkVariableSettings
        {
            WritePermission = NetworkVariablePermission.OwnerOnly,
            ReadPermission = NetworkVariablePermission.Everyone
        });
    public NetworkVariableVector3 transformParentPosition = new NetworkVariableVector3(new NetworkVariableSettings
        {
            WritePermission = NetworkVariablePermission.OwnerOnly,
            ReadPermission = NetworkVariablePermission.Everyone
        });
    public NetworkVariableVector3 transformForward = new NetworkVariableVector3(new NetworkVariableSettings
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
    private float timeout = 0;

    void Update(){
        // timeout+=Time.deltaTime;
        // if(IsOwner){
        //     transformParentPosition.Value = transform.parent.parent.position;
        //     transformForward.Value=transform.forward;
        //     if(Input.GetMouseButtonDown(0)){ 
        //        isShooting.Value=true;
        //     } else {
        //         if(timeout>3f){
        //             isShooting.Value = false;
        //             timeout=0;
        //         }
        //     }
        // }
        // if(isShooting.Value&&!previousIsShooting)
        //     hasShot=false;
        // if(isShooting.Value&&!hasShot){
        //     hasShot = true;
        //     Shoot(transformForward.Value,transformParentPosition.Value,CurrentlyEquipedWeapon);
        // }
        // previousIsShooting = isShooting.Value;
        if(IsOwner){
            if(Input.GetMouseButtonDown(0)){ 
               ShootServerRpc(transform.forward,transform.parent.parent.position,CurrentlyEquipedWeapon,transform.rotation);
            } 
            if(Input.GetKeyDown(KeyCode.Alpha1)){ 
               CurrentlyEquipedWeapon=GUNS.RocketLauncher;
            } 
            if(Input.GetKeyDown(KeyCode.Alpha2)){ 
               CurrentlyEquipedWeapon=GUNS.ShieldGun;
            } 
        }
    }

    [ServerRpc]
    void ShootServerRpc(Vector3 dirrection,Vector3 position,GUNS gun,Quaternion rotation){
        position += dirrection;
        switch(gun){
            case GUNS.RocketLauncher:
                GameObject gObj = Instantiate(shootPrefab,position,shootPrefab.transform.rotation);
                gObj.GetComponent<NetworkObject>().Spawn();
                gObj.transform.position = position;
                Rigidbody rb = gObj.AddComponent<Rigidbody>();
                rb.isKinematic=false;
                rb.AddForce(dirrection*ShootVelocity);
            break;
            case GUNS.ShieldGun:
                GameObject gObj2 = Instantiate(shieldPrefab,position,rotation);
                gObj2.transform.LookAt(position);
                gObj2.GetComponent<NetworkObject>().Spawn();

            break;
        }
    }
    
    void Shoot(Vector3 dirrection,Vector3 position,GUNS gun){
        print(" "+position);
        switch(gun){
            case GUNS.RocketLauncher:
                position += dirrection;
                if(IsServer){
                    GameObject gObj = Instantiate(shootPrefab,position,shootPrefab.transform.rotation);
                    gObj.GetComponent<NetworkObject>().Spawn();
                    gObj.transform.position = position;
                    Rigidbody rb = gObj.AddComponent<Rigidbody>();
                    rb.isKinematic=false;
                    rb.AddForce(dirrection*ShootVelocity);
                }
                
                break;
            case GUNS.ShieldGun:
                position += dirrection;
                GameObject gObj2 = Instantiate(shieldPrefab,position,transform.rotation);
                if(IsServer){
                    gObj2.GetComponent<NetworkObject>().Spawn();
                }
                break;
        }
    }

}
enum GUNS{
    RocketLauncher,
    ShieldGun
}
