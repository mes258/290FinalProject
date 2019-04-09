using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RearViewCamera : MonoBehaviour
{
    [SerializeField]
    public GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 eulers = camera.transform.localEulerAngles;
        //transform.rotation = Quaternion.Inverse(camera.transform.rotation);
        //Debug.Log(transform.TransformDirection(transform.forward) + " " + transform.TransformDirection(camera.transform.forward));
    }
}
