using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mulligan : MonoBehaviour
{
    [SerializeField]
    GameObject ball;

    [SerializeField]
    BallSFXPlayer sfx;

    [SerializeField]
    PlayerSFXPlayer playerSFX;

    HUDController hud;

    Vector3 lastKnownBallLocation;
    Vector3 lastKnownPlayerLocation;

    bool shouldFall = true;
    // Start is called before the first frame update
    void Start()
    {
        lastKnownPlayerLocation = transform.position;
        lastKnownBallLocation = ball.transform.position;

        hud = GetComponent<HUDController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool checkPosResets()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            sfx.playMulligan();
            hud.addScore(1);
            StartCoroutine(mulliganDelay());
            shouldFall = false;
            return true;
        }

        else
        {
            return checkOutOfBounds();
        }
    }

    IEnumerator mulliganDelay()
    {
        yield return new WaitForSeconds(2);
        transform.position = lastKnownPlayerLocation;
        ball.transform.position = lastKnownBallLocation;

        shouldFall = true;
        Rigidbody ballBody = ball.GetComponent<Rigidbody>();
        ballBody.velocity = Vector3.zero;
        ballBody.angularVelocity = Vector3.zero;

    }

    public void SetPlayerPos(Vector3 pos)
    {
        lastKnownPlayerLocation = pos;
    }

    public void SetBallPos(Vector3 pos)
    {
        lastKnownBallLocation = pos;
    }

    public bool checkOutOfBounds()
    {
        if (transform.position.y < 0)
        {
            transform.position = lastKnownPlayerLocation;
            playerSFX.playFall();
            hud.addScore(2);
            return true;
        }
        if (shouldFall && ball.transform.position.y < 0)
        {
            ball.transform.position = lastKnownBallLocation;

            Rigidbody ballBody = ball.GetComponent<Rigidbody>();
            ballBody.velocity = Vector3.zero;
            ballBody.angularVelocity = Vector3.zero;

            hud.addScore(2);
            sfx.playFall();
            return true;
        }

        return false;
    }
}
