using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabilizeRotation : MonoBehaviour
{
    Quaternion rotation = new Quaternion();
    // Start is called before the first frame update
    void Start()
    {
        rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
    transform.rotation = rotation;
    }
}
