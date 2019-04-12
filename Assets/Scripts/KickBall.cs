using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class KickBall : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    //[SerializeField] private GameObject powerMeter;
    //[SerializeField] private Text scoreLabel;
    [SerializeField] private BallSFXPlayer ballSFX;
    [SerializeField] private PlayerSFXPlayer playerSFX;

    public float kickDistance = 3f;
    public float kickForce = 300f;
    public float canKickAngle = 45f;
    public float kickYComponent = 1000f;
    private float maxPower = 5;

    private Vector3 lastKnownPlayerLocation;
    private Vector3 lastKnownBallLocation;

    private bool cancelled = false;

    private float timeSpacePressed = 0;

    private HUDController hud;
    [SerializeField] private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        //powerMeter.transform.localScale = new Vector3(1.0f, 0f, 1.0f);
        //_score = 0;
        //scoreLabel.text = "Strokes: " + _score.ToString();
        lastKnownPlayerLocation = transform.localPosition;
        lastKnownBallLocation = ball.transform.localPosition;

        hud = GetComponent<HUDController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            cancelled = false;
            timeSpacePressed = Time.time;
            playerSFX.playPower();

        }
        if (Input.GetKey(KeyCode.Space) && !cancelled)
        {
            hud.setPower(Mathf.Min(Time.time - timeSpacePressed, maxPower));
            //powerMeter.transform.localScale = new Vector3(1.0f, Mathf.Min(Time.time - timeSpacePressed, maxPower), 1.0f);
        }
        if (Input.GetKeyUp(KeyCode.Space) && IsBallNearby() && isBallForward() && !cancelled)
        {
            playerSFX.stopPower();
            anim.SetInteger("playerState", 3);
            hud.setPower(0);
            //powerMeter.transform.localScale = new Vector3(1.0f, 0f, 1.0f);
            //resetPower();

            //log positions
            lastKnownPlayerLocation = transform.localPosition;
            lastKnownBallLocation = ball.transform.localPosition;

            //make direction
            float diff = Mathf.Min(Time.time - timeSpacePressed, maxPower);
            //Vector3 kickPosition = transform.position;
            //kickPosition.y = kickPosition.y - 1.2f;
            //Vector3 direction = (ball.transform.position - kickPosition).normalized;
            Vector3 direction = generateKickDirection();
            Debug.Log(direction);
            ball.GetComponent<Rigidbody>().AddForce(direction * kickForce * diff);

            if(diff >= maxPower / 3)
            {
                ballSFX.playLargeHit();
            }
            else
            {
                ballSFX.playSmallHit();
            }

            //_score += 1;
            hud.addScore(1);
            //scoreLabel.text = "Strokes: " + _score.ToString();
            //updateScore();
            //anim.SetInteger("playerState", 0);
        }

        else if (Input.GetKeyUp(KeyCode.Space) && !cancelled)
        {
            //powerMeter.transform.localScale = new Vector3(1.0f, 0f, 1.0f);
            //resetPower();
            hud.setPower(0);
            //_score += 1;
            //scoreLabel.text = "Strokes: " + _score.ToString();
            //updateScore();
            hud.addScore(1);
            playerSFX.playMiss();
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            cancelled = true;
            //resetPower();
            hud.setPower(0);
            playerSFX.playCancel();
        }



        if (Input.GetKeyUp(KeyCode.M))
        {
            transform.localPosition = lastKnownPlayerLocation;
            ball.transform.localPosition = lastKnownBallLocation;

            Rigidbody ballBody = ball.GetComponent<Rigidbody>(); 
            ballBody.velocity = Vector3.zero;
            ballBody.angularVelocity = Vector3.zero;
            ball.GetComponent<Rigidbody>().velocity = Vector3.zero;

            //Debug.Log("set Velocity");
            //_score += 1;
            hud.addScore(1);
            //scoreLabel.text = "Strokes: " + _score.ToString();
            //updateScore();
        }
        else if (transform.localPosition.y < 0)
        {
            transform.localPosition = lastKnownPlayerLocation;
        }
        else if (ball.transform.localPosition.y < 0)
        {
            ball.transform.localPosition = lastKnownBallLocation;
            //_score += 2;
            //scoreLabel.text = "Strokes: " + _score.ToString();
            //updateScore();
            hud.addScore(2);
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

    //void resetPower()
    //{
    //    powerMeter.transform.localScale = new Vector3(1.0f, 0f, 1.0f);
    //}



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
