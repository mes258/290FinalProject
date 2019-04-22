using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BallHover : MonoBehaviour
{
    private Rigidbody body;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        body.useGravity = false;
        StartCoroutine(delayFall());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            body.useGravity = true;
        }
    }

    IEnumerator delayFall()
    {
        yield return new WaitForSeconds(5f);
        body.useGravity = true;
    }
}
