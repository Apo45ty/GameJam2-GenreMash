using MLAPI;
using UnityEngine;
using MLAPI.Transports.UNET;

namespace HelloWorld
{
    public class HelloWorldManager : MonoBehaviour
    {
        private static string ip="127.0.0.1";
        void Start(){
            Application.targetFrameRate = 30;
        }
        void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10, 10, 300, 300));
            if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
            {
                StartButtons();
            }
            else
            {
                StatusLabels();
            }

            GUILayout.EndArea();
        }

        static void StartButtons()
        {
            ip = GUILayout.TextField(ip,20);
            NetworkManager.Singleton.GetComponent<UNetTransport>().ConnectAddress =ip;
            if (GUILayout.Button("Host")) {
                NetworkManager.Singleton.StartHost();
            }
            if (GUILayout.Button("Client")){
                NetworkManager.Singleton.StartClient();
            } 
            if (GUILayout.Button("Server")){
                NetworkManager.Singleton.StartServer();
            } 
            
        }

        static void StatusLabels()
        {
            var mode = NetworkManager.Singleton.IsHost ?
                "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client";

            GUILayout.Label("Transport: " +
                NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name);
            GUILayout.Label("Mode: " + mode);
        }

        
    }
}