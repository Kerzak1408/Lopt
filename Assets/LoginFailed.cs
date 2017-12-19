using UnityEngine;
using System.Collections;

public class LoginFailed : MonoBehaviour {

    public static Vector3 originalScale;
    private float timeElapsed;

	// Use this for initialization
	void Start () {
        originalScale = transform.localScale;
        transform.localScale = new Vector3(0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.localScale != Vector3.zero && timeElapsed < 2)
        {
            timeElapsed += Time.deltaTime;
        }
        else
        {
            timeElapsed = 0;
            transform.localScale = new Vector3(0, 0, 0);
        }
        
	}
}
