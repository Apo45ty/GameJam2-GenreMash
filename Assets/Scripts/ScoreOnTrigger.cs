using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreOnTrigger : MonoBehaviour
{
    [SerializeField]
    private ScoreManager scoreManager;
    [SerializeField]
    private Teams team;

     private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Ball"){
            scoreManager.addScoreToTeam(1,team);
            other.transform.position = new Vector3(0,16.8f,0);
            Rigidbody rigidbody1 = other.GetComponent<Rigidbody>();
            if(rigidbody1)
                rigidbody1.velocity=Vector3.zero;
        }
    }
}
