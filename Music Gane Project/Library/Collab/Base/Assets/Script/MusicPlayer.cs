using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {
    public SongDifficultyCreate Data;
    public StageData Stage;
    public AudioSource BGMPlayer;
    public GameObject Note, Hold;
    public float StageTime;
    public Transform HoldFile, NoteFile;
    public List<List<GameObject>> NoteChker = new List<List<GameObject>>();
    public int Line1Mid, LineAndLine;


	// Use this for initialization
	public void Music_Start () {
        Stage = Data.Song;
        BGMPlayer.clip = Stage.BGM;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void NoteCreate(int Num)
    {
        GameObject Ob;
        for(int i = 0;i < Num; i++)
        {
            int Line = Stage.N[i].line;
            Ob = Instantiate(Note, NoteFile);
            Ob.transform.position = new Vector2(Line1Mid + LineAndLine * (Line - 1),Ob.transform.position.y);
            NoteChker[Line].Add(Ob);
        }
    } 

}
