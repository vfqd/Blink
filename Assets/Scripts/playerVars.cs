using UnityEngine;
using System.Collections;

public class playerVars : MonoBehaviour {
	//public float fogRadius = 0.05f;
	//public bool seeEnemy = false;
	// Use this for initialization

	public AudioClip noteSound;
	public static int notesCollected;//Player heartbeat start once first note is collected

	void Start () {

		notesCollected = 0;
		//RenderSettings.fog = true;
		//RenderSettings.fogDensity = fogRadius;

	}
	
	// Update is called once per frame
	void Update () {
		collectNote ();
	}

	void collectNote(){
		RaycastHit hit;
		if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit,3f)){
			//Debug.Log (hit.collider.tag);
			if ((Input.GetKey (KeyCode.Space) || Input.GetMouseButtonDown(0))&&hit.collider.tag=="Note") {
				notesCollected++;
				AudioSource.PlayClipAtPoint (noteSound, transform.position);
				Destroy (hit.collider.gameObject);
			}
		}
	}
		
}
