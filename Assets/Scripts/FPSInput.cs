using UnityEngine;
using System.Collections;

// basic WASD-style movement control
// commented out line demonstrates that transform.Translate instead of charController.Move doesn't have collision detection

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour
{
    public const float baseSpeed = 6.0f;
    public bool walking = false;
    public bool standing = false;

    public float speed = 6.0f;
    public float sideSpeed = 4.0f;
    public float rotateSpeed = 4.0f;

    private float vertspeed = 0;
    public float gravity = -9.8f;
    public float maxFall = -20f;

    private CharacterController _charController;


    //private Animator Player;

    [SerializeField]
    private Animator anim;

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
    }

    void Update()
    {
        //transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, Input.GetAxis("Vertical") * speed * Time.deltaTime);
        float deltaX = Input.GetAxis("Horizontal") * sideSpeed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);
        //anim = GetComponentInChildren<Animator>();
        float EPSILON = 0.000000000000001f;
        if (System.Math.Abs(deltaX) < EPSILON && System.Math.Abs(deltaZ) < EPSILON)
        {
            anim.SetInteger("playerState", 1);

        }
        else
        {
            anim.SetInteger("playerState", 0);
        }
        vertspeed = Mathf.Clamp(vertspeed - (gravity * Time.deltaTime), maxFall, 100f);
        //Debug.Log(vertspeed);
        if(isGrounded())
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
               //Debug.Log("Jdown");
                vertspeed = 10;
            }

            else if(vertspeed < 0)
            {
                vertspeed = 0;
            }
        }
        movement.y = vertspeed;

        movement *= Time.deltaTime;

        movement = transform.TransformDirection(movement);
        _charController.Move(movement);

        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(new Vector3(0f, rotateSpeed, 0f));
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(new Vector3(0f, -1 * rotateSpeed, 0f));
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