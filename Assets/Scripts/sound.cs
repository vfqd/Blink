using UnityEngine;
using System.Collections;

public class sound : MonoBehaviour {
	public AudioSource audio;
	public AudioClip[] owlSounds;
	bool play = true;
	float heartWait=0.8f;
	// Use this for initialization
	void Start () {
		audio = transform.GetComponent<AudioSource> ();
		if (audio.name == "Static") {
			audio.Play ();
		}
	
	}
	
	// Update is called once per frame
	void Update () {
		
		
		if (play) {
			if (audio.name == "Crickets") {
				play = false;
				audio.Play ();
				StartCoroutine (waitCricketTime ());
			} else if (audio.name == "Owl") {
				play = false;
				generateRandomOwlSound ();
				audio.Play ();
				StartCoroutine (waitOwlSound ());
			} 
			else if(audio.name=="Heartbeat"){
				if (playerVars.notesCollected >= 1) {
					heartWait = 1 - 0.2f * playerVars.notesCollected;
					play = false;
					audio.Play ();
					StartCoroutine (waitHeartSound ());
				}
			}
			else {
				play = false;
				audio.Play ();
				StartCoroutine (waitAudioSound ());
			}
		}
	
	}
	void generateRandomOwlSound(){
		audio.clip=owlSounds[Random.Range(0,owlSounds.Length)];
	}

	IEnumerator waitCricketTime(){
		yield return new WaitForSeconds (audio.clip.length + 10);
		play = true;
	}

	IEnumerator waitOwlSound(){
		yield return new WaitForSeconds (audio.clip.length + Random.Range (5,15));
		play = true;
	}

	IEnumerator waitHeartSound(){
		yield return new WaitForSeconds (audio.clip.length + heartWait);
		play = true;
	}

	IEnumerator waitAudioSound(){
		yield return new WaitForSeconds (audio.clip.length);
		play = true;
	}
}
