using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {

    private Text matchTimeText;
    private Text teamAScoreText;
    private Text teamBScoreText;
    public float timeLeft;
    private float teamARVal, teamAGVal, teamABVal;
    private float teamBRVal, teamBGVal, teamBBVal;

    // Use this for initialization
    void Start () {
        matchTimeText = GameObject.Find("MatchTime").GetComponent<Text>();
        teamAScoreText = GameObject.Find("TeamA_Score").GetComponent<Text>();
        teamBScoreText = GameObject.Find("TeamB_Score").GetComponent<Text>();

        teamARVal = PlayerPrefs.GetFloat("TeamA_RedValue");
        teamAGVal = PlayerPrefs.GetFloat("TeamA_GreenValue");
        teamABVal = PlayerPrefs.GetFloat("TeamA_BlueValue");

        teamBRVal = PlayerPrefs.GetFloat("TeamB_RedValue");
        teamBGVal = PlayerPrefs.GetFloat("TeamB_GreenValue");
        teamBBVal = PlayerPrefs.GetFloat("TeamB_BlueValue");

        teamAScoreText.color = new Color(teamARVal, teamAGVal, teamABVal);
        teamBScoreText.color = new Color(teamBRVal, teamBGVal, teamBBVal);
    }
	
	// Update is called once per frame
	void Update () {
        UpdateTime();
	}

    // Method is used for decrementing the match time by seconds
    void UpdateTime ()
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
