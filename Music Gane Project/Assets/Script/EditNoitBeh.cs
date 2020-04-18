using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditNoitBeh : NoteBehavior {
    public int NoteIDonLine;
    public int NoteIDinStageData;
    public Note NoteData;

	// Use this for initialization
	void Start () {
        Note_Start();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Note_FixedUpdate();
	}

    public void BottomBeh()
    {
        Editor.EditingNoteIDonLine = NoteIDonLine;
        Editor.EditingData = NoteData;
        Editor.EditingNoteIDinStage = NoteIDinStageData;
        if (isHold)
            Editor.HoldInd = true;
        else
            Editor.HoldInd = false;


    }

}
