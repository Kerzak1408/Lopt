using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GooglePlayGames;

public class Logic : MonoBehaviour {

    GameObject Cubes;
    GameObject Ball;
    GameObject Red;
    GameObject Green;
    GameObject Blue;
    GameObject Counter;
    GameObject PauseButton;
    GameObject IntroText;

    float worldScreenHeight;
    float worldScreenWidth;
    float rotationAngle;
    float additionalSpeed;
    float multiplCoef;
    float speed;
    float timeElapsed;
    float lastTime;

    public static int points;
    public static bool endOfTurn;
    public static bool paused;
    public static int mode;

    bool changedColor;
    bool preparationPhase;
    bool firstTimeEver;
    bool firstTimePhaseSecond;
    bool haveTime;
    bool gameFinished;

    public static int highscoreRelax;
    public static int highscoreNormal;
    public static int highscoreDevil;

    Color[] colors;
    Color[] colors2;
    Color[] colors3;

    Vector3 originalButtonSize;
    Vector3 originalPauseButtonSize;
    bool growingButtons;

    //AchievementsManager achievementManager;

    void Start()
    {

        highscoreDevil = PlayerPrefs.GetInt("highscore3");
        highscoreNormal = PlayerPrefs.GetInt("highscore2");
        highscoreRelax = PlayerPrefs.GetInt("highscore1");
        
        //PlayGamesPlatform.Instance.IncrementAchievement(GooglePlayConfig.achievement_enjoying_relaxing, 1, (bool success) => { });

        //LeaderboardControl.OnAddScoreToLeaderBorad(GooglePlayConfig.leaderboard_devil, highscoreDevil);
        //LeaderboardControl.OnAddScoreToLeaderBorad(GooglePlayConfig.leaderboard_normal, highscoreNormal);
        //LeaderboardControl.OnAddScoreToLeaderBorad(GooglePlayConfig.leaderboard_relax, highscoreRelax);

        gameFinished = false;
        //achievementManager = new AchievementsManager();
        haveTime = false;
        firstTimePhaseSecond = true;
        lastTime = 0;
        timeElapsed = 0;
        if (mode == 0)
        {
            rotationAngle = -270;
            additionalSpeed = -180;
            OnSpotlightsClick.firstClick = false;
        }
        if (mode == 1)
        {
            rotationAngle = -220;
            additionalSpeed = -120;
        } 
        else if (mode == 2)
        {
            rotationAngle = -270;
            additionalSpeed = -180;
        }
        else if (mode == 3)
        {
            rotationAngle = -500;
            additionalSpeed = -200;
        }

        preparationPhase = true;
        endOfTurn = false;
        points = 0;
        paused = false;
        firstTimeEver = PlayerPrefs.GetInt("firstTimeEver") == 0;
        //Debug.Log("First time ever == " + firstTimeEver);

        colors = new Color[3];
        colors2 = new Color[6];
        colors3 = new Color[7];
        colors3[0] = colors2[0] = colors[0] = new Color(255f, 0, 0, 255f);
        colors3[1] = colors2[1] = colors[1] = new Color(0, 255f, 0, 255f);
        colors3[2] = colors2[2] = colors[2] = new Color(0, 0, 255f, 255f);
        colors3[3] = colors2[3] = new Color(255f, 255f, 0, 255f);
        colors3[4] = colors2[4] = new Color(255f, 0, 255f, 255f);
        colors3[5] = colors2[5] = new Color(0, 255f, 255f, 255f);
        colors3[6] = new Color(255f, 255f, 255f, 255f);



        changedColor = false;
        speed = 0f;

        worldScreenHeight = Camera.main.orthographicSize * 2;
        worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
        //worldScreenWidth = worldScreenHeight * Camera.main.aspect;

        //worldScreenHeight = Screen.width;
        //worldScreenWidth = Screen.height;

        //Camera.main.orthographicSize = Screen.height/2;

        Cubes = GameObject.Find("Cubes");
        Ball = GameObject.Find("Ball");
        Red = GameObject.Find("Red");
        Green = GameObject.Find("Green");
        Blue = GameObject.Find("Blue");

        Counter = GameObject.Find("Counter");
        PauseButton = GameObject.Find("PauseButton");
        IntroText = GameObject.Find("IntroText");
        IntroText.GetComponent<Text>().text = "";

        ChangeBallColor(colors[0]);
        Initialize();

        originalButtonSize = Red.transform.lossyScale;
        originalPauseButtonSize = PauseButton.transform.lossyScale;
        growingButtons = true;
    }

