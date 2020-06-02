using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSlectManager : SlectChange
{
    [Header("UI")]
    public Image SongImage;
    public Text HighScore,Rate,Perfect,Good,Miss;

    public int DiffNum;
    public List<SongPreviewObjectCreate> Previews = new List<SongPreviewObjectCreate>();
    // Start is called before the first frame update
    [Header("TempIndex")]
    public PlayerData Player;
    void Start()
    {
        SlectChangeStart();
        for(int i = 0;i<SlectableOb.Count;i++){
            Previews.Add(SlectableOb[i]as SongPreviewObjectCreate);
        }
    }

    // Update is called once per frame
    void Update()
    {
        SlectChangeUpdate();
    }

    void PreviewDataUpdate(){
        if(!Moving){
            SongImage.sprite = Previews[ID].Song.SongImage;
            int SongID = Previews[ID].Song.SongID;
            HighScoreData scoreData = Player.Record[SongID].Record[DiffNum];
            HighScore.text = "HighScore:" + NumberAddZero(scoreData.HighScore , 7);
            Rate.text = "Rate:" + (scoreData.HighScore/10000f).ToString("f2");
            Perfect.text = "Perfect:" +  NumberAddZero(scoreData.Perfect , 4);
            Good.text = "Good:" +  NumberAddZero(scoreData.Good , 4);
            Miss.text = "Miss:" +  NumberAddZero(scoreData.Miss , 4);
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
