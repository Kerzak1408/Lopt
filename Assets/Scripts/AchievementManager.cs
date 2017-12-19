//using System;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;

//public class Achievement
//{
//    public int countToUnlock { get; set; }
//    public bool isUnlocked { get; set; }
//    public string Message { get; set; }
//    public int Reward { get; set; }
//}

//public enum AchievementType
//{
//    Tutorial,
//    Relax,
//    Normal,
//    Devil,
//    HighscoreRelax,
//    HighscoreNormal,
//    HighscoreDevil
//};

//public class AchievementEventArg : EventArgs
//{
//    public Achievement Data;
//    public AchievementEventArg(Achievement e)
//    {
//        Data = e;
//    }
//}

//public class AchievementsManager
//{
//    public Dictionary<AchievementType, List<Achievement>> _achievements;
//    public Dictionary<AchievementType, int> _achievementKeeper;

//    public event EventHandler AchievementUnlocked;

//    float worldScreenHeight = Camera.main.orthographicSize * 2;
//    float worldScreenWidth = Camera.main.orthographicSize * 2 / Screen.height * Screen.width;

//    protected void RaiseAchievementUnlocked(Achievement ach)
//    {
//        Debug.Log("Achievement unlocked: " + ach.Message);
//        PlayerPrefs.SetInt(ach.Message, 1);
//        GameObject achievementAlert = GameObject.Find("AchievementAlert");
//        achievementAlert.GetComponent<TextMesh>().text = "ACHIEVEMENT UNLOCKED: \n" + ach.Message;
//        AchievementAlert.Activate();
//        // unlock the event
//        ach.isUnlocked = true;
        
//        var del = AchievementUnlocked as EventHandler;
//        if (del != null)
//        {
//            del(this, new AchievementEventArg(ach));
//        }
//    }

//    public AchievementsManager()
//    {
//        _achievementKeeper = new Dictionary<AchievementType, int>();
        
        

//        _achievements = new Dictionary<AchievementType, List<Achievement>>();


//        _achievementKeeper[AchievementType.Tutorial] = PlayerPrefs.GetInt("games0");
//        _achievementKeeper[AchievementType.Relax] = PlayerPrefs.GetInt("games1");
//        _achievementKeeper[AchievementType.Normal] = PlayerPrefs.GetInt("games2");
//        _achievementKeeper[AchievementType.Devil] = PlayerPrefs.GetInt("games3");
//        _achievementKeeper[AchievementType.HighscoreRelax] = PlayerPrefs.GetInt("highscore1");
//        _achievementKeeper[AchievementType.HighscoreNormal] = PlayerPrefs.GetInt("highscore2");
//        _achievementKeeper[AchievementType.HighscoreDevil] = PlayerPrefs.GetInt("highscore3");


//        Debug.Log("achKeeper == " + _achievementKeeper[AchievementType.Tutorial] + " " +  _achievementKeeper[AchievementType.Relax] + 
//            " " + _achievementKeeper[AchievementType.Normal] + " " + _achievementKeeper[AchievementType.Devil]);

//        var listTurorial = new List<Achievement>();
//        string achText;
//        achText = "Play Tutorial!";
//        listTurorial.Add(new Achievement() { countToUnlock = 1, isUnlocked = (PlayerPrefs.GetInt(achText) >= 1), Message = achText, Reward = 5 });
//        _achievements.Add(AchievementType.Tutorial, listTurorial);

//        var listRelax = new List<Achievement>();
//        string achText1 = "Play a game in Relax mode!";
//        listRelax.Add(new Achievement() { countToUnlock = 1, isUnlocked = (PlayerPrefs.GetInt(achText1) >= 1), Message = achText1, Reward = 5 });
//        achText1 = "Play 100 games in Relax mode!";
//        listRelax.Add(new Achievement() { countToUnlock = 100, isUnlocked = (PlayerPrefs.GetInt(achText1) >= 100), Message = achText1, Reward = 10 });
//        achText1 = "Play 500 games in Relax mode!";
//        listRelax.Add(new Achievement() { countToUnlock = 500, isUnlocked = (PlayerPrefs.GetInt(achText1) >= 500), Message = achText1, Reward = 25 });
//        _achievements.Add(AchievementType.Relax, listRelax);

//        var listNormal = new List<Achievement>();
//        string achText2 = "Play a game in Normal mode!";
//        listNormal.Add(new Achievement() { countToUnlock = 1, isUnlocked = (PlayerPrefs.GetInt(achText2) >= 1), Message = achText2, Reward = 5 });
//        achText2 = "Play 100 games in Normal mode!";
//        listNormal.Add(new Achievement() { countToUnlock = 100, isUnlocked = (PlayerPrefs.GetInt(achText2) >= 100), Message = achText2, Reward = 10 });
//        achText2 = "Play 500 games in Normal mode!";
//        listNormal.Add(new Achievement() { countToUnlock = 500, isUnlocked = (PlayerPrefs.GetInt(achText2) >= 500), Message = achText2, Reward = 25 });
//        _achievements.Add(AchievementType.Normal, listNormal);

