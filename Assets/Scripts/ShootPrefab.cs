using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkVariable;
using MLAPI.Messaging;
using System;

public class ShootPrefab : NetworkBehaviour
{
    [SerializeField]
    private stats status;
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
        if(IsOwner){
            if(Input.GetMouseButtonDown(0)){ 
                if(status){
                    float weaponCost = 0;
                    switch(CurrentlyEquipedWeapon){
                        case GUNS.RocketLauncher:
                            weaponCost=30;
                            break;
                        case GUNS.ShieldGun:
                            weaponCost=90;
                            break;
                    }
                    if(status.getCurrentEnergy()>=weaponCost){
                        status.deductEnergy(weaponCost);
                        ShootServerRpc(transform.forward,transform.parent.parent.position,CurrentlyEquipedWeapon,transform.rotation);
                    }
                }
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
