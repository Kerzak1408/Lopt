using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SwitchColors : MonoBehaviour {

    Text text;

    // Use this for initialization
    void Start () {
        text = GetComponent<Text>();
	}

    // Update is called once per frame
    void Update() {
        int n = 50;
        text.color += new Color(Random.Range(-1f/n, 1f/n), Random.Range(-1f / n, 1f / n), Random.Range(-1f / n, 1f / n)); 
	}
}