    void Update()
    {
        if (!paused)
        {
            timeElapsed += Time.deltaTime;
            Move();
        }
    }

    void Move()
    {
        if (gameFinished)
        {
            if (!AchievementAlert.Active())
                SceneManager.LoadScene("menu");
            return;
        }
        RotateBall();
        if (preparationPhase)
        {
            if (mode == 0)
            {
                PauseButton.transform.localScale = new Vector3(0, 0, 0);
                IntroText.GetComponent<Text>().text = "Hey there! I am Lopt :)";
                Red.transform.localScale = new Vector3(0, 0, 0);
                Green.transform.localScale = new Vector3(0, 0, 0);
                Blue.transform.localScale = new Vector3(0, 0, 0);
            }
            MoveBall();
        }
        else
        {
            
            if (mode == 0)
            {
                if (timeElapsed > 3 && timeElapsed < 6)
                {
                   
                    PhaseFirst((int) (timeElapsed * 2) != (int) (lastTime * 2));
                    lastTime = timeElapsed;
                    
                }
                else if (timeElapsed > 6)
                {
                    
                    PhaseSecond();
                }

            }
            else
            {
                MoveGround();
            }
            
        }


    }

    private void PhaseFirst(bool change)
    {
        IntroText.GetComponent<Text>().text = "I change my color over time :)";
        if (change)
            ChangeBallColor(colors3[Random.Range(0, colors3.Length)]);
        
    }

    private void PhaseSecond()
    {
        if (firstTimePhaseSecond)
        {
            ChangeBallColor(colors3[4]);
            Red.transform.localScale = originalButtonSize;
            Green.transform.localScale = originalButtonSize;
            Blue.transform.localScale = originalButtonSize;
            firstTimePhaseSecond = false;
        }
        if (OnSpotlightsClick.firstClick)
            IntroText.GetComponent<Text>().text = "Good job! Can you mix the lights to match my color? ";
        else
            IntroText.GetComponent<Text>().text = "You can turn the spotlights ON and OFF. Try it out! ";
        speed = GetSpeedFromAngle(RecomputeBallAngle());
        Color playersColor = ComputeColor();
        bool continueMoving = true;
        foreach(Transform child in Cubes.transform)
        {
            foreach (Transform spotlight in child.transform)
            {
                if (Ball.transform.position.x >= spotlight.transform.position.x)
                    continueMoving = false;
            }
        }
        if (playersColor == Ball.transform.Find("Capsule").gameObject.GetComponent<Renderer>().material.color)
        {

            rotationAngle = -60;
            continueMoving = true;
            
            Red.transform.localScale = originalButtonSize;
            Green.transform.localScale = originalButtonSize;
            Blue.transform.localScale = originalButtonSize;
            if (!haveTime)
            {
                lastTime = timeElapsed;
                haveTime = true;
            }

            IntroText.GetComponent<Text>().text = "Spectacular! You are ready now :) ";
            if (timeElapsed - lastTime > 2)
            {
                rotationAngle = -270;
                PauseButton.transform.localScale = originalPauseButtonSize;
                PlayerPrefs.SetInt("firstTimeEver", 1);
                PlayerPrefs.Save();
                ActualiseAchievements();
                gameFinished = true;
            }

        }
        else
        {
            float growConst = 1.025f;
            if (growingButtons)
            {

                if (Red.transform.lossyScale.x < originalButtonSize.x * 1.5f)
                {
                    Red.transform.localScale = new Vector3(Red.transform.localScale.x * growConst, Red.transform.localScale.y * growConst, Red.transform.localScale.z);
                    Green.transform.localScale = new Vector3(Green.transform.localScale.x * growConst, Green.transform.localScale.y * growConst, Green.transform.localScale.z);
                    Blue.transform.localScale = new Vector3(Blue.transform.localScale.x * growConst, Blue.transform.localScale.y * growConst, Blue.transform.localScale.z);

                }
                else
                {
                    growingButtons = false;
                }
            }
            else
            {
                if (Red.transform.lossyScale.x > originalButtonSize.x)
                {
                    Red.transform.localScale = new Vector3(Red.transform.localScale.x / growConst, Red.transform.localScale.y / growConst, Red.transform.localScale.z);
                    Green.transform.localScale = new Vector3(Green.transform.localScale.x / growConst, Green.transform.localScale.y / growConst, Green.transform.localScale.z);
                    Blue.transform.localScale = new Vector3(Blue.transform.localScale.x / growConst, Blue.transform.localScale.y / growConst, Blue.transform.localScale.z);
                }
                else
                {
                    growingButtons = true;
                }
            }
        }
        foreach (Transform child in Cubes.transform)
        {
            if (continueMoving)
                child.gameObject.transform.position += new Vector3(-1, 0, 0) * speed;
            if (child.gameObject.transform.position.x < -worldScreenWidth)
            {
                child.gameObject.transform.position += new Vector3(2 * worldScreenWidth, 0, 0);

            }
            RefreshReflectors(child, playersColor);
        }

    }


