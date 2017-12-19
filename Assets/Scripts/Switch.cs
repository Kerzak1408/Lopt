using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour {

    GameObject otherState;

	// Use this for initialization
	void Start () {
        GameObject Switch = GameObject.Find("Switch");
        if (this.name == "Off")
        {
            GetComponent<Renderer>().enabled = true;
            otherState = Switch.transform.Find("On").gameObject;
        }
        else
        {
            GetComponent<Renderer>().enabled = false;
            otherState = Switch.transform.Find("Off").gameObject;
        }

	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnMouseDown()
    {
        GetComponent<Renderer>().enabled = false;
        otherState.GetComponent<Renderer>().enabled = true;
    }
}
