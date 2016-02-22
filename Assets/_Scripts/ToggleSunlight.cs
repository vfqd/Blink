using UnityEngine;
using System.Collections;

public class ToggleSunlight : MonoBehaviour {

    public GameObject sunlight;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.L))
        {
            sunlight.SetActive(!sunlight.activeInHierarchy);
            RenderSettings.fog = !sunlight.activeInHierarchy;
        }
	}
}
