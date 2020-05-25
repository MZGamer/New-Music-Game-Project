using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlectChange : MonoBehaviour {
    [Header("BasicSetting")]
    public Transform Ob;
    public float[] perMove;
    public float Movetime;
    public int ID;
    public bool Moving;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
           
	}

    public float SelectSlide(float time)
    {
        float[] ts = { transform.position.x, transform.position.y };
        if (Mathf.Abs(ts[0]) <Mathf.Abs(perMove[0] * ID) || Mathf.Abs(ts[1]) < Mathf.Abs(perMove[1] * ID))
        {
            for(int i = 0; i < 2; i++)
            {
                if (ts[i] + (perMove[i] / Movetime) * Time.deltaTime > perMove[i] * ID)
                    ts[i] = perMove[i] * ID;
                else
                    ts[i] += (perMove[i] / Movetime) * Time.deltaTime;
            }
            Ob.transform.position = new Vector2(ts[0], ts[1]);

        }
        time -= Time.deltaTime;
        if (time >= 0)
            return SelectSlide(time);
        else
        {
            Moving = false;
            return 0;
        }

    }
    public void InputTrigger()
    {
        List<KeyCode> Key = new List<KeyCode>() {KeyCode.UpArrow,KeyCode.DownArrow };
        for(int i = 0; i < Key.Count; i++)
        {
            if (Input.GetKey(Key[i]) && !Moving)
            {
                if(i == 0 && ID++<=0)
                {
                    
                }
            }
        }


    }

}
