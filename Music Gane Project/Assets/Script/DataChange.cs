using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataChange : MonoBehaviour {

    public StageCreator OldStage;
    public SongDifficultyCreate NewStage;


	// Use this for initialization
	void Start () {

		for(int i = 0; i < OldStage.Stage.Notes.Count; i++)
        {
            List<NoteData> Line =new List<NoteData> { OldStage.Stage.Notes[i].Line1,OldStage.Stage.Notes[i].Line2,OldStage.Stage.Notes[i].Line3,OldStage.Stage.Notes[i].Line4 };
            for(int k = 0; k < 4; k++)
            {
                if (Line[k].active)
                {
                    Hold Note = new Hold();
                    Note.line = k;
                    Note.Time = OldStage.Stage.Notes[i].time;
                    if (Line[k].Hold)
                    {
                        Note.EndTime = Line[k].endtime;
                        NewStage.Song.H.Add(Note);
                    }
                    else
                    {
                        NewStage.Song.N.Add(Note);
                    }
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
