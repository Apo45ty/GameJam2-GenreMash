using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [SerializeField]
    private bool useXAxis=false;
     [SerializeField]
    private bool useYAxis=false;
    // Update is called once per frame
    void Update()
    {
        float verti = useXAxis?Input.GetAxis("Mouse X"):0;
        float horiz = useYAxis?-Input.GetAxis("Mouse Y"):0;
        this.transform.eulerAngles += new Vector3(horiz,verti,0);
    }
}
