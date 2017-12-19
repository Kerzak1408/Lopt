using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Difficulty : MonoBehaviour {

    Slider slider;
    GameObject Difficulties;
	// Use this for initialization
	void Start () {
        slider = GameObject.Find("Slider").GetComponent<Slider>();
        Difficulties = GameObject.Find("Difficulties");
        Color oldColor = this.GetComponent<Image>().color;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnClick()
    {
        foreach (Transform child in Difficulties.transform)
        {
            Color childColor = child.GetComponent<Image>().color;
            child.GetComponent<Image>().color = new Color(childColor.r, childColor.g, childColor.b, 0.2f);
        }
        Color oldColor = this.GetComponent<Image>().color;
        this.GetComponent<Image>().color = new Color(oldColor.r, oldColor.g, oldColor.b, 1);
        slider.value = int.Parse(name);
        Logic.mode = (int)slider.value;
    }
}
