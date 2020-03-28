using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {
    public SongDifficultyCreate Data;
    public StageData Stage;
    public AudioSource BGMPlayer;
    public List<GameObject> NoteOrigin = new List<GameObject>();
    public float StageTime;
    public List<Transform> NoteFile = new List<Transform>();
    public List<List<GameObject>> NoteChker = new List<List<GameObject>>();
    public int Line1Mid, LineAndLine;

    int[] count;
    public int Speed;
    public static int MoveSpeed;
    public int CreateHigh;
    public int ClickLine;


    // Use this for initialization
    void Start()
    {
        MoveSpeed = Speed;
        Music_Start();
    }

    // Update is called once per frame
    void Update () {
        NoteCreate();

    }

    public void Music_Start()
    {
        Stage = Data.Song;
        BGMPlayer.clip = Stage.BGM;
        count = new int[] { 0, 0 };
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
                    NoteLine[i] = Stage.N.Count;
                    NoteTime[i] = Stage.N[count[i]].Time;
                }
                else
                {
                    NoteLine[i] = Stage.H.Count;
                    NoteTime[i] = Stage.H[count[i]].Time;
                }

        }

        for (int k = 0; k <= 1; k++)
        {
            for (int i = count[k]; i < maxchk[k]; i++)
            {
            Ob = Instantiate(NoteOrigin[k], NoteFile[k]);
            Ob.transform.position = new Vector2(Line1Mid + LineAndLine * NoteLine[k], Ob.transform.position.y);
            if(k == 1)
            {
                Ob.transform.localScale = new Vector3(1, (Speed * (Stage.H[count[i]].EndTime - NoteTime[i])) / 100, 1);
            }
            NoteChker[NoteLine[k]].Add(Ob);
            count[k] = i;
            }
        }


      
    } 

}
