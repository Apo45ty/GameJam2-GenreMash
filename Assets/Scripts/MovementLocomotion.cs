using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementLocomotion : MonoBehaviour
{
    [SerializeField]
    private float MOVE_SPEED =10;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private float DampXAxis = 0.30f;
    [SerializeField]
    private float DampX=3;
    [SerializeField]
    private float DampZ=3;
    [SerializeField]
    private float DampZAxis=0.3f;
    [SerializeField]
    private float MaxSpeed=10f;


    void Start(){
            rb=GetComponent<Rigidbody>();
    }
    void FixedUpdate(){
            //Send inputs to server
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            movePlayerServerRpc(horizontal,vertical);
            
        }
   
        void movePlayerServerRpc(float horizontal,float vertical){
            float ZForce=0,XForce=0;
            if(Mathf.Abs(horizontal)<0.01f)
                rb.velocity=new Vector3(0,rb.velocity.y,rb.velocity.z);
            if(Mathf.Abs(vertical)<0.01f)
                 rb.velocity=new Vector3(rb.velocity.x,rb.velocity.y,0);

            if(Mathf.Abs(horizontal)>0.01f||Mathf.Abs(vertical)>0.01f){
                if(horizontal<-DampXAxis&&rb.velocity.x>DampX){
                    rb.velocity=Vector3.zero;
                } else if(horizontal>DampXAxis&&rb.velocity.x<-DampX){
                    rb.velocity=Vector3.zero;
                }
                if(vertical<-DampZAxis&&rb.velocity.z>DampZ){
                    rb.velocity=Vector3.zero;
                } else if(vertical>DampZAxis&&rb.velocity.z<-DampZ){
                    rb.velocity=Vector3.zero;
                }
                XForce=MOVE_SPEED*horizontal;
                ZForce=MOVE_SPEED*vertical;
                if(rb.velocity.x>MaxSpeed){
                    XForce=0;
                }
                if(rb.velocity.z>MaxSpeed){
                    ZForce=0;
                }
                rb.AddForce(new Vector3(XForce,0,ZForce));
            } else {
                rb.velocity=Vector3.zero;
            }
            print(horizontal+":"+vertical+" "+" " +ZForce+" , "+XForce);
            
        }
}
