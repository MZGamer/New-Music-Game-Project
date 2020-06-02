using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerData
{
    public string UserName,UserPassword;
    public int Dan;
    public int Level;
    public List<RecordData> Record = new List<RecordData>();
    
    

}
