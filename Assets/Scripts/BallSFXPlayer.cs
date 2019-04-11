using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSFXPlayer : MonoBehaviour
{
    [SerializeField]
    AudioClip smallHitSound;

    [SerializeField]
    AudioClip largeHitSound;

    [SerializeField]
    AudioClip groundHitSound;

    AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSmallHit()
    {
        source.clip = smallHitSound;
        source.Play();
    }

    public void playLargeHit()
    {
        source.clip = largeHitSound;
        source.Play();
    }

    public void playGroundHit()
    {
        source.clip = groundHitSound;
        source.Play();
    }
}
