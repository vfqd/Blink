using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playerVars : MonoBehaviour {
	//public float fogRadius = 0.05f;
	//public bool seeEnemy = false;
	// Use this for initialization

	public AudioClip noteSound;
	public static int notesCollected;//Player heartbeat start once first note is collected

    public Text collectNotesText, numNotesCollectedText;
    public GameObject enemy;

	void Start () {

		notesCollected = 0;
        //RenderSettings.fog = true;
        //RenderSettings.fogDensity = fogRadius;
        Invoke("ClearStartingText", 5f);

	}

    void ClearStartingText()
    {
        collectNotesText.CrossFadeAlpha(0, 1, true);
    }

    void ClearNotesCollectedText()
    {
        numNotesCollectedText.CrossFadeAlpha(0, 1, true);
    }
	
	// Update is called once per frame
	void Update () {
        collectNote();
	}

	void collectNote(){
		RaycastHit hit;
		if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit,3f)){
			if ((Input.GetKey (KeyCode.Space) || Input.GetMouseButtonDown(0))&&hit.collider.tag=="Note") {
				notesCollected++;
                numNotesCollectedText.color = new Color(1, 1, 1, 0.63f);
				numNotesCollectedText.CrossFadeAlpha(0.63f, 0.00001f, true);
                numNotesCollectedText.text = notesCollected + "/5";
                Invoke("ClearNotesCollectedText", 2f);
                AudioSource.PlayClipAtPoint (noteSound, transform.position);
				Destroy (hit.collider.gameObject);
                
			}
		}
        if (notesCollected == 5)
        {
            Invoke("WinGame", 5f);
        }
	}

    void WinGame()
    {
        this.GetComponent<HealthAndStatic>().health = 0;
        Invoke("ShowDeathScreen", 5f);
    }

    void ShowDeathScreen()
    {
        enemy.transform.parent = this.transform;
        enemy.transform.localPosition = new Vector3(0, 0, 2f);
    }
		
}
