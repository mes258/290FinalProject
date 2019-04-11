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

    [SerializeField]
    AudioClip holeEnter;

    bool blockingPlay = false;
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
        if (!blockingPlay)
        {
            source.clip = groundHitSound;
            source.Play();
        }
    }

    public void playHoleEnter()
    {
        blockingPlay = true;
        source.clip = holeEnter;
        source.Play();
        StartCoroutine(delay());
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(1f);
        blockingPlay = false;
    }
}