    private void Intro()
    {
        speed = GetSpeedFromAngle(RecomputeBallAngle());
        Color playersColor = ComputeColor();
        //Debug.Log("First color = " + playersColor);
        foreach (Transform child in Cubes.transform)
        {
            child.gameObject.transform.position += new Vector3(-1, 0, 0) * speed;
            if (child.gameObject.transform.position.x < -worldScreenWidth)
            {
                child.gameObject.transform.position += new Vector3(2 * worldScreenWidth, 0, 0);
                
            }
            RefreshReflectors(child, playersColor);
        }
    }

    private float RecomputeBallAngle()
    {
        float result = 0;
        result = rotationAngle * Time.deltaTime;
        return result;
    }

    private void MoveBall()
    {
        if (Ball.transform.position.x < -4 * worldScreenWidth / 2 / 5)
        {
            Ball.transform.position += new Vector3(GetSpeedFromAngle(RecomputeBallAngle()), 0, 0);
        }
        else
        {
            preparationPhase = false;
        }
    }

    private void MoveGround()
    {
        speed = GetSpeedFromAngle(RecomputeBallAngle());
        Color playersColor = ComputeColor();
        
        // Move ground left
        foreach (Transform child in Cubes.transform)
        {
            child.gameObject.transform.position += new Vector3(-1, 0, 0) * speed;
        }

        foreach (Transform child in Cubes.transform)
        {

            if (child.gameObject.transform.position.x < -worldScreenWidth)
            {
                child.gameObject.transform.position += new Vector3(2 * worldScreenWidth, 0, 0);
                changedColor = false;
            }
            else if (!changedColor && child.gameObject.transform.position.x + child.transform.lossyScale.x / 2 < Ball.transform.position.x)
            {
                // check color
                //Debug.Log("Ball color = " + Ball.transform.FindChild("Capsule").gameObject.GetComponent<Renderer>().material.color + " Light color = " + playersColor);
                if (Ball.transform.Find("Capsule").gameObject.GetComponent<Renderer>().material.color.Equals(playersColor))
                {
                    Color newColor = new Color(Random.value, Random.value, Random.value, 1.0f);
                    // apply it on current object's material

                    ChangeBallColor(colors[Random.Range(0, colors.Length)]);
                    ResetRGBValues();
                    points += ComputePoints(playersColor);
                    CheckHighscores(points);
                    if (points > 5)
                    {
                        colors = colors3;
                    }
                    else if (points > 2)
                    {
                        colors = colors2;
                    }
                    if (mode == 1)
                    {
                        rotationAngle += additionalSpeed;
                        additionalSpeed *= 0.6f;
                    }
                    else if (mode == 2)
                    {
                        rotationAngle += additionalSpeed;
                        additionalSpeed *= 0.66f;//  * multiplCoef;
                        //multiplCoef = multiplCoef * multiplCoef;
                    }
                    else if (mode == 3)
                    {
                        rotationAngle += additionalSpeed;
                        additionalSpeed *= 0.66f;
                    }
                }
                else
                {
                    
                    int highscore = PlayerPrefs.GetInt("highscore" + mode);
#if UNITY_ANDROID
                    ActualiseAchievements();
                    if (points > highscore)
                    {
                        Highscore.changed = true;
                        if (mode == 3)
                        {
                            highscoreDevil = points;
                            if (Social.localUser.authenticated)
                            {
                                Social.ReportProgress(GooglePlayConfig.achievement_leviathan, 10 * highscoreDevil, (bool success) => { });
                                Social.ReportProgress(GooglePlayConfig.achievement_mefistofeles, 2 * highscoreDevil, (bool success) => { });
                                Social.ReportProgress(GooglePlayConfig.achievement_beelzebub, highscoreDevil, (bool success) => { });
                                Social.ReportProgress(GooglePlayConfig.achievement_lucifer, highscoreDevil / 5, (bool success) => { });
                                LeaderboardControl.OnAddScoreToLeaderBorad(GooglePlayConfig.leaderboard_devil, highscoreDevil);
                            }
                            
                        }
                        else if (mode == 2)
                        {
                            highscoreNormal = points;
                            if (Social.localUser.authenticated)
                            {
                                Social.ReportProgress(GooglePlayConfig.achievement_extraordinary, 10 * highscoreNormal, (bool success) => { });
                                Social.ReportProgress(GooglePlayConfig.achievement_rare, 2 * highscoreNormal, (bool success) => { });
                                Social.ReportProgress(GooglePlayConfig.achievement_epic, highscoreNormal, (bool success) => { });
                                Social.ReportProgress(GooglePlayConfig.achievement_phenomenal, highscoreNormal / 5, (bool success) => { });
                                LeaderboardControl.OnAddScoreToLeaderBorad(GooglePlayConfig.leaderboard_normal, highscoreNormal);
                            }
                        }
                        else
                        {
                            highscoreRelax = points;
                            if (Social.localUser.authenticated)
                            {
                                Social.ReportProgress(GooglePlayConfig.achievement_talented, 10 * highscoreRelax, (bool success) => { });
                                Social.ReportProgress(GooglePlayConfig.achievement_chiller, 2 * highscoreRelax, (bool success) => { });
                                Social.ReportProgress(GooglePlayConfig.achievement_patient, highscoreRelax, (bool success) => { });
                                Social.ReportProgress(GooglePlayConfig.achievement_nolife, highscoreRelax / 5, (bool success) => { });
                                LeaderboardControl.OnAddScoreToLeaderBorad(GooglePlayConfig.leaderboard_relax, highscoreRelax);
                            }
                        }
                            
                        PlayerPrefs.SetInt("highscore" + mode, points);
                        PlayerPrefs.Save();
                    }
#endif
                    PlayerPrefs.SetInt("last" + mode, points);
                    endOfTurn = true;
                    if (!AchievementAlert.Active())
                        SceneManager.LoadScene("menu");
                    else
                        gameFinished = true;
                }



                changedColor = true;
            }
            RefreshReflectors(child, playersColor);
        }
    }

