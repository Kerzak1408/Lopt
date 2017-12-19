using UnityEngine;
using System.Collections;

public class PausePlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        if (!Logic.paused)
         this.transform.localScale *= 1.5f;
    }

    void OnMouseUp()
    {
        if (!Logic.paused)
        {
            Logic.paused = true;
            this.transform.localScale /= 1.5f;
        }

    }
}
