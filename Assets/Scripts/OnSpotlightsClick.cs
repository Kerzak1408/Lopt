using UnityEngine;
using System.Collections;

public class OnSpotlightsClick : MonoBehaviour
{
    public GameObject Red;
    public GameObject Green;
    public GameObject Blue;

    public static bool red_pressed;
    public static bool green_pressed;
    public static bool blue_pressed;
    public static bool reset;
    public static bool firstClick;
    bool firstTimeEver;

	// Use this for initialization
	void Start () {
        red_pressed = green_pressed = blue_pressed = false;
        reset = false;
    }
	
	// Update is called once per frame
	void Update () {
	    if (reset)
        {
            Red.transform.Find("Sphere").gameObject.GetComponent<Renderer>().material = Resources.Load<Material>("DarkRed");
            Green.transform.Find("Sphere").gameObject.GetComponent<Renderer>().material = Resources.Load<Material>("DarkGreen");
            Blue.transform.Find("Sphere").gameObject.GetComponent<Renderer>().material = Resources.Load<Material>("DarkBlue");
            reset = false;
        }
        if (!Logic.paused)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
        UpdateAndroid();
#else
        UpdateStandalone();
#endif
        }

    }

    private void UpdateAndroid()
    {
        Touch[] myTouches = Input.touches;
        for (int i = 0; i < Input.touchCount; i++)
        {
            Ray ray = Camera.main.ScreenPointToRay(myTouches[i].position);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray.origin, ray.direction, out hit, 100))
            {

                GameObject parentOfInterest = hit.collider.gameObject.transform.parent.gameObject;
                if (myTouches[i].phase == TouchPhase.Began)
                {
                    firstClick = true;
                    if (!Logic.paused)
                        parentOfInterest.transform.localScale *= 1.1f;
                }
                else if (myTouches[i].phase == TouchPhase.Ended)
                {
                    parentOfInterest.transform.localScale /= 1.1f;
                    switch (parentOfInterest.name)
                    {
                        case "Red":
                            {
                                red_pressed = !red_pressed;
                            }
                            break;
                        case "Green":
                            {
                                green_pressed = !green_pressed;
                            }
                            break;
                        case "Blue":
                            {
                                blue_pressed = !blue_pressed;
                            }
                            break;
                    }
                    ChangeHighlightOfButton(parentOfInterest);
                }
            }
        }
    }

    private void ChangeHighlightOfButton(GameObject parentOfInterest)
    {
        if (parentOfInterest == null) return;
        if (parentOfInterest.transform.Find("Sphere").gameObject.GetComponent<Renderer>().material.name == parentOfInterest.name + " (Instance)")
        {
            parentOfInterest.transform.Find("Sphere").gameObject.GetComponent<Renderer>().material = Resources.Load<Material>("Dark" + parentOfInterest.name);
        }
        else
        {
            parentOfInterest.transform.Find("Sphere").gameObject.GetComponent<Renderer>().material = Resources.Load<Material>(parentOfInterest.name);
        }
    }

    private void UpdateStandalone()
    {
        GameObject parentOfInterest = null;
        if (Input.GetKeyDown(KeyCode.A))
        {
            red_pressed = !red_pressed;
            parentOfInterest = Red;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            green_pressed = !green_pressed;
            parentOfInterest = Green;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            blue_pressed = !blue_pressed;
            parentOfInterest = Blue;
        }
        ChangeHighlightOfButton(parentOfInterest);
    }

    public static void Intro()
    {
        red_pressed = true;
        GameObject.Find("Red").transform.Find("Sphere").gameObject.GetComponent<Renderer>().material = Resources.Load<Material>("Red");
    }

    //public void OnMouseDown()
    //{
    //    firstClick = true;
    //    if (!Logic.paused)
    //        this.transform.localScale *= 1.1f;

    //}

    //public void OnMouseUp()
    //{
    //    if (!Logic.paused)
    //    {
    //        this.transform.localScale /= 1.1f;
    //        switch (name)
    //        {
    //            case "Red":
    //                {
    //                    red_pressed = !red_pressed;
    //                }
    //                break;
    //            case "Green":
    //                {
    //                    green_pressed = !green_pressed;
    //                }
    //                break;
    //            case "Blue":
    //                {
    //                    blue_pressed = !blue_pressed;
    //                }
    //                break;
    //        }
            //if (transform.FindChild("Sphere").gameObject.GetComponent<Renderer>().material.name == name + " (Instance)")
            //{
            //    transform.FindChild("Sphere").gameObject.GetComponent<Renderer>().material = Resources.Load<Material>("Dark" + name);
            //}
            //else
            //{
            //    transform.FindChild("Sphere").gameObject.GetComponent<Renderer>().material = Resources.Load<Material>(name);
            //}
    //    }

    //}
}