    private void CheckHighscores(int points)
    {
        //if (mode == 3)
        //{
        //    if (points > highscoreDevil)
        //        achievementManager.Highscore(AchievementType.HighscoreDevil, points);
        //}
        //else if (points > highscoreNormal)
        //{
        //    if (points > highscoreNormal)
        //        achievementManager.Highscore(AchievementType.HighscoreNormal, points);
        //}
        //else
        //{
        //    if (points > highscoreRelax)
        //        achievementManager.Highscore(AchievementType.HighscoreRelax, points);
        //}

    }

    private void ActualiseAchievements()
    {
        
        int games = PlayerPrefs.GetInt("games" + mode);
        games++;
        PlayerPrefs.SetInt("games" + mode, games);
        //AchievementType achType;
        if (mode == 0)
        {
            //achType = AchievementType.Tutorial;
            //PlayGamesPlatform.Instance.IncrementAchievement(GooglePlayConfig.achievement_disciple, 100, (bool success) => { });
            if (Social.localUser.authenticated)
                Social.ReportProgress(GooglePlayConfig.achievement_disciple, 100, (bool success) =>{});

        }
        else if (mode == 1)
        {
            //achType = AchievementType.Relax;
            if (Social.localUser.authenticated)
            {
                Social.ReportProgress(GooglePlayConfig.achievement_first_time_relaxing, 100, (bool success) => { });
                Social.ReportProgress(GooglePlayConfig.achievement_enjoying_relaxing, 10 * games, (bool success) => { });
                Social.ReportProgress(GooglePlayConfig.achievement_devoted_to_relaxing, 2 * games, (bool success) => { });
                Social.ReportProgress(GooglePlayConfig.achievement_tranquil, games, (bool success) => { });
                Social.ReportProgress(GooglePlayConfig.achievement_stoic, games / 5, (bool success) => { });
            }

        }
        else if (mode == 2)
        {
            //achType = AchievementType.Normal;
            if (Social.localUser.authenticated)
            {
                Social.ReportProgress(GooglePlayConfig.achievement_ordinary_one, 100, (bool success) => { });
                Social.ReportProgress(GooglePlayConfig.achievement_fealty, 10.01 * games, (bool success) => { });
                Social.ReportProgress(GooglePlayConfig.achievement_loyal, 2 * games, (bool success) => { });
                Social.ReportProgress(GooglePlayConfig.achievement_lopts_friend,  games, (bool success) => { });
                Social.ReportProgress(GooglePlayConfig.achievement_lopts_favourite, games / 5, (bool success) => { });
            }
        }
        else //mode == 3
        {
            //achType = AchievementType.Devil;
            if (Social.localUser.authenticated)
            {
                Social.ReportProgress(GooglePlayConfig.achievement_first_sin, 100, (bool success) => { });
                Social.ReportProgress(GooglePlayConfig.achievement_curious, 10 * games, (bool success) => { });
                Social.ReportProgress(GooglePlayConfig.achievement_playing_with_fire, 2 * games, (bool success) => { });
                Social.ReportProgress(GooglePlayConfig.achievement_tempted, games, (bool success) => { });
                Social.ReportProgress(GooglePlayConfig.achievement_hell_cartographer, games / 5, (bool success) => { });
            }
        }

        //Debug.Log("Ach. type: " + achType);

        //achievementManager.RegisterEvent(achType);
    }

