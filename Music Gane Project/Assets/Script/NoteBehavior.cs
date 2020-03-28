using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBehavior : MonoBehaviour {
    public float arrivetime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public void Note_FixedUpdate () {
        transform.localPosition = new Vector2(transform.localPosition.x,transform.localPosition.y-(MusicPlayer.MoveSpeed)*Time.deltaTime);

	}

}
