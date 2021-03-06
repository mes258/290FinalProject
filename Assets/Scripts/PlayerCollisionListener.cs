﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionListener : MonoBehaviour
{
    private const int ENEMYLAYER = 11;

    private HUDController hud;
    private PlayerSFXPlayer sfx;

    [SerializeField]
    private MouseLook camera;

    private bool canhit = true;
    // Start is called before the first frame update
    void Start()
    {
        hud = GetComponent<HUDController>();
        sfx = GetComponent<PlayerSFXPlayer>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (canhit && hit.collider.gameObject.layer == ENEMYLAYER)
        {
            hud.addScore(1);
            sfx.playDamage();
            canhit = false;
            camera.shake(0.5f);
            StartCoroutine(delay());
        }
    }

    private IEnumerator delay()
    {
        yield return new WaitForSeconds(1f);
        canhit = true;
    }
}
