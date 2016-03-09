using UnityEngine;
using System.Collections;

public class enemyMovement : MonoBehaviour {
	public GameObject player;//Passes in player model
	bool canMove;//Can the weeping angel move
	float followRadius;//Radius of which angel can spawn, decreases over time
	float fogRange;
	float minimumSpawnRange;
	AudioSource audio;
	public AudioClip[] suspenseSounds;
	public AudioClip[] statueSounds;
	public float statueMoveTime;

	bool enemyFlag;
	float spawnAngle;
	int inFront;
	Renderer r1;
	// Use this for initialization
	void Start () {
		audio = transform.GetComponent<AudioSource> ();
		canMove = true;
		followRadius = 50;
		fogRange = 30;
		minimumSpawnRange = 5;
		r1 = this.GetComponent<Renderer>();
		inFront = 1;
		spawnAngle = 180;
		enemyFlag = false;

	}
	
	// Update is called once per frame
	void Update () {
		seePlayer ();
		if( r1.isVisible && ((player.transform.position - transform.position).magnitude < 30)){
			if(enemyFlag ==false && seePlayer()){
				enemyFlag=true;
				audio.volume = 1;
				audio.clip = suspenseSounds [Random.Range (0, suspenseSounds.Length)];
				audio.Play ();
			}
		}
		if ((canMove && !r1.isVisible) || (canMove && (player.transform.position - transform.position).magnitude > 30)) {
			enemyFlag = false;
			StartCoroutine (Movement ());
		}
		StartCoroutine (ShrinkRadius ());
	}

	//Enemy spawn behaviour
	IEnumerator Movement(){
		canMove = false;

		if (followRadius < 30) {					//Second stage - Spawns out of vision (66% chance), 33% to spawn in vision, but then gets pushed back into fog distance
			float temp = followRadius;
			if (Random.Range (0, 3) != 2){			//66% chance, out of vision
				spawnAngle = 120;
				inFront = -1;
				Debug.Log ("He is in behind you!");
			} else {
				spawnAngle = 60;					//In vision
				inFront = 1;
				followRadius = 30;
				Debug.Log ("He is in front of you!");
			}

			audio.clip = statueSounds [Random.Range (0, statueSounds.Length)];
			//AudioSource.PlayClipAtPoint (audio.clip, transform.position);
			adjustStatueVolume();
			audio.Play ();

			randomPointOnCircle ();
			followRadius = temp;

		} else {									//Fist stage - enemy spawns in circle around enemy, radius is > minimumSpawnRange, doesn't matter if spawns in front (fog covers)
			randomPointOnCircle ();
		}

		transform.LookAt(player.transform.position);
		yield return new WaitForSeconds(statueMoveTime);
		canMove = true;
	}

	IEnumerator ShrinkRadius(){
		if(followRadius>minimumSpawnRange){
			followRadius = followRadius - 0.08f;	//20 minutes for radius to get from 100 to 5;
		}
		yield return new WaitForSeconds (10);
	}

	void randomPointOnCircle(){//Spawns enemy 
		Quaternion randAng = Quaternion.Euler (0, Random.Range (-spawnAngle, spawnAngle), 0);
		randAng = player.transform.rotation * randAng;
		Vector3 spawnPos = player.transform.position+randAng*Vector3.forward*followRadius*inFront;
		transform.position=getYValue (spawnPos);
		//transform.position = spawnPos;
	}

	Vector3 getYValue(Vector3 pos){
		Vector3 newSpawnPos = pos;
		newSpawnPos.y = 1000;
		RaycastHit hit;
		if(Physics.Raycast(newSpawnPos,Vector3.down,out hit,1000f)){
			newSpawnPos.y = hit.point.y+1;
		}
		return newSpawnPos;
	}

	bool seePlayer(){

		RaycastHit hit;
		if (Physics.Raycast (player.transform.position, transform.position - player.transform.position, out hit)) {
			Debug.DrawRay (player.transform.position, transform.position - player.transform.position, Color.green);
			if (hit.transform.gameObject.name == "Enemy") {
				return true;
			} else {
				return false;
			}
			Debug.Log (hit.transform.gameObject.name);
			return false;
		} else {
			return false;
		}
	}

	void adjustStatueVolume(){
		if (followRadius == 30) {
			audio.volume = 0;
		} else {
			audio.volume = (-0.04f * followRadius) + 1.2f;
		}
	}
}
