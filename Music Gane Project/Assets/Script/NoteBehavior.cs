using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBehavior : MonoBehaviour {
    public float arrivetime,EndTime;
    public bool isHold;
    public void Start()
    {
        Note_Start();
    }
    public void FixedUpdate()
    {
        Note_FixedUpdate();
    }

    // Use this for initialization
    public void Note_Start() {
        Move();
    }
	
	// Update is called once per frame
	public void Note_FixedUpdate () {
        if (isHold && EndTime - MusicPlayer.StageTime > -1)
            Move();
        else if (arrivetime - MusicPlayer.StageTime > -1 && !isHold)
            Move();
        else
            transform.localPosition = new Vector2(transform.localPosition.x, -3000);

    }

    public void Move()
    {
        transform.localPosition = new Vector2(transform.localPosition.x, MusicPlayer.StageBottom + (arrivetime - MusicPlayer.StageTime) * (MusicPlayer.MoveSpeed));
    }

}
