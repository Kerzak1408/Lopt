//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.UI;

//public class ShowAchievements : MonoBehaviour {

//    AchievementsManager achievementManager;
//    float worldScreenHeight;
//    float worldScreenWidth;
//    float panelHeight;

//    // Use this for initialization
//    void Start () {
//        achievementManager = new AchievementsManager();

//        worldScreenHeight = Camera.main.orthographicSize * 2;
//        worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
//        panelHeight = worldScreenHeight / 10;

//        int achievementsCount = 0;
//        foreach (List<Achievement> achievementsList in achievementManager._achievements.Values)
//            foreach (Achievement achievement in achievementsList)
//                achievementsCount++;

//        transform.localScale = transform.parent.lossyScale;

//        //GetComponent<VerticalLayoutGroup>().spacing = Screen.height/3;

//        int i = 0;
//        foreach (List<Achievement> achievementsList in achievementManager._achievements.Values)
//            foreach (Achievement achievement in achievementsList)
//            {
//                GameObject Panel = new GameObject("Panel");
//                Panel.AddComponent<Image>();
//                Panel.GetComponent<Image>().sprite = Resources.Load<Sprite>("RoundedRectangle");
//                Panel.GetComponent<Image>().color = achievement.isUnlocked ? Color.green : Color.grey;

//                Panel.transform.position = Vector3.zero;
//                Panel.transform.parent = this.transform;

//                Panel.AddComponent<LayoutElement>();
//                Panel.GetComponent<LayoutElement>().preferredHeight = 0.2f * GameObject.Find("Canvas").GetComponent<RectTransform>().rect.height;
//                Panel.GetComponent<LayoutElement>().preferredWidth = 0.65f * GameObject.Find("Canvas").GetComponent<RectTransform>().rect.width;
//                //Panel.transform.localScale = new Vector3(Panel.transform.localScale.x, Panel.transform.localScale.y / 2, Panel.transform.localScale.z);
//                //GetComponent<VerticalLayoutGroup>().spacing = 2 * Panel.GetComponent<LayoutElement>().preferredHeight / 3;
//                GetComponent<VerticalLayoutGroup>().spacing = Panel.GetComponent<RectTransform>().localScale.y * 100 -100;

//                GameObject TextObject = new GameObject("Text");
//                //Panel.transform.localScale = new Vector3(0.9f * worldScreenWidth / transform.lossyScale.x, panelHeight, 1);
//                //Panel.transform.position = new Vector3(transform.position.x, i * transform.lossyScale.y / achievementsCount, Panel.transform.position.z);
//                TextObject.AddComponent<RectTransform>();
//                //Panel.GetComponent<RectTransform>().sizeDelta = new Vector2(0.9f * GameObject.Find("Canvas").GetComponent<RectTransform>().sizeDelta.x, panelHeight);
//                TextObject.AddComponent<Text>();
//                //TextObject.GetComponent<RectTransform>().position = new Vector3(Panel.GetComponent<RectTransform>().position.x - Panel.GetComponent<RectTransform>().sizeDelta.x/2, 0, 0);
//                TextObject.GetComponent<RectTransform>().anchorMin = new Vector2(0.1f, 0.5f);

//                TextObject.GetComponent<Text>().alignment = TextAnchor.MiddleLeft;
//                TextObject.GetComponent<Text>().color = Color.black;
//                TextObject.GetComponent<Text>().font = Resources.GetBuiltinResource<Font>("Arial.ttf");
//                TextObject.GetComponent<Text>().fontSize = 35;
//                TextObject.GetComponent<Text>().horizontalOverflow = HorizontalWrapMode.Overflow;
//                TextObject.GetComponent<Text>().text = achievement.Message;

//                TextObject.transform.parent = Panel.transform;
//                //Image panelImage = Panel.AddComponent<Image>();
//                //Panel.GetComponent<Image>().sprite = Resources.Load<>
//                //panelImage.color = new Color(100, 100, 100, 100);

//                GameObject RewardImage = new GameObject("RewardImage");
//                RewardImage.AddComponent<Image>();
//                RewardImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("gold");
//                RewardImage.GetComponent<RectTransform>().position = new Vector3(0.25f * GameObject.Find("Canvas").GetComponent<RectTransform>().rect.width, 0, 0);
//                RewardImage.transform.parent = Panel.transform;

//                GameObject RewardTextObject = new GameObject("RewardText");
//                RewardTextObject.AddComponent<RectTransform>();
//                RewardTextObject.AddComponent<Text>();
//                Debug.Log("width: " + Panel.GetComponent<RectTransform>().rect.width);
//                RewardTextObject.GetComponent<RectTransform>().position = new Vector3(0.25f * GameObject.Find("Canvas").GetComponent<RectTransform>().rect.width, 0,0);

//                RewardTextObject.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
//                RewardTextObject.GetComponent<Text>().color = Color.black;
//                RewardTextObject.GetComponent<Text>().font = Resources.GetBuiltinResource<Font>("Arial.ttf");
//                RewardTextObject.GetComponent<Text>().fontSize = 35;
//                RewardTextObject.GetComponent<Text>().horizontalOverflow = HorizontalWrapMode.Overflow;
//                RewardTextObject.GetComponent<Text>().text = achievement.Reward.ToString();

//                RewardTextObject.transform.parent = Panel.transform;


//                i++;
//            }
//	}
	
//	// Update is called once per frame
//	void Update () {
//	}
//}
