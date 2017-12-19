using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Play()
    {
        PlayerPrefs.SetInt("firstTimeEver", 0);
        OnSpotlightsClick.firstClick = false;
        Application.LoadLevel(1);
    }
}
