using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MusicPlayer
{
    [Header("KeySetting")]
    public List<KeyCode> Click  = new List<KeyCode>();

    [Header("NoteClick Dect")]
    public float PerfectTime;
    public float GoodTime;
    bool[] holding = new bool[4];
    int[] ChkCount = new int[4];
    [Header("PlayerData")]
    public int MaxCombo;
    public int Combo,Perfect, Good, Miss,HoldMiss;
    public int Score;

    [Header("UI")]
    public Text ComboText;
    public Text ScoreText;

	// Use this for initialization
	void Start () {
        Player_Start();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Player_FixedUpdate();
        PlayingInfUpdate();
    }
    private void Update()
    {
        ClickChk();
        HoldChk();
    }
    void ClickChk()
    {
        for(int i = 0; i < 4; i++)
        {
            if(ChkCount[i] < NoteChker[i].Count)
            {
                NoteBehavior Data = NoteChker[i][ChkCount[i]];
                if (!Data.isHold && StageTime - Data.arrivetime > GoodTime)
                {
                    Data.click = true;
                    Destroy(Data.gameObject);
                    ChkCount[i]++;
                    Miss++;
                    Combo = 0;
                    continue;
                }

                if (Input.GetKeyDown(Click[i]))
                {
                    int[] Playing = { Perfect, Good };
                    float[] TimeChk = { PerfectTime, GoodTime };

                        for (int k = 0; k < 2; k++)
                        {
                            if (Mathf.Abs(StageTime - Data.arrivetime) < TimeChk[k])
                            {
                                Combo++;
                                Playing[k]++;
                                if (Data.isHold)
                                {
                                    Data.click = true;
                                    holding[i] = true;
                                }
                                else
                                {
                                    Data.click = true;
                                    Destroy(Data.gameObject);
                                    ChkCount[i]++;
                                }
                                break;
                            }
                        }
                        Debug.Log("Hit" + "Line: " + i);
                        Perfect = Playing[0];
                        Good = Playing[1];

                }
            }
        }
    }

    void HoldChk()
    {
        for(int i = 0;i < 4; i++)
        {
            float[] TimeChk = { PerfectTime, GoodTime };
            if (ChkCount[i] < NoteChker[i].Count)
            {

                NoteBehavior Data = NoteChker[i][ChkCount[i]];
                if (holding[i])
                {
                    if (Input.GetKeyUp(Click[i]))
                    {
                        holding[i] = false;
                        Destroy(Data.gameObject);
                        ChkCount[i]++;
                        if (StageTime - Data.EndTime < -GoodTime)
                        {
                            HoldMiss++;
                            Combo = 0;
                        }
                    }

                }
                if (StageTime - Data.EndTime > GoodTime &&Data.isHold)
                {
                    holding[i] = false;
                    Destroy(Data.gameObject);
                    ChkCount[i]++;
                }
            }

        }
    }
    
    void PlayingInfUpdate()
    {
        float PerNoteScore = 900000F / StageMaxCombo;
        float PerMaxComboScore = 100000F / StageMaxCombo;
        if (Combo > MaxCombo)
            MaxCombo = Combo;

        Score = (int)(PerNoteScore * Perfect + PerNoteScore * Good * 0.8F + PerMaxComboScore * MaxCombo);
        ComboText.text = Combo.ToString();
        ScoreText.text = NumberAddZero(Score,7);
    }
}
