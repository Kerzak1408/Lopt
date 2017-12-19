using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{

    Vector3 originalScale;
    bool growing;

	// Use this for initialization
	void Start ()
    {
        growing = true;
        originalScale = GameObject.Find("Resume").transform.localScale;
        transform.localScale = new Vector3(0, 0, 0);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Logic.paused = !Logic.paused;
            if (!Logic.paused)
            {
                transform.localScale = new Vector3(0, 0, 0);
            }
            return;
        }
        if (Logic.paused)
        {
            if (growing)
            {
                if (transform.localScale == Vector3.zero)
                {
                    transform.localScale = originalScale;
                }
                else if (transform.localScale.x < originalScale.x * 1.2f)
                {
                    transform.localScale *= 1.005f;
                }
                else
                {
                    growing = false;
                }
            }
            else
            {
                if (transform.localScale.x > originalScale.x)
                {
                    transform.localScale /= 1.005f;
                }
                else
                {
                    growing = true;
                }
            }
        }
    }

    void OnApplicationPause()
    {
        Logic.paused = true;
    }

    public void OnClick()
    {
        transform.localScale = new Vector3(0, 0, 0);
        Logic.paused = false;
    }
}
