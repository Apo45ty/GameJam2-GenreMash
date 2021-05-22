using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;
using UnityEngine;

namespace HelloWorld
{
    public class HelloWorldPlayer : NetworkBehaviour
    {
        public NetworkVariableVector3 Position = new NetworkVariableVector3(new NetworkVariableSettings
        {
            WritePermission = NetworkVariablePermission.ServerOnly,
            ReadPermission = NetworkVariablePermission.Everyone
        });
        [SerializeField]
        private float MOVE_SPEED =10;

        public override void NetworkStart()
        {
        }

        void Update()
        {
            
            //Move player in game
            transform.position = Position.Value;
            //Send inputs to server
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            if(IsOwner&&(Mathf.Abs(horizontal)>0.01f||Mathf.Abs(vertical)>0.01f)){
                movePlayerServerRpc(horizontal,vertical);
            }
        }
        
        [ServerRpc]
        void movePlayerServerRpc(float horizontal,float vertical){
            if(!NetworkManager.Singleton.IsServer)return;
            Position.Value += new Vector3(MOVE_SPEED*horizontal*Time.deltaTime,Position.Value.y,MOVE_SPEED*vertical*Time.deltaTime);
            print(horizontal+":"+vertical+" "+Position.Value);
            transform.position = Position.Value;
        }
    }
}