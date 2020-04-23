using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MusicPlayer
{
    [Header("KeySetting")]
    public List<KeyCode> Click  = new List<KeyCode>();
	// Use this for initialization
	void Start () {
        Player_Start();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Player_FixedUpdate();

    }

    void ClickChk()
    {
        for(int i = 0; i < 4; i++)
        {
            if (Input.GetKeyDown(Click[i]))
            {

            }
        }
    }
    
}
