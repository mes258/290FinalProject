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
    private HUDController hud;

    [SerializeField]
    private BallSFXPlayer ballSFX;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Ball)
        {
            hud.endHole();
            uiController.showLevelEnd();
            ballSFX.playHoleEnter();
        }
    }
}
