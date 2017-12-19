using UnityEngine;
using System.Collections;



public class AchievementAlert : MonoBehaviour {

    public enum AlertState
    {
        DOWN,
        UP,
        INACTIVE,
        SHOWING
    }

    static AlertState alertState = AlertState.INACTIVE;

    float worldScreenHeight;
    float worldScreenWidth;
    float timePassed;

    void Start()
    {
        worldScreenHeight = Camera.main.orthographicSize * 2;
        worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        this.transform.localScale = new Vector3(1, 1, 1);
        this.transform.position = new Vector3(0, worldScreenHeight/2 + transform.lossyScale.y/2, 0);
    }

    //void Awake()
    //{
    //    DontDestroyOnLoad(transform.gameObject);
    //    this.transform.position = new Vector3(0, worldScreenHeight / 2 + transform.lossyScale.y / 2, 0);
    //}

    void Update()
    {
        switch (alertState)
        {
            case AlertState.DOWN:
                {
                    this.transform.localScale = new Vector3(1,2,1);
                    if (transform.position.y > worldScreenHeight / 2 - transform.lossyScale.y)
                    {
                        transform.position -= new Vector3(0, worldScreenHeight / 150, 0);
                    }
                    else
                    {
                        alertState = AlertState.SHOWING;
                    }
                }
                break;
            case AlertState.SHOWING:
                {
                    timePassed += Time.deltaTime;
                    if (timePassed > 1)
                    {
                        timePassed = 0;
                        alertState = AlertState.UP;
                    }

                }
                break;
            case AlertState.UP:
                {
                    if (transform.position.y < worldScreenHeight / 2 + transform.lossyScale.y)
                    {
                        transform.position += new Vector3(0, worldScreenHeight / 150, 0);
                    }
                    else
                    {
                        alertState = AlertState.INACTIVE;
                    }
                }
                break;
            case AlertState.INACTIVE:
                {
                    
                    this.transform.localScale = new Vector3(0, 0, 0);
                }
                break;
        }
         

    }

    public static void Activate()
    {
        alertState = AlertState.DOWN;
        
    }

    public static bool Active()
    {
        return alertState != AlertState.INACTIVE;
    }
}
