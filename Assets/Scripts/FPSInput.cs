using UnityEngine;
using System.Collections;

// basic WASD-style movement control
// commented out line demonstrates that transform.Translate instead of charController.Move doesn't have collision detection

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour
{
    public const float baseSpeed = 6.0f;

    public float speed = 6.0f;
    public float sideSpeed = 4.0f;
    public float rotateSpeed = 4.0f;
    public float gravity = -9.8f;

    private CharacterController _charController;
    private Animator animator;

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
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        //transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, Input.GetAxis("Vertical") * speed * Time.deltaTime);
        float deltaX = Input.GetAxis("Horizontal") * sideSpeed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);

        movement.y = gravity;

        if (Input.GetKey(KeyCode.J))
        {
            if (transform.localPosition.y < 25)
            {
                movement.y = 15;
            }
        }

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
}