    private void RefreshReflectors(Transform child, Color playersColor)
    {
        foreach (Transform reflector in child.transform)
        {
            LineRenderer lr = reflector.GetComponent<LineRenderer>();
            Vector3 pointOfLook = new Vector3(child.transform.position.x + Cubes.transform.localScale.x / 2 + reflector.transform.lossyScale.x / 2, child.gameObject.transform.position.y, 0);
            float xCoord = ComputeStartLineRendererX(reflector, pointOfLook);
            lr.SetPosition(0, reflector.transform.position - new Vector3(-xCoord,reflector.lossyScale.y/2 - reflector.lossyScale.y/10,0));
            
            lr.SetPosition(1, pointOfLook);
            lr.SetWidth(reflector.transform.lossyScale.x/4, 2 * Ball.transform.localScale.x);
            switch (reflector.name)
            {
                case "Reflector_Red":
                    {
                        //Debug.Log("red == " + playersColor.r);
                        if (playersColor.r == 0)
                        {
                            lr.enabled = false;
                        }
                        else
                        {
                            lr.enabled = true;
                            lr.SetColors(new Color(playersColor.r, 0, 0), new Color(playersColor.r, 0, 0));
                        }

                    }
                    break;
                case "Reflector_Green":
                    {
                        if (playersColor.g == 0)
                        {
                            lr.enabled = false;
                        }
                        else
                        {
                            lr.enabled = true;
                            lr.SetColors(new Color(0, playersColor.g, 0), new Color(0, playersColor.g, 0));
                        }
                    }
                    break;
                case "Reflector_Blue":
                    {
                        if (playersColor.b == 0)
                        {
                            lr.enabled = false;
                        }
                        else
                        {
                            lr.enabled = true;
                            lr.SetColors(new Color(0, 0, playersColor.b), new Color(0, 0, playersColor.b));
                        }
                    }
                    break;
            }
        }
    }

