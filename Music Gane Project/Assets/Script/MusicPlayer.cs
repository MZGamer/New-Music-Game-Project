using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {
    [Header("SongData")]
    public SongDifficultyCreate Data;
    public StageData Stage;
    public List<List<GameObject>> NoteChker = new List<List<GameObject>>();

    [Header("ObjectNeedCreate")]
    public List<Transform> NoteFile = new List<Transform>();
    public List<GameObject> NoteOrigin = new List<GameObject>();
    public List<Transform> CreatePerBeatFile = new List<Transform>();
    public List<GameObject> CreatePerBeatOrigin = new List<GameObject>();
    public int[] CreatePerBeatCount;


    [Header("StageInformation")]
    public bool Editor;
    public bool Pause;
    public static float StageTime;
    public int Line1Mid, LineAndLine;
    public AudioSource BGMPlayer;
    public int Speed;
    public float NoteTop,NoteBottom;
    public static float StageBottom;
    public static int MoveSpeed;
    protected int[] count;
    protected int[] maxchk = {0 , 0};
    public bool Started = false;

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

        
        CreatePerBeatCount[0] = CreatePreBeat(CreatePerBeatCount[0], CreatePerBeatOrigin[0], CreatePerBeatFile[0]);

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

        maxchk = new int[] { Stage.N.Count, Stage.H.Count };

        BGMPlayer.PlayDelayed(1);
    }


    public void NoteCreate()
    {
        GameObject Ob;

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
        //Debug.Log("NextGenerate: Note: Line " + NoteLine[0] + " Time " + NoteTime[0] + " Hold: Line " + NoteLine[1] + " Time " + NoteTime[1]);
        for (int k = 0; k <= 1; k++)
        {
            float NoteReady = ( NoteTime[k] * (Stage.BPM / 60) + Stage.offset )  - ( (NoteTop - NoteBottom) / MoveSpeed);
            if (NoteReady <= StageTime)
            {
                if (count[k]< maxchk[k])
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
                    Data.arrivetime = NoteTime[k] * (Stage.BPM / 60) + Stage.offset;

                    if (k == 1)
                    {
                        Data.isHold = true;
                        Data.EndTime = (Stage.H[count[k]].EndTime) * (Stage.BPM / 60) + Stage.offset;
                        Ob.transform.localScale = new Vector3(1, ( Speed * ( (Stage.H[count[k]].EndTime - NoteTime[k])) * (Stage.BPM / 60) ) / 100, 1);
                    }
                    NoteChker[NoteLine[k]].Add(Ob);

                    count[k] += 1;
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

    public int CreatePreBeat(int count ,GameObject OB , Transform TS)
    {
        if ((count * 0.25 * (Stage.BPM / 60) + Stage.offset) - ((NoteTop - NoteBottom) / MoveSpeed) <= StageTime)
        {
            count++;
            GameObject Create =  Instantiate(OB, TS);
            Create.GetComponent<NoteBehavior>().arrivetime = (float)(count * 0.25 * (Stage.BPM / 60) + Stage.offset);
        }
        return count;
    }

}
