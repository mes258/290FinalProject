using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class KickBall : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private BallSFXPlayer ballSFX;
    [SerializeField] private PlayerSFXPlayer playerSFX;
    [SerializeField] private MouseLook camera;

    public float kickDistance = 3f;
    public float kickForce = 300f;
    public float canKickAngle = 45f;
    public float kickYComponent = 1000f;

    private float maxPower = 5;
    
    private bool cancelled = false;

    private float timeSpacePressed = 0;

    private HUDController hud;
    [SerializeField] private Animator anim;
    private Mulligan mulligan;


    // Start is called before the first frame update
    void Start()
    {
        hud = GetComponent<HUDController>();
        mulligan = GetComponent<Mulligan>();
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
        }
        if (Input.GetKeyUp(KeyCode.Space) && IsBallNearby() && isBallForward() && !cancelled)
        {
            playerSFX.stopPower();
            anim.SetInteger("playerState", 3);
            hud.setPower(0);

            //log positions
            mulligan.SetPlayerPos(transform.position);
            mulligan.SetBallPos(ball.transform.position);

            //make direction
            float diff = Mathf.Min(Time.time - timeSpacePressed, maxPower);
            Vector3 direction = generateKickDirection();
            ball.GetComponent<Rigidbody>().AddForce(direction * kickForce * diff);

            camera.shake(diff / maxPower);

            if(diff >= maxPower / 3)
            {
                ballSFX.playLargeHit();
            }
            else
            {
                ballSFX.playSmallHit();
            }

            hud.addScore(1);
            StartCoroutine(delay());

        }

        else if (Input.GetKeyUp(KeyCode.Space) && !cancelled)
        {
            anim.SetInteger("playerState", 3);
            hud.setPower(0);
            hud.addScore(1);
            playerSFX.playMiss();
            StartCoroutine(delay());
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            cancelled = true;
            hud.setPower(0);
            playerSFX.playCancel();
        }
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(1f);
        anim.SetInteger("playerState", 0);
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

        return Vector2.Angle(playerToBall, playerForward) < canKickAngle;
    }

    Vector3 generateKickDirection()
    {
        Vector2 playerToBall = getPlayerToBall();
        Vector2 playerForward = getPlayerForward();
        Vector2 dirInPlane = (playerForward * 0.5f + playerToBall * 0.5f).normalized;
        return new Vector3(dirInPlane.x, kickYComponent, dirInPlane.y);
    }
}