    private int ComputePoints(Color color)
    {
        int result = 0;
        if (color.r != 0)
            result++;
        if (color.g != 0)
            result++;
        if (color.b != 0)
            result++;
        return result;
    }

    private void ResetRGBValues()
    {
        OnSpotlightsClick.red_pressed = false;
        OnSpotlightsClick.green_pressed = false;
        OnSpotlightsClick.blue_pressed = false;
        OnSpotlightsClick.reset = true;
    }

    private Color ComputeColor()
    {

        Color result = new Color(0, 0, 0);
        if (Red == null || Green == null || Blue == null)
        {
            //Debug.Log("Color counters null");
            return result;

        }
        int red = (OnSpotlightsClick.red_pressed ? 1 : 0);
        int green = (OnSpotlightsClick.green_pressed ? 1 : 0);
        int blue = (OnSpotlightsClick.blue_pressed ? 1 : 0);



        if (red == 0 && green == 0 && blue == 0)
            return result;

        // find maximum
        if (red > green)
        {
            // red or blue
            if (red > blue)
            {
                // red
                result.r = 1;
                result.g = 1f * green / red;
                result.b = 1f * blue / red;
            }
            else
            {
                // blue
                result.r = 1f * red / blue;
                result.g = 1f * green / blue;
                result.b = 1;
            }
        }
        else
        {
            // green or blue
            if (green > blue)
            {
                // green
                result.r = 1f * red / green;
                result.g = 1;
                result.b = 1f * blue / green;
            }
            else
            {
                // blue
                result.r = 1f * red / blue;
                result.g = 1f * green / blue;
                result.b = 1;
            }
        }
        //Debug.Log(" Computed color: (" + result.r + ", " + result.g + ", " + result.b + ")");
        return result*255;
    }

    private void RotateBall()
    {
        Ball.transform.Rotate(new Vector3(0, 0, RecomputeBallAngle()));
    }

    private float GetSpeedFromAngle(float angle)
    {
        float result = (Mathf.Abs(angle) * 2 * Mathf.PI * Ball.transform.localScale.x / 2) / 360;
        //Debug.Log("speed: " + result);
        return result;

    }

    private void ChangeBallColor(Color color)
    {
        foreach (Transform child in Ball.transform)
        {
            child.gameObject.GetComponent<Renderer>().material.color = color;
        }
    }

