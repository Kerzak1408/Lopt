using UnityEngine;
using System.Collections;

public class PointsCounter : MonoBehaviour {

    Vector3 originalLossy;
	// Use this for initialization
	void Start () {
        originalLossy = transform.lossyScale;
	}
	
	// Update is called once per frame
	void Update () {
        int newValue = Logic.points;
        int oldValue = int.Parse(GetComponent<TextMesh>().text);
        if (newValue != oldValue)
        {
            GetComponent<TextMesh>().text = Logic.points.ToString();
            transform.localScale *= (1 + newValue - oldValue);
        }
        else
        {
            if (transform.lossyScale.x > originalLossy.x)
            {
                transform.localScale *= 0.9f;
            }
        }
       
	}
}
