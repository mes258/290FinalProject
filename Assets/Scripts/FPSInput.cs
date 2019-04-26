using UnityEngine;
using System.Collections;

// basic WASD-style movement control
// commented out line demonstrates that transform.Translate instead of charController.Move doesn't have collision detection

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour
{
    private const float EPSILON = 0.001f;

    public const float baseSpeed = 6.0f;
    public bool walking = false;
    public bool standing = false;

    public float startspeed = 2.0f;
    public float accelartion = 1.5f;
    public float maxspeed = 6.0f; 
    private float speed;
    public float sideSpeed = 4.0f;

    public float minrotateSpeed = 2.0f;
    public float maxrotateSpeed = 270f;
    private float rotateSpeed;
    public float rotateAccel = 75f;

    private float vertspeed = 0;
    public float gravity = -9.8f;
    public float maxFall = -20f;
    public float jumpHeight = 10f;

    private CharacterController _charController;

    //private bool airborn = false;


    //private Animator Player;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private PlayerSFXPlayer sfx;

    private Mulligan mulligan;

    void Awake()
    {
        //Keep Player speed constant
        //Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }
    void OnDestroy()
    {
        //Keep Player speed constant
        //Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    void Start()
    {
        _charController = GetComponent<CharacterController>();
        //Player = GetComponentInChildren<Animator>();
        //rotateSpeed = 270f;
        mulligan = GetComponent<Mulligan>();
        speed = startspeed;
        rotateSpeed = minrotateSpeed;
    }

    void Update()
    {
        bool grounded = isGrounded();

        //transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, Input.GetAxis("Vertical") * speed * Time.deltaTime);
        float deltaX = Input.GetAxis("Horizontal") * sideSpeed;
        float zinput = Input.GetAxis("Vertical");
        float deltaZ;
        if(Mathf.Abs(zinput) < EPSILON)
        {
            speed = startspeed;
            rotateSpeed = minrotateSpeed;
            deltaZ = 0;
        }
        else
        {
            speed = Mathf.Clamp(speed + accelartion * Time.deltaTime, startspeed, maxspeed);
            rotateSpeed = Mathf.Clamp(rotateSpeed + rotateAccel * Time.deltaTime, minrotateSpeed, maxrotateSpeed);
            deltaZ = Input.GetAxis("Vertical") * speed;
        }



        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);
        //anim = GetComponentInChildren<Animator>();

        if (System.Math.Abs(deltaX) < EPSILON && System.Math.Abs(deltaZ) < EPSILON)
        {
            if (grounded && anim.GetInteger("playerState") != 3) 
            {
                anim.SetInteger("playerState", 0);
                sfx.stopWalk();
            }

            //Debug.Log("stopped");
        }
        else
        {
            if (grounded && vertspeed < 0.2f)
            {
                sfx.playWalk();
                anim.SetInteger("playerState", 1);
            }
        }
        vertspeed = Mathf.Clamp(vertspeed - (gravity * Time.deltaTime), maxFall, 100f);
        //Debug.Log(vertspeed);
        if(grounded)
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                //Debug.Log("Jdown");
                vertspeed = jumpHeight;
                anim.SetInteger("playerState", 2);
                sfx.stopWalk();
                sfx.jump();
            }

            else if(vertspeed < 0)
            {
                if(vertspeed < -4f)
                {
                    sfx.land();
                }
                vertspeed = -2f;
            }
        }
        movement.y = vertspeed;

        movement *= Time.deltaTime;

        movement = transform.TransformDirection(movement);

        if(!mulligan.checkPosResets())
        {
            _charController.Move(movement);
        }


        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(new Vector3(0f, rotateSpeed * Time.deltaTime, 0f));
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(new Vector3(0f, -1 * rotateSpeed * Time.deltaTime, 0f));
        }
    }

    private void OnSpeedChanged(float value)
    {
        speed = baseSpeed * value;
    }

    private bool isGrounded()
    {
        Vector3 top = transform.position + new Vector3(0, 1, 0);
        Vector3 down = new Vector3(0, -1, 0);

        int ballLayer = 10;
        int layermask = ~(1 << ballLayer);
        RaycastHit hit;

        return Physics.SphereCast(top, 1.1f, down, out hit, 1f, layermask);
    }
}