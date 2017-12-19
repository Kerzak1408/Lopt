using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Highscore : MonoBehaviour {

    public static bool changed;
    int phase;
    Vector3 originalScale;
    int lastMode;
    



    // Use this for initialization
    void Start () {


        lastMode = -1;
        originalScale = transform.localScale;
        //PlayerPrefs.DeleteAll();
        //Debug.Log("Logic mode" + Logic.mode);
        if (Logic.mode == 0)
        {
            GetComponent<Text>().text = "";
        }
        else 
        {
            GetComponent<Text>().text = "HIGH SCORE: " + PlayerPrefs.GetInt("highscore" + Logic.mode);
        }
        
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("Logic mode " + Logic.mode);
        if (lastMode != Logic.mode)
        {
            //// JUST THIS UPDATE FOR SAVING HIGHSCORE
            string patch = PlayerPrefs.GetString("patch");
            //GameObject.Find("Debugger").GetComponent<Text>().text += ("patch " + patch + "\n");
            if (patch != "2.0")
            {

                int highscore2 = PlayerPrefs.GetInt("highscore");
                //GameObject.Find("Debugger").GetComponent<Text>().text += "highscore == " + highscore2 + "\n";
                PlayerPrefs.SetInt("highscore2", highscore2);
                PlayerPrefs.SetString("patch", "2.0");
                PlayerPrefs.Save();
                //GameObject.Find("Debugger").GetComponent<Text>().text += "highscore2 == " +PlayerPrefs.GetInt("highscore2") +"\n"; 
            }

            //// JUST THIS UPDATE FOR SAVING HIGHSCORE

            if (Logic.mode != 0)
            {
                //Debug.Log("Highscore change: " + PlayerPrefs.GetInt("highscore" + Logic.mode));
                GetComponent<Text>().text = "HIGH SCORE: " + PlayerPrefs.GetInt("highscore" + Logic.mode);
                
            }
            else
            {
                GetComponent<Text>().text = "";
            }
            lastMode = Logic.mode;
        }
        if (changed && LastScore.calm)
        {
            switch (phase)
            {
                case 0:
                    {
                        if (transform.localScale.x > originalScale.x / 2)
                        {
                            transform.localScale *= 0.95f;
                        }
                        else
                            phase++;
                    }
                    break;
                case 1:
                    {
                        if (transform.localScale.x < originalScale.x * 4)
                        {
                            transform.localScale *= 1.15f;
                        }
                        else
                            phase++;
                    }
                    break;
                case 2:
                    {
                        if (transform.localScale.x > originalScale.x)
                        {
                            transform.localScale *= 0.95f;
                        }
                        else
                        {
                            phase = 0;
                            changed = false;
                        }
                    }
                    break;
            }
        }
	}
}
