using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField]
    private float secondsBeforeDestroy = 3f;
    private float countdown;

    // Update is called once per frame
    void Update()
    {
        countdown += Time.deltaTime;
        if(countdown>secondsBeforeDestroy){
            Destroy(gameObject);
        }
    }
}
