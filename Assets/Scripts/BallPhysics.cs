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
    }

    void Update()
    {
        if(body.velocity.sqrMagnitude < 0.05)
        {
            body.velocity = new Vector3(0, body.velocity.y, 0);
        }
  
    }

    private void OnCollisionEnter(Collision col)
    { 
        if(col.relativeVelocity.y > 3f)
        {
            sfx.playGroundHit();
        }
    }
}
