using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    [SerializeField]
    public GameObject Ball;

    [SerializeField]
    private UIControl uiController;

    [SerializeField]
    private BallSFXPlayer ballSFX;

   //CapsuleCollider holeCollider;
    // Start is called before the first frame update
    void Start()
    { 
        //holeCollider = gameObject.GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Ball)
        {
            Debug.Log("BALL");
            uiController.showLevelEnd();

        }
        else
        {
            Debug.Log("PLAYER");
        }
    }
}
