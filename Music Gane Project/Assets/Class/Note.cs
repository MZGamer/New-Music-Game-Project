using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Note{
    public float Time;
    public int line;
}
[System.Serializable]
public class Hold : Note
{
    public bool Miss;
    public float EndTime;
}
