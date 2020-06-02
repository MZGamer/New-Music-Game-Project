using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class RecordData{
    public HighScoreData[] Record= new HighScoreData[4];
}

[System.Serializable]
public class HighScoreData
{
    public int HighScore,Perfect,Good,Miss;
}
