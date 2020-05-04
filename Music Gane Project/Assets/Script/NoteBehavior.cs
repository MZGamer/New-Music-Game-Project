using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBehavior : MonoBehaviour {
    public float arrivetime,EndTime;
    public bool isHold,click;
    public void Start()
    {
        Note_Start();
    }
    public void Update()
    {
        Note_Update();
    }

    // Use this for initialization
    public void Note_Start() {
        Move();
    }
	
	// Update is called once per frame
	public void Note_Update () {
        if (isHold && EndTime - MusicPlayer.StageTime > -1)
            Move();
        else if (arrivetime - MusicPlayer.StageTime > -1 && !isHold)
            Move();
        else
            transform.localPosition = new Vector2(transform.localPosition.x, -3000);

    }
    private void FixedUpdate()
    {
        if (!isHold)
        {
            if (MusicPlayer.StageTime > arrivetime + 0.5f)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (MusicPlayer.StageTime > EndTime + 0.5f)
            {
                Destroy(gameObject);
            }
        }

    }

    public void Move()
    {
        transform.localPosition = new Vector2(transform.localPosition.x, MusicPlayer.StageBottom + (arrivetime - MusicPlayer.StageTime) * (MusicPlayer.MoveSpeed));
    }

}
