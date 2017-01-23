using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSounds : MonoBehaviour {

    AudioSource[] sources;
    int current = 0;

    // Use this for initialization
    void Start () {
        sources = GetComponents<AudioSource>();
    }
	
	// Update is called once per frame
	public void PlayStarSound () {
        sources[current].Play();
        current = (current + 1) % sources.Length;
	}
}
