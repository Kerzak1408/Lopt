using UnityEngine;
using System.Collections;

public class Gradient : MonoBehaviour {



	// Use this for initialization
	void Start () {
        transform.localScale =  transform.parent.localScale;
        transform.position = transform.parent.position;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
