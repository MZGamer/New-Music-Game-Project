using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {
    [Header("SongData")]
    public SongDifficultyCreate Data;
    public StageData Stage;
    public List<List<GameObject>> NoteChker = new List<List<GameObject>>();


    [Header("StageInformation")]
    public bool Editor;
    public bool Pause;
    public static float StageTime;
    public int Line1Mid, LineAndLine;
    public AudioSource BGMPlayer;
    public int Speed;
    public float NoteTop,NoteBottom;
    public static float StageBottom;
    public List<Transform> NoteFile = new List<Transform>();
    public List<GameObject> NoteOrigin = new List<GameObject>();
    public static int MoveSpeed;
    int[] count;
    int temp;



    // Use this for initialization
    public void Player_Start()
    {
        MusicPlayer.StageBottom = NoteBottom;
        MusicPlayer.MoveSpeed = Speed;
        Music_Start();
    }

    // Update is called once per frame
    public void Player_FixedUpdate () {
        NoteCreate();
        if(!Pause)
            MusicPlayer.StageTime += Time.fixedDeltaTime;

    }

    void Music_Start()
    {
        Stage = Data.Song;
        BGMPlayer.clip = Stage.BGM;
        count = new int[] { 0, 0 };
        NoteChker.Add(new List<GameObject>());
        NoteChker.Add(new List<GameObject>());
        NoteChker.Add(new List<GameObject>());
        NoteChker.Add(new List<GameObject>());
    }


    public void NoteCreate()
    {
        GameObject Ob;
        int[] maxchk = {Stage.N.Count,Stage.H.Count};
        int[] NoteLine = {0,0};
        float[] NoteTime = { 0, 0 };
        for(int i = 0; i < 2; i++)
        {
            if (count[i] < maxchk[i])
                if(i == 0)
                {
                    NoteLine[i] = Stage.N[count[i]].line;
                    NoteTime[i] = Stage.N[count[i]].Time;
                }
                else
                {
                    NoteLine[i] = Stage.H[count[i]].line;
                    NoteTime[i] = Stage.H[count[i]].Time;
                }

        }

        for (int k = 0; k <= 1; k++)
        {
            float NoteReady = NoteTime[count[k]] * (Stage.BPM / 60) + Stage.offset  - ( (NoteTop - NoteBottom) / MoveSpeed);
            if (NoteReady <= StageTime)
            {
                for (int i = count[k]; i < maxchk[k]; i++)
                {

                    Ob = Instantiate(NoteOrigin[k], NoteFile[k]);
                    Ob.transform.localPosition = new Vector2(Line1Mid + LineAndLine * NoteLine[k], Ob.transform.localPosition.y);

                    if (Editor)
                    {
                        EditNoitBeh DataID = Ob.GetComponent<EditNoitBeh>();
                        DataID.NoteIDonLine = NoteChker[NoteLine[k]].Count;
                        if (k == 1)
                            DataID.NoteData = Stage.H[count[k]];
                        else
                            DataID.NoteData = Stage.N[count[k]];
                        DataID.NoteIDinStageData = count[k];
                    }

                    NoteBehavior Data = Ob.GetComponent<NoteBehavior>();
                    Data.arrivetime = NoteTime[i] * (Stage.BPM / 60) + Stage.offset;
                    Debug.Log("ArriveTime" + Data.arrivetime + "Ready" + NoteReady + "Now" + MusicPlayer.StageTime);

                    if (k == 1)
                    {
                        Data.isHold = true;
                        Ob.transform.localScale = new Vector3(1, (Speed * (Stage.H[count[k]].EndTime - NoteTime[k])) / 100, 1);
                    }
                    NoteChker[NoteLine[k]].Add(Ob);
                    count[k] = i+1;

                }
            }                
        }      
    }

    public string NumberAddZero(int ReadyToAdd , int Count)
    {
        string AddZero = "";
        int Checker = ReadyToAdd,LessZero = 0;
        while (Checker/10 != 0)
        {
            Checker = Checker / 10;
            LessZero ++;
        }
        Count -= (LessZero + 1);
        for(int i = 0;i<Count; i++)
        {
            AddZero += "0";
        }
        AddZero += ReadyToAdd.ToString();
        return AddZero;
    }

}
