using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBehavior : MonoBehaviour {
    public float arrivetime,EndTime;

    // Use this for initialization
    public void Note_Start() {
        transform.localPosition = new Vector2(transform.localPosition.x, MusicPlayer.StageBottom + (arrivetime-MusicPlayer.StageTime) * (MusicPlayer.MoveSpeed) );
    }
	
	// Update is called once per frame
	public void Note_FixedUpdate () {
        transform.localPosition = new Vector2(transform.localPosition.x,transform.localPosition.y-(MusicPlayer.MoveSpeed)*Time.deltaTime);

	}

}
