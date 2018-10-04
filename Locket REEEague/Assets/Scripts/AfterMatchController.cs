using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AfterMatchController : MonoBehaviour {

    private Text teamAScoreText;
    private Text teamBScoreText;

    private float teamARVal;
    private float teamAGVal;
    private float teamABVal;

    private float teamBRVal;
    private float teamBGVal;
    private float teamBBVal;

    // Use this for initialization
    void Start()
    {
        teamAScoreText = GameObject.Find("FinalTeamA_Score").GetComponent<Text>();
        teamBScoreText = GameObject.Find("FinalTeamB_Score").GetComponent<Text>();

        teamAScoreText.text = PlayerPrefs.GetFloat("TeamA_TotalScore").ToString();
        teamBScoreText.text = PlayerPrefs.GetFloat("TeamB_TotalScore").ToString();

        teamARVal = PlayerPrefs.GetFloat("LeftImage_RedValue");
        teamAGVal = PlayerPrefs.GetFloat("LeftImage_GreenValue");
        teamABVal = PlayerPrefs.GetFloat("LeftImage_BlueValue");

        teamBRVal = PlayerPrefs.GetFloat("RightImage_RedValue");
        teamBGVal = PlayerPrefs.GetFloat("RightImage_GreenValue");
        teamBBVal = PlayerPrefs.GetFloat("RightImage_BlueValue");

        teamAScoreText.color = new Color(teamARVal, teamAGVal, teamABVal);
        teamBScoreText.color = new Color(teamBRVal, teamBGVal, teamBBVal);
    }
}