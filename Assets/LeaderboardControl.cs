using UnityEngine;
using System.Collections;
using GooglePlayGames;

public class LeaderboardControl : MonoBehaviour {
#if UNITY_ANDROID
    public string leaderboard = GooglePlayConfig.leaderboard_devil;

    // Use this for initialization
    void Start () {
        PlayGamesPlatform.Activate();
        //LeaderboardControl.OnAddScoreToLeaderBorad(GooglePlayConfig.leaderboard_devil, Logic.highscoreDevil);
        //LeaderboardControl.OnAddScoreToLeaderBorad(GooglePlayConfig.leaderboard_normal, Logic.highscoreNormal);
        //LeaderboardControl.OnAddScoreToLeaderBorad(GooglePlayConfig.leaderboard_relax, Logic.highscoreRelax);
    }

    public void ShowHighScore()
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

        int highscoreRelax = PlayerPrefs.GetInt("highscore1");
        LeaderboardControl.OnAddScoreToLeaderBorad(GooglePlayConfig.leaderboard_relax, highscoreRelax);
        int highscoreNormal = PlayerPrefs.GetInt("highscore2");
        LeaderboardControl.OnAddScoreToLeaderBorad(GooglePlayConfig.leaderboard_normal, highscoreNormal);
        int highscoreDevil = PlayerPrefs.GetInt("highscore3");
        LeaderboardControl.OnAddScoreToLeaderBorad(GooglePlayConfig.leaderboard_devil, highscoreDevil);

        Social.Active.ShowLeaderboardUI();
    }

    // Update is called once per frame
    void Update () {
       // OnShowLeaderBoard();

    }

    #region BUTTON_CALLBACKS
    /// <summary>
    /// Login In Into Your Google+ Account
    /// </summary>
    public void LogIn()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                Debug.Log("Login Sucess");
            }
            else {
                Debug.Log("Login failed");
            }
        });
    }
    /// <summary>
    /// Shows All Available Leaderborad
    /// </summary>
    public void OnShowLeaderBoard()
    {
        //        Social.ShowLeaderboardUI (); // Show all leaderboard
        
        ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(leaderboard); // Show current (Active) leaderboard
    }
    /// <summary>
    /// Adds Score To leader board
    /// </summary>
    public static void OnAddScoreToLeaderBorad(string leaderboardID, int score)
    {
        if (!Social.localUser.authenticated)
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (!success)
                {
                    // Couldn't connect, should probably show an error
                    Debug.Log("Logging in; Failed");

                    return;
                }
            });
        }

        Social.ReportScore(score, leaderboardID, (bool success) =>
        {
            if (success)
            {
                Debug.Log("Update Score Success");

            }
            else {
                Debug.Log("Update Score Fail");
            }
        });
        
    }
    /// <summary>
    /// On Logout of your Google+ Account
    /// </summary>
    public void OnLogOut()
    {
        ((PlayGamesPlatform)Social.Active).SignOut();
    }
    #endregion
#endif
}
