using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KickBall : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    public float kickDistance = 3f;
    public float kickForce = 300f;

    private float timeSpacePressed = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            timeSpacePressed = Time.time;
        }
        if (Input.GetKeyUp(KeyCode.Space) && IsBallNearby())
        {
            float diff = Time.time - timeSpacePressed;
            Vector3 kickPosition = transform.position;
            kickPosition.y = kickPosition.y - 1.2f;
            Vector3 direction = (ball.transform.position - kickPosition).normalized;
            ball.GetComponent<Rigidbody>().AddForce(direction * kickForce*diff);

        }
        /*
        if (Input.GetKey(KeyCode.Space) && IsBallNearby())
        {
            Vector3 kickPosition = transform.position;
            kickPosition.y = kickPosition.y - 1.2f;
            Vector3 direction = (ball.transform.position - kickPosition).normalized;
            ball.GetComponent<Rigidbody>().AddForce( direction * kickForce );
        }*/
    }

    bool IsBallNearby()
    {
        return Vector3.Distance(ball.transform.position, transform.position) < kickDistance;
    }
}
