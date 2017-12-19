using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Slide : MonoBehaviour {

    GameObject Difficulties;
    Slider slider;

	// Use this for initialization
	void Start () {
        Difficulties = GameObject.Find("Difficulties");
        slider = GetComponent<Slider>();
        //Debug.Log("Previous = " + PlayerPrefs.GetFloat("previousDifficulty"));
        slider.value = PlayerPrefs.GetFloat("previousDifficulty");
        OnValueChanged();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnValueChanged()
    {
        //Debug.Log("Slide changed to " + slider.value);
        GameObject highlighted = Difficulties.transform.Find(slider.value.ToString()).gameObject;
        HighLight(highlighted);
        PlayerPrefs.SetFloat("previousDifficulty", slider.value);
        Logic.mode = (int)slider.value;
    }

    void HighLight(GameObject highlighted)
    {
        foreach (Transform child in Difficulties.transform)
        {
            Color childColor = child.GetComponent<Image>().color;
            child.GetComponent<Image>().color = new Color(childColor.r, childColor.g, childColor.b, 0.2f);
        }
        Color oldColor = highlighted.GetComponent<Image>().color;
        highlighted.GetComponent<Image>().color = new Color(oldColor.r, oldColor.g, oldColor.b, 1);
        slider.value = int.Parse(highlighted.name);
    }
}
