using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour {


    float worldScreenWidth;
    float worldScreenHeight;

    // Use this for initialization
    void Start () {

        worldScreenHeight = Camera.main.orthographicSize * 2;
        worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
