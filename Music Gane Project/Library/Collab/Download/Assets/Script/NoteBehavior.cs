using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = new Vector2(transform.position.x,transform.position.y-(MusicPlayer.MoveSpeed)*Time.timeScale);
        Debug.Log("OwO");
	}

}
