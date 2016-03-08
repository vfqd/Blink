using UnityEngine;
using System.Collections;

public class AssignNotesScript : MonoBehaviour {

    [SerializeField] private GameObject[] noteLocations;
    [SerializeField] private Material[] materials;

    bool notesPopulated = false;
	
	// Update is called once per frame
	void Update () {
        if (!notesPopulated)
        {
            for (int i = 0; i < noteLocations.Length; i++)
            {
                if (Vector3.Distance(this.transform.position, noteLocations[i].transform.position) < 15)
                {
                    PopulateNotes(i);
                    Debug.Log("Populating Notes");
                    notesPopulated = true;
                }
            }
        }
	}

    void PopulateNotes(int nearNote)
    {
        noteLocations[nearNote].GetComponent<Renderer>().material = materials[4];

        int m = 0;
        for (int i = 0; i < noteLocations.Length; i++)
        {
            if (i != nearNote)
            {
                noteLocations[i].GetComponent<Renderer>().material = materials[m];
                m++;
            }
            
        }
    }
}
