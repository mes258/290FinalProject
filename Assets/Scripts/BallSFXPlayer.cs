using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSFXPlayer : MonoBehaviour
{
    [SerializeField]
    AudioClip hitSound;

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

    public void playHit()
    {
        source.clip = hitSound;
        source.Play();
    }
}
