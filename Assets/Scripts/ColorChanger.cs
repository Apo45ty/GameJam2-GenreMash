using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkVariable;

public class ColorChanger : NetworkBehaviour
{
    public NetworkVariableInt count = new NetworkVariableInt(new NetworkVariableSettings
        {
            WritePermission = NetworkVariablePermission.Everyone,
            ReadPermission = NetworkVariablePermission.Everyone
        },0);
    
    [SerializeField]
    private Material[] materials;
    private MeshRenderer ren;
    void Start()
    {
        ren = GetComponent<MeshRenderer>();
        ren.material = materials[0];
    }
    void Update(){
        ren.material = materials[count.Value];
    }
     void OnGUI()
        {
            if(!IsOwner)return;
            GUILayout.BeginArea(new Rect(0, 100, 300, 300));
            GUILayout.Label("You are on "+materials[count.Value].name+" Team");
            if (GUILayout.Button("ChangeTeam")) {
                count.Value=(count.Value+1)%materials.Length;
            }
            GUILayout.EndArea();
            
        }

}
