using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPhysics : MonoBehaviour
{
    [SerializeField]
    private BallSFXPlayer sfx;

    private Rigidbody body;

    void Start()
    {
        body = GetComponent<Rigidbody>();

        body.sleepThreshold = body.sleepThreshold * 2;
    }

    void Update()
    {
        //Debug.Log(body.velocity);
        /*if (body.velocity.x > 0.0001f && body.velocity.z > 0.0001f && body.velocity.x < 0.05f && body.velocity.z < 0.05f)
        {
            //Debug.Log("old" + body.velocity);
            body.velocity = new Vector3(0, body.velocity.y, 0);
           //Debug.Log("new" + body.velocity);
        }*/
    }

    private void OnCollisionEnter(Collision col)
    { 
        //Debug.Log(col.gameObject);
        if(col.relativeVelocity.y > 3f)
        {
            sfx.playGroundHit();
        }
    }
}
