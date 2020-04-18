using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Editor : MusicPlayer
{
    [Header("General")]
    public Text EditTime;
    public Text BPMCUT;
    public Text LineSlectDisplay;
    [Header("NoteTime")]
    public Text NTDisplay;
    public Text NTEdit;
    [Header("HoldTime")]
    public Text HTDisplay;
    public Text HTEdit;
    [Header("Editing")]
    public int LineSlect;

    public int NoteIDonLine;
    public static int EditingNoteIDonLine;

    public Note EditingDataDisplay;
    public static Note EditingData;

    public bool EditingisHold;
    public static bool HoldInd;

    public int NoteIDinStage;
    public static int EditingNoteIDinStage;

    public GameObject Editing;

    // Use this for initialization
    void Start () {
        Player_Start();
        NoteIDonLine = 0;
        EditingData = new Note();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Player_FixedUpdate();
        if (Input.anyKeyDown)
            Pause = true;
        if (Input.GetKeyDown(KeyCode.Space))
            Pause = false;
        DisPlayDataUpdate();

        EditorDataUpdate();




    }
    void DisPlayDataUpdate()
    {
        EditTime.text = "SongTime:" + NumberAddZero((int)MusicPlayer.StageTime / 60, 2) + ":" + NumberAddZero((int)MusicPlayer.StageTime % 60, 2);
        BPMCUT.text = "(" + ((MusicPlayer.StageTime - Stage.offset) / (Stage.BPM / 60)).ToString("F4") + ")";
        LineSlectDisplay.text = "LineSlect : " + LineSlect;
    }
    void EditorDataUpdate()
    {
        EditingisHold = HoldInd;
        NoteIDonLine = EditingNoteIDonLine;
        EditingDataDisplay = EditingData;
        NoteIDinStage = EditingNoteIDinStage;

        if (NoteChker[EditingData.line].Count - 1 >= EditingNoteIDonLine)
            Editing = NoteChker[EditingData.line][EditingNoteIDonLine];
    }
    public void SlectLine(int Line)
    {
        LineSlect = Line;
    }
}
