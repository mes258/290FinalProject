using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerSFXPlayer : MonoBehaviour
{
    AudioSource walkSrc;
    AudioSource effectSrc;
    AudioSource hitSrc;


    bool isWalking = false;
    [SerializeField]
    AudioClip walkSound;

    [SerializeField]
    AudioClip jumpStart;

    [SerializeField]
    AudioClip environmentHit;

    [SerializeField]
    AudioClip jumpEnd;

    [SerializeField]
    AudioClip power;

    [SerializeField]
    AudioClip miss;

    [SerializeField]
    AudioClip cancel;


    // Start is called before the first frame update

    void Start()
    {
        var sources = GetComponents<AudioSource>();
        walkSrc = sources[0];
        walkSrc.loop = true;
        effectSrc = sources[1];
        hitSrc = sources[2];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playWalk()
    {
        if(!isWalking)
        {
            isWalking = true;
            walkSrc.clip = walkSound;
            walkSrc.loop = true;
            walkSrc.Play();
        }
    }

    public void stopWalk()
    {
        isWalking = false;
        walkSrc.Stop();
    }

    public void jump()
    {
        effectSrc.clip = jumpStart;
        effectSrc.Play();
    }

    public void land()
    {
        effectSrc.clip = jumpEnd;
        effectSrc.Play();
    }

    public void playDamage()
    {
        effectSrc.clip = environmentHit;
        effectSrc.Play();
    }

    public void playPower()
    {
        hitSrc.Stop();
        hitSrc.clip = power;
        hitSrc.Play();
    }

    public void stopPower()
    {
        hitSrc.Stop();
    }

    public void playMiss()
    {
        hitSrc.Stop();
        hitSrc.clip = miss;
        hitSrc.Play();
    }

    public void playCancel()
    {
        hitSrc.Stop();
        hitSrc.clip = cancel;
        hitSrc.Play();
    }
}
