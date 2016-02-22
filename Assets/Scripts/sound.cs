﻿using UnityEngine;
using System.Collections;

public class sound : MonoBehaviour {
	AudioSource audio;
	public AudioClip[] owlSounds;
	bool play = true;
	// Use this for initialization
	void Start () {
		audio = transform.GetComponent<AudioSource> ();
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (play) {
			Debug.Log (audio.name);
			if (audio.name == "Crickets") {
				play = false;
				audio.Play ();
				StartCoroutine (waitCricketTime ());
			} else if (audio.name == "Owl") {
				Debug.Log ("HERE");
				play = false;
				generateRandomOwlSound ();
				audio.Play ();
				StartCoroutine (waitOwlSound ());
			} else {
				play = false;
				audio.Play ();
				StartCoroutine (waitAudioSound ());
			}
		}
	
	}
	void generateRandomOwlSound(){
		Debug.Log (owlSounds.Length);
		audio.clip=owlSounds[Random.Range(0,owlSounds.Length)];
	}

	IEnumerator waitCricketTime(){
		yield return new WaitForSeconds (audio.clip.length + 10);
		play = true;
	}

	IEnumerator waitOwlSound(){
		yield return new WaitForSeconds (audio.clip.length + Random.Range (5,10));
		play = true;
	}

	IEnumerator waitAudioSound(){
		yield return new WaitForSeconds (audio.clip.length);
		play = true;
	}
}