    void Initialize()
    {

        Cubes.gameObject.transform.localScale = new Vector3(
            worldScreenWidth / Cubes.gameObject.transform.localScale.x,
            worldScreenWidth / Cubes.gameObject.transform.localScale.y / 9,
            1);

        Ball.transform.localScale = new Vector3(Cubes.transform.localScale.y, Cubes.transform.localScale.y, Cubes.transform.localScale.y);
        Red.transform.localScale = Ball.transform.localScale;
        Green.transform.localScale = Red.transform.localScale;
        Blue.transform.localScale = Red.transform.localScale;

        float yCoord = -worldScreenHeight / 2 + Cubes.gameObject.transform.localScale.y / 2;

        Green.transform.position = new Vector3(0, yCoord, -2);
        Red.transform.position = Green.transform.position - new Vector3(2 * Red.transform.lossyScale.x, 0, 0);
        Blue.transform.position = Green.transform.position + new Vector3(2 * Blue.transform.lossyScale.x, 0, 0);

        int i = 0;
        foreach (Transform child in Cubes.transform)
        {
            child.transform.position = new Vector3(i * (worldScreenWidth), -worldScreenHeight / 2 + Cubes.gameObject.transform.localScale.y / 2, 0);
            i++;

            Ball.transform.position = new Vector3(-worldScreenWidth / 2, child.gameObject.transform.position.y + Cubes.transform.localScale.y / 2 + Ball.transform.localScale.y / 2, -2);
            Counter.transform.position = new Vector3(worldScreenWidth / 2 - 2 * Counter.transform.lossyScale.x, worldScreenHeight / 2 - 2.5f * Counter.transform.lossyScale.y, Counter.transform.position.z);
            
            Vector3 pointOfLook = new Vector3(child.transform.position.x + Cubes.transform.localScale.x / 2, child.gameObject.transform.position.y, 0);
            int j = 0;
            foreach (Transform reflector in child.gameObject.transform)
            {
                Vector3 childScale = child.transform.localScale;
                reflector.gameObject.transform.position = new Vector3(child.transform.position.x + Cubes.transform.localScale.x / 2 - (j + 1) * 2 * Cubes.transform.localScale.x / 20, worldScreenHeight / 2 - reflector.lossyScale.y/4, 1);
                reflector.gameObject.transform.localScale = new Vector3(childScale.x / 15, childScale.y / 1.8f, childScale.z);
                PauseButton.transform.localScale = new Vector3(reflector.transform.lossyScale.x/2, reflector.transform.lossyScale.x/2, 1);
                float angle = ComputeRotationAngle(reflector.transform.position, pointOfLook);
                reflector.Rotate(new Vector3(0, 0, angle));
                j--;
                LineRenderer lr = reflector.GetComponent<LineRenderer>();
                lr.material = new Material(Shader.Find("Particles/Additive"));
            }

        }

        PauseButton.transform.position = new Vector3(-4 * worldScreenWidth / 2 / 5, Counter.transform.position.y, Counter.transform.position.z);

    }

    private float ComputeRotationAngle(Vector3 reflectorPosition, Vector3 pointOfLook)
    {
        //Debug.Log("reflectorPos = " + reflectorPosition + " pointOfLook = " + pointOfLook);
        if (Mathf.Abs(reflectorPosition.x - pointOfLook.x) < 1e-5)
        {
            //Debug.Log("Zero rotation");
            return 0;
        }
        //Debug.Log("Diff: " + (reflectorPosition.x - pointOfLook.x));
        float result = 0;
        float height = -reflectorPosition.y + pointOfLook.y;
        float width = -reflectorPosition.x + pointOfLook.x;
        result = Mathf.Atan(height / width);
        

        return -result;
    }

    private float ComputeStartLineRendererX(Transform reflector, Vector3 pointOfLook)
    {
        if (reflector.name == "Reflector_Green")
            return 0;
        else
        {
            //float result = 0;
            //float alpha = ComputeRotationAngle(reflector.transform.position, pointOfLook);
            //Debug.Log("ALPHA == " + (alpha + Mathf.PI / 2));
            //float b = Mathf.Sqrt((reflector.lossyScale.x / 2) * (reflector.lossyScale.x / 2) + (reflector.lossyScale.y / 2) * (reflector.lossyScale.y / 2));
            //result = Mathf.Tan(alpha + Mathf.PI/2) * b;
            //return result;
            float result = reflector.lossyScale.x / 10;
            if (reflector.name == "Reflector_Red")
            {
                return result;
            }
            else
                return -result;
        }
    }
}