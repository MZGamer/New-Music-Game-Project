using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Editor : MusicPlayer
{
    [Header("NoteTime")]
    public Text NTDisplay;
    public Text NTEdit;
    [Header("HoldTime")]
    public Text HTDisplay;
    public Text HTEdit;
    [Header("Editing")]
    public int EditNoteID;
    public static float Time, EndTime;

    // Use this for initialization
    void Start () {
        Player_Start();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Player_FixedUpdate();
	}
}
