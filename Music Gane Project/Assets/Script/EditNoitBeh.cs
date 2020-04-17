using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditNoitBeh : NoteBehavior {
    public int NoteID;

	// Use this for initialization
	void Start () {
        Note_Start();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Note_FixedUpdate();
	}

    public void BottomEvent()
    {

    }
}