//        var listDevil = new List<Achievement>();
//        string achText3 = "Play a game in Devil mode!";
//        listDevil.Add(new Achievement() { countToUnlock = 1, isUnlocked = (PlayerPrefs.GetInt(achText3) >= 1), Message = achText3, Reward = 5 });
//        achText3 = "Play 100 games in Devil mode!";
//        listDevil.Add(new Achievement() { countToUnlock = 100, isUnlocked = (PlayerPrefs.GetInt(achText3) >= 100), Message = achText3, Reward = 10 });
//        achText3 = "Play 500 games in Devil mode!";
//        listDevil.Add(new Achievement() { countToUnlock = 500, isUnlocked = (PlayerPrefs.GetInt(achText3) >= 500), Message = achText3, Reward = 25 });
//        _achievements.Add(AchievementType.Devil, listDevil);

//        var listDevilHS = new List<Achievement>();
//        string achHS = "Reach 10 points in Devil mode!";
//        listDevilHS.Add(new Achievement() { countToUnlock = 10, isUnlocked = _achievementKeeper[AchievementType.HighscoreDevil] >= 10, Message = achHS, Reward = 20 });
//        achHS = "Reach 100 points in Devil mode!";
//        listDevilHS.Add(new Achievement() { countToUnlock = 100, isUnlocked = _achievementKeeper[AchievementType.HighscoreDevil] >= 100, Message = achHS, Reward = 66 });
//        achHS = "Reach 500 points in Devil mode!";
//        listDevilHS.Add(new Achievement() { countToUnlock = 500, isUnlocked = _achievementKeeper[AchievementType.HighscoreDevil] >= 500, Message = achHS, Reward = 99 });
//        _achievements.Add(AchievementType.HighscoreDevil, listDevilHS);

//        var listNormalHS = new List<Achievement>();
//        achHS = "Reach 10 points in Normal mode!";
//        listNormalHS.Add(new Achievement() { countToUnlock = 10, isUnlocked = _achievementKeeper[AchievementType.HighscoreNormal] >= 10, Message = achHS, Reward = 15 });
//        achHS = "Reach 100 points in Normal mode!";
//        listNormalHS.Add(new Achievement() { countToUnlock = 100, isUnlocked = _achievementKeeper[AchievementType.HighscoreNormal] >= 100, Message = achHS, Reward = 40 });
//        achHS = "Reach 500 points in Normal mode!";
//        listNormalHS.Add(new Achievement() { countToUnlock = 500, isUnlocked = _achievementKeeper[AchievementType.HighscoreNormal] >= 500, Message = achHS, Reward = 80 });
//        _achievements.Add(AchievementType.HighscoreNormal, listNormalHS);

//        var listRelaxHS = new List<Achievement>();
//        achHS = "Reach 10 points in Relax mode!";
//        listRelaxHS.Add(new Achievement() { countToUnlock = 10, isUnlocked = _achievementKeeper[AchievementType.HighscoreRelax] >= 10, Message = achHS, Reward = 10 });
//        achHS = "Reach 100 points in Relax mode!";
//        listRelaxHS.Add(new Achievement() { countToUnlock = 100, isUnlocked = _achievementKeeper[AchievementType.HighscoreRelax] >= 100, Message = achHS, Reward = 20 });
//        achHS = "Reach 500 points in Relax mode!";
//        listRelaxHS.Add(new Achievement() { countToUnlock = 500, isUnlocked = _achievementKeeper[AchievementType.HighscoreRelax] >= 500, Message = achHS, Reward = 40 });
//        _achievements.Add(AchievementType.HighscoreRelax, listRelaxHS);
//    }

//    public void RegisterEvent(AchievementType type)
//    {
//        Debug.Log("EVENT: " + type);
//        if (!_achievementKeeper.ContainsKey(type))
//            return;

//        _achievementKeeper[type]++;

//        ParseAchievements(type);
//    }

//    public void Highscore(AchievementType type, int points)
//    {
//        Debug.Log("EVENT: " + type);
//        if (!_achievementKeeper.ContainsKey(type))
//            return;
//        _achievementKeeper[type] = points;
//        ParseAchievements(type);
//    }

//    public void ParseAchievements(AchievementType type)
//    {
//        Debug.Log("achKeeper == " + _achievementKeeper[AchievementType.Tutorial] + " " + _achievementKeeper[AchievementType.Relax] +
//    " " + _achievementKeeper[AchievementType.Normal] + " " + _achievementKeeper[AchievementType.Devil]);
//        foreach (var kvp in _achievements.Where(a => a.Key == type))
//        {
//            foreach (var ach in kvp.Value.Where(a => a.isUnlocked == false))
//            {
//                Debug.Log(type + " count: " + _achievementKeeper[type] + " count to unlock: " + ach.countToUnlock);
//                if (_achievementKeeper[type] >= ach.countToUnlock)
//                    RaiseAchievementUnlocked(ach);
//            }
//        }
//    }
//}