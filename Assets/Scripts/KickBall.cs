using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class KickBall : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject powerMeter;
    [SerializeField] private Text scoreLabel;
    public float kickDistance = 3f;
    public float kickForce = 300f;
    public float canKickAngle = 45f;
    public float kickYComponent = 1000f;
    private float maxPower = 5;
    private int _score;
    private Vector3 lastKnownPlayerLocation;
    private Vector3 lastKnownBallLocation;

    private float timeSpacePressed = 0;

    // Start is called before the first frame update
    void Start()
    {
        powerMeter.transform.localScale = new Vector3(1.0f, 0f, 1.0f);
        _score = 0;
        scoreLabel.text = "Strokes: " + _score.ToString();
        lastKnownPlayerLocation = transform.localPosition;
        lastKnownBallLocation = ball.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            timeSpacePressed = Time.time;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            powerMeter.transform.localScale = new Vector3(1.0f, Mathf.Min(Time.time - timeSpacePressed, maxPower), 1.0f);
        }
        if (Input.GetKeyUp(KeyCode.Space) && IsBallNearby() && isBallForward())
        {
            powerMeter.transform.localScale = new Vector3(1.0f, 0f, 1.0f);
            lastKnownPlayerLocation = transform.localPosition;
            lastKnownBallLocation = ball.transform.localPosition;
            float diff = Mathf.Min(Time.time - timeSpacePressed, maxPower);
            Vector3 kickPosition = transform.position;
            kickPosition.y = kickPosition.y - 1.2f;
            Vector3 direction = (ball.transform.position - kickPosition).normalized;
            //Vector3 direction = generateKickDirection();
            Debug.Log(direction);
            ball.GetComponent<Rigidbody>().AddForce(direction * kickForce * diff);
            _score += 1;
            scoreLabel.text = "Strokes: " + _score.ToString();
        }else if (Input.GetKeyUp(KeyCode.Space))
        {
            powerMeter.transform.localScale = new Vector3(1.0f, 0f, 1.0f);

        }

        if(transform.localPosition.y < 0 || ball.transform.localPosition.y < 0)
        {
            transform.localPosition = lastKnownPlayerLocation;
            ball.transform.localPosition = lastKnownBallLocation;
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

    Vector2 positionInPlane(Vector3 vectorIn)
    {
        return new Vector2(vectorIn.x, vectorIn.z);
    }

    Vector2 getPlayerToBall()
    {
        Vector2 playerPos = positionInPlane(transform.position);
        Vector2 ballPos = positionInPlane(ball.transform.position);

        return (ballPos - playerPos).normalized;
    }

    Vector2 getPlayerForward()
    {
        return positionInPlane(transform.forward).normalized;
    }

    bool isBallForward()
    {

        Vector2 playerToBall = getPlayerToBall();
        Vector2 playerForward = getPlayerForward();

        Debug.Log(Vector2.Angle(playerToBall, playerForward));
        Debug.Log(Vector2.Angle(playerToBall, playerForward) < canKickAngle);
        return Vector2.Angle(playerToBall, playerForward) < canKickAngle;
    }

    Vector3 generateKickDirection()
    {
        Vector2 playerToBall = getPlayerToBall();
        Vector2 playerForward = getPlayerForward();

        Debug.Log(playerToBall);
        Debug.Log(playerForward);

       

        Vector2 dirInPlane = (playerForward * 0.5f + playerToBall * 0.5f).normalized;
        return new Vector3(dirInPlane.x, kickYComponent, dirInPlane.y);
    }
}
