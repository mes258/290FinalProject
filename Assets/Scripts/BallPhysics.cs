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

        //Debug.Log(body.sleepThreshold);
        //body.sleepThreshold = body.sleepThreshold * 4;
    }

    void Update()
    {
        if(body.velocity.sqrMagnitude < 0.05)
        {
            body.velocity = new Vector3(0, body.velocity.y, 0);
            //Debug.Log("extra friction");
        }
        //Debug.Log(body.sleepThreshold + " current: " + Mathf.Pow(body.velocity.magnitude, 2) * 0.5f);
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
