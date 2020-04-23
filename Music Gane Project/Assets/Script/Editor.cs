﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Editor : MusicPlayer
{
    [Header("General")]
    public Text EditTime;
    public Text BPMCUT;
    public Text LineSlectDisplay;
    public Scrollbar TimeControl;
    [Header("NoteTime")]
    public Text NTDisplay;
    public InputField NTEdit;
    [Header("HoldTime")]
    public Text HTDisplay;
    public InputField HTEdit;
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
    public static bool DataChange;
    public static bool Slected;
    // Use this for initialization
    void Start () {
        Player_Start();

        DataChange = false;
        NoteIDonLine = 0;
        EditingData = new Note();
        Slected = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Player_FixedUpdate();
        if (Input.anyKeyDown)
        {
            Pause = true;
            BGMPlayer.Pause();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Pause = false;
            if (StageTime < 0)
            {
                BGMPlayer.time = 0;
                BGMPlayer.PlayDelayed(0 - StageTime);
            }
            else if (StageTime >= Stage.BGM.length)
            {

            }
            else
            {
                BGMPlayer.time = StageTime;
                BGMPlayer.Play();
            }

        }
        DisPlayDataUpdate();

        EditorDataUpdate();

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            Delete();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && StageTime <Stage.BGM.length)
            MoveTimewithKey(1);
        else if (Input.GetKeyDown(KeyCode.DownArrow) && StageTime > 0)
            MoveTimewithKey(-1);

    }
    void DisPlayDataUpdate()
    {
        EditTime.text = "SongTime:" + NumberAddZero((int)MusicPlayer.StageTime / 60, 2) + ":" + NumberAddZero((int)MusicPlayer.StageTime % 60, 2);
        BPMCUT.text = "(" + ((MusicPlayer.StageTime - Stage.offset) / (RealTime)).ToString("F4") + ")";
        LineSlectDisplay.text = "LineSlect : " + LineSlect;
    }
    void EditorDataUpdate()
    {
        if (Slected)
        {
            if (NoteChker[EditingData.line].Count - 1 >= EditingNoteIDonLine)
                Editing = NoteChker[EditingData.line][EditingNoteIDonLine].gameObject;
        }
        EditingisHold = HoldInd;
        NoteIDonLine = EditingNoteIDonLine;
        EditingDataDisplay = EditingData;
        NoteIDinStage = EditingNoteIDinStage;


        if (DataChange)
        {
            NTDisplay.text =EditingData.Time.ToString();
            NTEdit.text = NTDisplay.text;
            HTDisplay.text = "0.0";
            HTEdit.text = HTDisplay.text;
            if (EditingisHold)
            {
                HTDisplay.text = "" + ((EditingData as Hold).EndTime - EditingData.Time).ToString();
                HTEdit.text = HTDisplay.text;
            }
            DataChange = false;
        }

        if ((BGMPlayer.clip.length ) / (StageTime ) > 0 || (BGMPlayer.clip.length ) / (StageTime ) < 1)
        {
            if (!Pause)
                TimeControl.value = (StageTime ) / (BGMPlayer.clip.length );
            else
                StageTime = (BGMPlayer.clip.length * TimeControl.value) ;
        }
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Pause = true;
            BGMPlayer.Pause();
            TimeControl.value += 0.001f;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            Pause = true;
            BGMPlayer.Pause();
            TimeControl.value -= 0.001f;
        }



    }
    public void SlectLine(int Line)
    {
        LineSlect = Line;
    }
    public void CreateNote(int isHold)
    {
        GameObject Ob = Instantiate(NoteOrigin[isHold], NoteFile[isHold]);
        Hold NoteData = new Hold();
        NoteData.line = LineSlect;
        NoteData.Time = (MusicPlayer.StageTime - Stage.offset) / (RealTime);
        if (isHold == 1)
        {
            NoteData.EndTime = NoteData.Time + 0.25f;
        }


        Ob.transform.localPosition = new Vector2(Line1Mid + LineAndLine * LineSlect, Ob.transform.localPosition.y);



        EditNoitBeh DataID = Ob.GetComponent<EditNoitBeh>();
        DataID.NoteIDonLine = NoteChker[LineSlect].Count;
        if (isHold == 1)
        {
            DataID.NoteData = NoteData;
            DataID.NoteIDinStageData = Stage.H.Count;
            Stage.H.Add(NoteData);
            HoldInd = true;
        }
        else
        {
            DataID.NoteData = NoteData;
            DataID.NoteIDinStageData = Stage.N.Count;
            Stage.N.Add(NoteData);
            HoldInd = false;

        }
        EditingNoteIDonLine = DataID.NoteIDonLine;
        EditingData = NoteData;
        EditingNoteIDinStage = DataID.NoteIDinStageData;
        DataChange = true;
        Slected = true;


        NoteBehavior Data = Ob.GetComponent<NoteBehavior>();
        Data.arrivetime = NoteData.Time * RealTime + Stage.offset;
        if (isHold == 1)
        {
            Data.isHold = true;
            Ob.transform.localScale = new Vector3(1, (Speed * (NoteData.EndTime - NoteData.Time)) / 100, 1);
            Data.EndTime = NoteData.EndTime * RealTime + Stage.offset;
        }

        NoteChker[LineSlect].Add(Data);
    }

    public void Refresh()
    {

        for(int i = 0; i <= 1; i++)
        {
            var Target = new List<Note>();
            if (i == 0)
            {

                Target = Stage.N;
            }

            else
            {
                for (int k = 0; k < Stage.H.Count; k++)
                {
                    Target.Add(Stage.H[k]);
                }
            }

            for (int k = 0;k <= 100; k++)
            {
                for(int m = Target.Count - 1; m >= 1; m--)
                {
                    Note temp = new Note();
                    if (Target[m].Time < Target[m - 1].Time)
                    {
                        temp = Target[m];
                        Target[m] = Target[m - 1];
                        Target[m - 1] = temp;
                    }
                }
            }

            if (i == 0)
            {

                Stage.N = Target;
            }

            else
            {
                Stage.H.Clear();
                for (int k = 0; k < Target.Count; k++)
                {
                    Stage.H.Add(Target[k] as Hold);
                }
            }

            #if UNITY_EDITOR
                UnityEditor.AssetDatabase.Refresh();
            #endif
        }

        Data.Song.N = Stage.N;
        Data.Song.H = Stage.H;
    }
    public void Delete()
    {
        if (Slected)
        {
            int HoldInd = 0;
            NoteChker[EditingData.line][NoteIDonLine] = null;
            Destroy(Editing);
            if (EditingisHold)
                HoldInd = 1;
            maxchk[HoldInd]--;
            count[HoldInd]--;
            if (EditingisHold)
            {
                Stage.H.Remove(Stage.H[NoteIDinStage]);
            }
            else
            {
                Stage.N.Remove(Stage.N[NoteIDinStage]);
            }
            Data.Song.N = Stage.N;
            Data.Song.H = Stage.H;
            Slected = false;
        }

    }

    public void EditNote()
    {
        if (Slected)
        {
            EditNoitBeh Data = Editing.GetComponent<EditNoitBeh>();
            string ARTime = NTEdit.text , endtimeST= HTEdit.text;
            if(NTEdit.text == "")
            {
                ARTime = NTDisplay.text;
            }
            if(HTEdit.text == "")
            {
                endtimeST = HTDisplay.text;
            }

            float arrivetime = float.Parse(ARTime, System.Globalization.NumberStyles.AllowDecimalPoint);


            if (EditingisHold)
            {
                if(endtimeST == "0" || endtimeST == "")
                {
                    endtimeST = 0.25F.ToString();
                    HTEdit.text = "0.25";
                }
                float endtime = float.Parse(endtimeST, System.Globalization.NumberStyles.AllowDecimalPoint);
                Stage.H[NoteIDinStage].Time = arrivetime;
                Stage.H[NoteIDinStage].EndTime = arrivetime + endtime;
                Data.EndTime = (arrivetime + endtime) * (RealTime) + Stage.offset;
            }
            else
            {
                Stage.N[NoteIDinStage].Time = arrivetime;
            }

            Data.arrivetime = arrivetime * (RealTime) + Stage.offset;
            DataChange = true;
        }

    }

    public void MoveTime(int k)
    {
        if(k == 0)
        {
            Pause = true;
            BGMPlayer.Pause();
            StageTime = 0;
            BGMPlayer.time = 0;
            TimeControl.value = (StageTime) / (BGMPlayer.clip.length);

        }
        else if (k == 1)
        {
            Pause = true;
            BGMPlayer.Pause();
            StageTime = Stage.BGM.length;
            BGMPlayer.time = StageTime;
            TimeControl.value = (StageTime) / (BGMPlayer.clip.length);

        }
    }
    public void MoveTimewithKey(int i)
    {
        Pause = true;
        BGMPlayer.Pause();
        float BGMTime = (StageTime - Stage.offset) / RealTime;
        float ChangeTime = new float();
        if (Mathf.Abs(BGMTime % 0.25f) <= 0.0001)
            ChangeTime = ( (BGMTime / 0.25f) + i ) * 0.25f;
        else
            ChangeTime = ( (int)(BGMTime / 0.25f) + i) * 0.25f;
        StageTime = ChangeTime * RealTime + Stage.offset;
        BGMPlayer.time = StageTime;
        TimeControl.value = (StageTime) / (BGMPlayer.clip.length);

    }

}
