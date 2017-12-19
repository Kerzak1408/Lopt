using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AdMobPlugin))]
public class Menu : MonoBehaviour
{

	private const string AD_UNIT_ID = "ca-app-pub-5173247295783358/8863921223";
	private const string INTERSTITIAL_ID = "ca-app-pub-5173247295783358/9795597629";

	private AdMobPlugin admob;
    private bool runInterstitial;

    private bool playClicked;

    GameObject Play;
    GameObject Tutorial;
    GameObject Exit;

    Vector3 originalSize;
    GameObject beingResized;

    bool growing;
    bool canceledClick;

    Slider slider;

	void Start()
    {
        canceledClick = false;
        GameObject Play = GameObject.Find("Play");
        GameObject Tutorial = GameObject.Find("Tutorial");
        GameObject Exit = GameObject.Find("Exit");

        slider = GameObject.Find("Slider").GetComponent<Slider>();

        growing = true;

        runInterstitial = true;
		admob = GetComponent<AdMobPlugin>();
		admob.CreateBanner(adUnitId: AD_UNIT_ID,
		                   adSize: AdMobPlugin.AdSize.SMART_BANNER,
		                   isTopPosition: true,
		                   interstitialId: INTERSTITIAL_ID,
		                   isTestDevice: false);
		admob.RequestAd();
        admob.ShowBanner();

        playClicked = false;
	}

    void Update()
    {
        if (!Highscore.changed && runInterstitial)
        {
            if (Random.value < 0.2f)
            {
                admob.RequestInterstitial();
            }
            runInterstitial = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) ||
            Input.GetKeyDown(KeyCode.Return))
        {
            PlayDown();
        }
    }

    //void ClickEffect(GameObject clicked)
    //{
    //    if (originalSize == Vector3.zero)
    //    {
    //        originalSize = clicked.transform.localScale;
    //    }
    //    if (growing)
    //    {
    //        Debug.Log("growing");
    //        if (clicked.transform.localScale.x < originalSize.x * 1.3f)
    //            clicked.transform.localScale *= 1.15f;
    //    }
    //    else
    //    {
    //        Debug.Log("NOT growing");
    //        if (clicked.transform.localScale.x > originalSize.x)
    //            clicked.transform.localScale /= 1.15f;
    //        else
    //        {
    //            originalSize = Vector3.zero;
    //            growing = true;
    //            beingResized = null;
    //            Logic.mode = (int) slider.value;
    //            admob.HideBanner();
    //            SceneManager.LoadScene("colors");
    //            PlayClickFinish();
    //        }
                
    //    }

    //}

	void OnEnable() {
		AdMobPlugin.AdClosed += () => { Debug.Log ("AdClosed"); };
		AdMobPlugin.AdFailedToLoad += () => { Debug.Log ("AdFailedToLoad"); };
		AdMobPlugin.AdLeftApplication += () => { Debug.Log ("AdLeftApplication"); };
		AdMobPlugin.AdOpened += () => { Debug.Log ("AdOpened"); };

		AdMobPlugin.InterstitialClosed += () => { Debug.Log ("InterstitialClosed"); };
		AdMobPlugin.InterstitialFailedToLoad += () => { Debug.Log ("InterstitialFailedToLoad"); };
		AdMobPlugin.InterstitialLeftApplication += () => { Debug.Log ("InterstitialLeftApplication"); };
		AdMobPlugin.InterstitialOpened += () => { Debug.Log ("InterstitialOpened"); };

		AdMobPlugin.AdLoaded += HandleAdLoaded;
		AdMobPlugin.InterstitialLoaded += HandleInterstitialLoaded;
	}

    private void OnDisable()
    {
		AdMobPlugin.AdLoaded -= HandleAdLoaded;
		AdMobPlugin.InterstitialLoaded -= HandleInterstitialLoaded;
	}

    private void HandleAdLoaded()
    {
#if !UNITY_EDITOR
		admob.ShowBanner();
#endif
	}

	private void HandleInterstitialLoaded()
    {
#if !UNITY_EDITOR
		admob.ShowInterstitial();
#endif
	}

    public void PlayDown()
    {
        //beingResized = GameObject.Find("Play");
        Logic.mode = (int)slider.value;
        admob.HideBanner();
        SceneManager.LoadScene("colors");
    }


    public void LoadInfo()
    {
        admob.HideBanner();
        SceneManager.LoadScene("credits");
    }

    public void LoadAchievements()
    {
        admob.HideBanner();
        SceneManager.LoadScene("achievements");
    }

    //public void PointerUp()
    //{

    //    if (!canceledClick)
    //        growing = false;
    //    canceledClick = false;
    //}

    //private void PlayClickFinish()
    //{

    //}

    //public void ClickExit()
    //{
    //    if (beingResized != null)
    //        beingResized.transform.localScale = originalSize;
    //    originalSize = Vector3.zero;
    //    growing = true;
    //    beingResized = null;
    //    canceledClick = true;
    //}

    //public void TutorialClick()
    //{
    //    admob.HideBanner();
    //    PlayerPrefs.SetInt("firstTimeEver", 0);
    //    OnClick.firstClick = false;
    //    Application.LoadLevel(1);
    //}

    void OnGUI() {
//		if (GUI.Button(buttonPositionShowAds, "Show Ads")) {
//#if !UNITY_EDITOR
//			admob.ShowBanner();
//#endif
//		}

//		if (GUI.Button(buttonPositionHideAds, "Hide Ads")) {
//#if !UNITY_EDITOR
//			admob.HideBanner();
//#endif
//		}

//		if (GUI.Button(buttonPositionShowInterstitial, "Show Interstitial")) {
//#if !UNITY_EDITOR
//			admob.RequestInterstitial();
//#endif
//		}
	}

  
}
