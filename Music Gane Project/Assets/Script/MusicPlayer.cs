using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {
    [Header("SongData")]
    public SongDifficultyCreate Data;
    public StageData Stage;
    public List<List<GameObject>> NoteChker = new List<List<GameObject>>();


    [Header("StageInformation")]
    public bool Editor;
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
        MusicPlayer.StageTime += Time.fixedDeltaTime;

    }

    void Music_Start()
    {
        Stage = Data.Song;
        BGMPlayer.clip = Stage.BGM;
        count = new int[] { 0, 0 };
        NoteChker.Add(new List<GameObject>() );
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
            float NoteReady = NoteTime[count[k]] * (Stage.BPM / 60) - (NoteTop / MoveSpeed) + 1;
            if (NoteReady <= StageTime)
            {
                for (int i = count[k]; i < maxchk[k]; i++)
                {

                    Ob = Instantiate(NoteOrigin[k], NoteFile[k]);
                    Ob.transform.position = new Vector2(Line1Mid + LineAndLine * NoteLine[k], Ob.transform.position.y);

                    if (Editor)
                    {
                        EditNoitBeh Data = Ob.GetComponent<EditNoitBeh>();
                        Data.NoteID = i;
                        Data.arrivetime = NoteTime[i] * (Stage.BPM / 60) + Stage.offset;
                    }
                    if (k == 1)
                    {
                        Ob.transform.localScale = new Vector3(1, (Speed * (Stage.H[count[i]].EndTime - NoteTime[i])) / 100, 1);
                    }
                    NoteChker[NoteLine[k]].Add(Ob);
                    count[k] = i;
                }
            }                
        }


      
    } 

}
