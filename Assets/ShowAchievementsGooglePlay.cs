using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.UI;

public class ShowAchievementsGooglePlay : MonoBehaviour {

	// Use this for initialization
	void Start () {
#if UNITY_ANDROID
        PlayGamesPlatform.Activate();
#endif

    }

	// Update is called once per frame
	void Update () {
	
	}


    public void ShowAchievements()
    {
        if (!Social.localUser.authenticated)
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (!success)
                {
                    // Couldn't connect, should probably show an error
                    Debug.Log("Logging in; Failed");
                    GameObject.Find("LoginFailed").transform.localScale = LoginFailed.originalScale;
                    return;
                }
            });
        }

        int highscoreDevil = PlayerPrefs.GetInt("highscore3");
        //GameObject.Find("Debug").GetComponent<Text>().text += "\n Logic.highscoreDevil " + highscoreDevil;
        Social.ReportProgress(GooglePlayConfig.achievement_leviathan, 10 * highscoreDevil, (bool success) => { });
        Social.ReportProgress(GooglePlayConfig.achievement_mefistofeles, 2 * highscoreDevil, (bool success) => { });
        Social.ReportProgress(GooglePlayConfig.achievement_beelzebub, highscoreDevil, (bool success) => { });
        Social.ReportProgress(GooglePlayConfig.achievement_lucifer, highscoreDevil / 5, (bool success) => { });

        int highscoreNormal = PlayerPrefs.GetInt("highscore2");
        //GameObject.Find("Debug").GetComponent<Text>().text += "\n Logic.highscoreNormal " + highscoreNormal;
        Social.ReportProgress(GooglePlayConfig.achievement_extraordinary, 10 * highscoreNormal, (bool success) => { });
        Social.ReportProgress(GooglePlayConfig.achievement_rare, 2 * highscoreNormal, (bool success) => { });
        Social.ReportProgress(GooglePlayConfig.achievement_epic, highscoreNormal, (bool success) => { });
        Social.ReportProgress(GooglePlayConfig.achievement_phenomenal, highscoreNormal / 5, (bool success) => { });

        int highscoreRelax = PlayerPrefs.GetInt("highscore1");
        //GameObject.Find("Debug").GetComponent<Text>().text += "\n Logic.highscoreRelax " + highscoreRelax;
        Social.ReportProgress(GooglePlayConfig.achievement_talented, 10 * highscoreRelax, (bool success) => {});
        Social.ReportProgress(GooglePlayConfig.achievement_chiller, 2 * highscoreRelax, (bool success) => {  });
        Social.ReportProgress(GooglePlayConfig.achievement_patient, highscoreRelax, (bool success) => {  });
        Social.ReportProgress(GooglePlayConfig.achievement_nolife, highscoreRelax / 5, (bool success) => { });

        int games = PlayerPrefs.GetInt("games0");
        Social.ReportProgress(GooglePlayConfig.achievement_disciple, 100 * games, (bool success) => { });

        games = PlayerPrefs.GetInt("games1");
        //GameObject.Find("Debug").GetComponent<Text>().text += "\n games1 " + games;
        Social.ReportProgress(GooglePlayConfig.achievement_first_time_relaxing, 100 * games, (bool success) => { });
        Social.ReportProgress(GooglePlayConfig.achievement_enjoying_relaxing, 10 * games, (bool success) => { });
        Social.ReportProgress(GooglePlayConfig.achievement_devoted_to_relaxing, 2 * games, (bool success) => { });
        Social.ReportProgress(GooglePlayConfig.achievement_tranquil, games, (bool success) => { });
        Social.ReportProgress(GooglePlayConfig.achievement_stoic, games / 5, (bool success) => { });

        games = PlayerPrefs.GetInt("games2");
        //GameObject.Find("Debug").GetComponent<Text>().text += "\n games2 " + games;
        Social.ReportProgress(GooglePlayConfig.achievement_ordinary_one, 100 * games, (bool success) => { });
        Social.ReportProgress(GooglePlayConfig.achievement_fealty, 10 * games, (bool success) => { });
        Social.ReportProgress(GooglePlayConfig.achievement_loyal, 2 * games, (bool success) => { });
        Social.ReportProgress(GooglePlayConfig.achievement_lopts_friend, games, (bool success) => { });
        Social.ReportProgress(GooglePlayConfig.achievement_lopts_favourite, games / 5, (bool success) => { });

        games = PlayerPrefs.GetInt("games3");
        //GameObject.Find("Debug").GetComponent<Text>().text += "\n games3 " + games;
        Social.ReportProgress(GooglePlayConfig.achievement_first_sin, 100 * games, (bool success) => { });
        Social.ReportProgress(GooglePlayConfig.achievement_curious, 10 * games, (bool success) => { });
        Social.ReportProgress(GooglePlayConfig.achievement_playing_with_fire, 2 * games, (bool success) => { });
        Social.ReportProgress(GooglePlayConfig.achievement_tempted, games, (bool success) => { });
        Social.ReportProgress(GooglePlayConfig.achievement_hell_cartographer, games / 5, (bool success) => { });
        
        Social.ShowAchievementsUI();
    }
}
