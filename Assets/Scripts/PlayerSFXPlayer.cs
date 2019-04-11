using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerSFXPlayer : MonoBehaviour
{
    AudioSource walkSrc;
    AudioSource effectSrc;



    bool isWalking = false;
    [SerializeField]
    AudioClip walkSound;

    [SerializeField]
    AudioClip jumpStart;

    [SerializeField]
    AudioClip environmentHit;


    // Start is called before the first frame update
    void Start()
    {
        var sources = GetComponents<AudioSource>();
        walkSrc = sources[0].loop ? sources[0] : sources[1];
        effectSrc = sources[0].loop ? sources[1] : sources[0];
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

    public void playDamage()
    {
        effectSrc.clip = environmentHit;
        effectSrc.Play();
    }
}
