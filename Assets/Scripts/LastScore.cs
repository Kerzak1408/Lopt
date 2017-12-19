using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LastScore : MonoBehaviour {

    Vector3 defaultScale;
    public static bool calm;
    int lastMode;

	// Use this for initialization
	void Start () {
        lastMode = -1;
        calm = true;
        defaultScale = transform.lossyScale;
	    if (Logic.endOfTurn)
        {
            GetComponent<Text>().text = "LAST: " + PlayerPrefs.GetInt("last" + Logic.mode);
            transform.localScale *= 20;
        }
        else
        {
            GetComponent<Text>().text = "";
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (lastMode != Logic.mode)
        {
            if (Logic.mode != 0)
            {
                //Debug.Log("Last change: " + PlayerPrefs.GetInt("last" + Logic.mode));
                GetComponent<Text>().text = "LAST: " + PlayerPrefs.GetInt("last" + Logic.mode);

            }
            else
            {
                GetComponent<Text>().text = "";
            }
            lastMode = Logic.mode;
        }
	    if (transform.lossyScale.x > defaultScale.x)
        {
            transform.localScale *= 0.92f;
            calm = false;
        }
        else
        {
            calm = true;
        }
	}
}
