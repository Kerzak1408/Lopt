using UnityEngine;
using System.Collections;

public class MenuReflector : MonoBehaviour {

    LineRenderer lr;

    // Use this for initialization
    void Start () {
        lr = GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Particles/Additive"));
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, -transform.position/2);
        lr.SetWidth(0,1000);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
