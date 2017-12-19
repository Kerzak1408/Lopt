//using UnityEngine;
//using System.Collections;
//using UnityEngine.UI;

//public class AchievementPoints : MonoBehaviour {

//	// Use this for initialization
//	void Start () {
//        int achPoints = 0;
//        AchievementsManager achievementManager = new AchievementsManager();
//        foreach (System.Collections.Generic.List<Achievement> achievementsList in achievementManager._achievements.Values)
//            foreach (Achievement achievement in achievementsList)
//            {
//                //Debug.Log(achievement.Message + "  " + achievement.Reward + "  " + achievement.isUnlocked);
//                achPoints += achievement.isUnlocked ? achievement.Reward : 0;
//            }

//        GetComponent<Text>().text = achPoints.ToString();

//    }
	
//}
