using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AfterMatchController : MonoBehaviour {

    private Text teamAScoreText;
    private Text teamBScoreText;
    private float teamARVal, teamAGVal, teamABVal;
    private float teamBRVal, teamBGVal, teamBBVal;

    // Use this for initialization
    void Start()
    {
        teamAScoreText = GameObject.Find("FinalTeamA_Score").GetComponent<Text>();
        teamBScoreText = GameObject.Find("FinalTeamB_Score").GetComponent<Text>();

        teamAScoreText.text = PlayerPrefs.GetFloat("TeamA_TotalScore").ToString();
        teamBScoreText.text = PlayerPrefs.GetFloat("TeamB_TotalScore").ToString();

        teamARVal = PlayerPrefs.GetFloat("TeamA_RedValue");
        teamAGVal = PlayerPrefs.GetFloat("TeamA_GreenValue");
        teamABVal = PlayerPrefs.GetFloat("TeamA_BlueValue");

        teamBRVal = PlayerPrefs.GetFloat("TeamB_RedValue");
        teamBGVal = PlayerPrefs.GetFloat("TeamB_GreenValue");
        teamBBVal = PlayerPrefs.GetFloat("TeamB_BlueValue");

        teamAScoreText.color = new Color(teamARVal, teamAGVal, teamABVal);
        teamBScoreText.color = new Color(teamBRVal, teamBGVal, teamBBVal);
    }
}