using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Author: Mengyu Chen, 2018
//For questions: mengyuchenmat@gmail.com
public class SoundPlay : MonoBehaviour {

	AudioSource audioSource;
	void Start () {
		audioSource = GetComponent<AudioSource>();
		audioSource.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
