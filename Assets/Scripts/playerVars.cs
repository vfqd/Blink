using UnityEngine;
using System.Collections;

public class playerVars : MonoBehaviour {
	//public float fogRadius = 0.05f;
	//public bool seeEnemy = false;
	// Use this for initialization
	void Start () {
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
				Destroy (hit.collider.gameObject);
				//Debug.Log ("Destroyed");
			}
		}
	}
}
