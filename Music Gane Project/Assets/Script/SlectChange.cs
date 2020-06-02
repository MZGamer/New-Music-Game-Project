using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlectChange : MonoBehaviour {
    [Header("BasicSetting")]
    public float[] perMove;
    public float Movetime,MoveCD,MoveSide;
    public int ID,MaxID;
    public bool Moving;
    public GameObject Ob;

    public List<ScriptableObject> SlectableOb = new List<ScriptableObject>();



	// Use this for initialization
	public void SlectChangeStart () {
		MaxID = SlectableOb.Count;
	}
	
	// Update is called once per frame
	public void SlectChangeUpdate () {
        InputTrigger();
	}

    public float SelectSlide(float time)
    {
        float[] ts = { Ob.transform.localPosition.x, Ob.transform.localPosition.y };
        if (Mathf.Abs(ts[0]) !=(Mathf.Abs(perMove[0] * ID)) || Mathf.Abs(ts[1])  !=(Mathf.Abs(perMove[1] * ID)))
        {
            for(int i = 0; i < 2; i++)
            {
                if (Mathf.Abs( (ts[i] + (perMove[i] / Movetime) * Time.deltaTime -  perMove[i] * ID)) < 0.1f)
                    ts[i] = perMove[i] * ID;
                else
                    ts[i] += (perMove[i] / Movetime) * Time.deltaTime * MoveSide;
            }
            Ob.transform.localPosition = new Vector2(ts[0], ts[1]);

        }
        time -= Time.deltaTime;
        if (time <= 0)
        {
            Moving = false;
            return 0;
        }
        else{
            return time;
        }

    }
    public void InputTrigger()
    {
        List<KeyCode> Key = new List<KeyCode>() {KeyCode.UpArrow,KeyCode.DownArrow };
        if(Moving){
            MoveCD = SelectSlide(MoveCD);
        }
        for(int i = 0; i < Key.Count; i++)
        {
            if (Input.GetKey(Key[i]) && !Moving)
            {
                if(i == 0 && ID+1 < MaxID)
                {
                    Moving = true;
                    ID+= 1;
                    MoveSide  = 1;
                    MoveCD = SelectSlide(Movetime);
                }
                else if (i == 1 && ID-1 >=0){
                    Moving = true;
                    ID-=1;
                    MoveSide = -1;
                    MoveCD = SelectSlide(Movetime);
                }
            }
        }


    }

}
