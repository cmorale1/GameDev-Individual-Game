using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {

    private Text matchTimeText;
    public int teamAScore = 4;
    public int teamBScore = 0;
    private Text teamAScoreText;
    private Text teamBScoreText;
    public float timeLeft = 60.0f;

    private float teamARVal;
    private float teamAGVal;
    private float teamABVal;

    private float teamBRVal;
    private float teamBGVal;
    private float teamBBVal;

    // Use this for initialization
    void Start () {
        matchTimeText = GameObject.Find("MatchTime").GetComponent<Text>();
        teamAScoreText = GameObject.Find("TeamA_Score").GetComponent<Text>();
        teamBScoreText = GameObject.Find("TeamB_Score").GetComponent<Text>();
        teamAScoreText.text = teamAScore.ToString();
        teamBScoreText.text = teamBScore.ToString();

        teamARVal = PlayerPrefs.GetFloat("LeftImage_RedValue");
        teamAGVal = PlayerPrefs.GetFloat("LeftImage_GreenValue");
        teamABVal = PlayerPrefs.GetFloat("LeftImage_BlueValue");

        teamBRVal = PlayerPrefs.GetFloat("RightImage_RedValue");
        teamBGVal = PlayerPrefs.GetFloat("RightImage_GreenValue");
        teamBBVal = PlayerPrefs.GetFloat("RightImage_BlueValue");

        teamAScoreText.color = new Color(teamARVal, teamAGVal, teamABVal);
        teamBScoreText.color = new Color(teamBRVal, teamBGVal, teamBBVal);
    }
	
	// Update is called once per frame
	void Update () {
        updateTime();
	}

    void updateTime ()
    {
        timeLeft -= Time.deltaTime;
        matchTimeText.text = (timeLeft).ToString("0");
        if (timeLeft < 0)
        {
            // Handle switching to end-game scene
            SceneManager.LoadScene("AfterMatchScene");
            matchTimeText.text = "Game Over!";
        }
    }
}
