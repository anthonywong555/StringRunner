using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {
    AudioSource audioSource;

    public AudioClip playMusic;
    public AudioClip endMusic;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = playMusic;

        audioSource.Play();

    }
	
    public void EndMusic()
    {
        audioSource.Stop();
        audioSource.clip = endMusic;
        audioSource.loop = false;
        audioSource.Play();
    }
}
