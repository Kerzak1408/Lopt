using UnityEngine;
using System.Collections;

public class AchievRefresh : MonoBehaviour {

    float timeElapsed;

	// Use this for initialization
	void Start () {
        timeElapsed = 0;
	}
	
	// Update is called once per frame
	void Update () {
        float previousTime = timeElapsed;
        timeElapsed += Time.deltaTime;
        if ((int)previousTime != (int)timeElapsed &&  Social.localUser.authenticated)
        {
            Social.ReportProgress(GooglePlayConfig.achievement_leviathan, 10 * Logic.highscoreDevil, (bool success) => { });
            Social.ReportProgress(GooglePlayConfig.achievement_mefistofeles, 2 * Logic.highscoreDevil, (bool success) => { });
            Social.ReportProgress(GooglePlayConfig.achievement_beelzebub, Logic.highscoreDevil, (bool success) => { });
            Social.ReportProgress(GooglePlayConfig.achievement_lucifer, Logic.highscoreDevil / 5, (bool success) => { });

            Social.ReportProgress(GooglePlayConfig.achievement_extraordinary, 10 * Logic.highscoreNormal, (bool success) => { });
            Social.ReportProgress(GooglePlayConfig.achievement_rare, 2 * Logic.highscoreNormal, (bool success) => { });
            Social.ReportProgress(GooglePlayConfig.achievement_epic, Logic.highscoreNormal, (bool success) => { });
            Social.ReportProgress(GooglePlayConfig.achievement_phenomenal, Logic.highscoreNormal / 5, (bool success) => { });


            Social.ReportProgress(GooglePlayConfig.achievement_talented, 10 * Logic.highscoreRelax, (bool success) => { });
            Social.ReportProgress(GooglePlayConfig.achievement_chiller, 2 * Logic.highscoreRelax, (bool success) => { });
            Social.ReportProgress(GooglePlayConfig.achievement_patient, Logic.highscoreRelax, (bool success) => { });
            Social.ReportProgress(GooglePlayConfig.achievement_nolife, Logic.highscoreRelax / 5, (bool success) => { });

            int games = PlayerPrefs.GetInt("games0");
            Social.ReportProgress(GooglePlayConfig.achievement_disciple, 100 * games, (bool success) => { });

            games = PlayerPrefs.GetInt("games1");
            Social.ReportProgress(GooglePlayConfig.achievement_first_time_relaxing, 100 * games, (bool success) => { });
            Social.ReportProgress(GooglePlayConfig.achievement_enjoying_relaxing, 10 * games, (bool success) => { });
            Social.ReportProgress(GooglePlayConfig.achievement_devoted_to_relaxing, 2 * games, (bool success) => { });
            Social.ReportProgress(GooglePlayConfig.achievement_tranquil, games, (bool success) => { });
            Social.ReportProgress(GooglePlayConfig.achievement_stoic, games / 5, (bool success) => { });

            games = PlayerPrefs.GetInt("games2");
            Social.ReportProgress(GooglePlayConfig.achievement_ordinary_one, 100 * games, (bool success) => { });
            Social.ReportProgress(GooglePlayConfig.achievement_fealty, 10 * games, (bool success) => { });
            Social.ReportProgress(GooglePlayConfig.achievement_loyal, 2 * games, (bool success) => { });
            Social.ReportProgress(GooglePlayConfig.achievement_lopts_friend, games, (bool success) => { });
            Social.ReportProgress(GooglePlayConfig.achievement_lopts_favourite, games / 5, (bool success) => { });

            games = PlayerPrefs.GetInt("games3");
            Social.ReportProgress(GooglePlayConfig.achievement_first_sin, 100 * games, (bool success) => { });
            Social.ReportProgress(GooglePlayConfig.achievement_curious, 10.01 * games, (bool success) => { });
            Social.ReportProgress(GooglePlayConfig.achievement_playing_with_fire, 2 * games, (bool success) => { });
            Social.ReportProgress(GooglePlayConfig.achievement_tempted, games, (bool success) => { });
            Social.ReportProgress(GooglePlayConfig.achievement_hell_cartographer, games / 5, (bool success) => { });
        }
       
    }
}
