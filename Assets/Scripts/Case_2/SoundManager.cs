using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public float pitchAmount = 1f;
    public AudioClip matchingClip;
    public AudioClip fallingClip;
    public AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        instance = GetComponent<SoundManager>();
        audio = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
           
    }

    public void PlayMatchingSound(float pitchAmount)
    {
        audio.pitch = pitchAmount;
        audio.PlayOneShot(matchingClip);
    }

    public void PlayFallingSound(float pitchAmount)
    {
        audio.pitch = pitchAmount;
        audio.PlayOneShot(fallingClip);
    }
}
