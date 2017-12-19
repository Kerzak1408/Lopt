using UnityEngine;
using System.Collections;

public class MenuLopt : MonoBehaviour {

    float worldScreenWidth;
    float worldScreenHeight;
    float rotationAngle;
    Color[] colors3;

    // Use this for initialization
    void Start () {
        colors3 = new Color[7];
        colors3[0] = new Color(255f, 0, 0, 255f);
        colors3[1] = new Color(0, 255f, 0, 255f);
        colors3[2] = new Color(0, 0, 255f, 255f);
        colors3[3] = new Color(255f, 255f, 0, 255f);
        colors3[4] = new Color(255f, 0, 255f, 255f);
        colors3[5] = new Color(0, 255f, 255f, 255f);
        colors3[6] = new Color(255f, 255f, 255f, 255f);
        rotationAngle = 360;
        worldScreenHeight = Camera.main.orthographicSize * 2;
        worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
        transform.position = new Vector3(0, -worldScreenHeight / 2 + transform.lossyScale.y / 2, -1);
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x > worldScreenWidth / 2)
        {
            rotationAngle = 360;
            ChangeBallColor(colors3[Random.Range(0, colors3.Length)]);
        }
        else if (transform.position.x < -worldScreenWidth / 2)
        {
            rotationAngle = -360;
            ChangeBallColor(colors3[Random.Range(0, colors3.Length)]);
        }
        transform.position -= new Vector3(GetSpeedFromAngle(RecomputeBallAngle()), 0, 0);
        RotateBall();
    }

    private float GetSpeedFromAngle(float angle)
    {
        float result = ((angle) * 2 * Mathf.PI * transform.localScale.x / 2) / 360;
        //Debug.Log("speed: " + result);
        return result;

    }

    private void RotateBall()
    {
        transform.Rotate(new Vector3(0, 0, RecomputeBallAngle()));
    }

    private float RecomputeBallAngle()
    {
        float result = 0;
        result = rotationAngle * Time.deltaTime;
        return result;
    }

    private void ChangeBallColor(Color color)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.GetComponent<Renderer>().material.color = color;
        }
    }
}
