using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour {

    public AudioSource audioSourceIntro;
    public AudioSource audioSourceLoop;
    private bool startedLoop;

    void FixedUpdate()
    {
        if(!audioSourceIntro.isPlaying && !startedLoop)
        {
            audioSourceLoop.Play();
            startedLoop = true;
        }
    }
}
