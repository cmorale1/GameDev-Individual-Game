using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AfterMatchScript : MonoBehaviour {

    private Text teamAScoreText;
    private Text teamBScoreText;

	// Use this for initialization
	void Start () {
        teamAScoreText = GameObject.Find("FinalTeamA_Score").GetComponent<Text>();
        teamBScoreText = GameObject.Find("FinalTeamB_Score").GetComponent<Text>();

        teamAScoreText.text = PlayerPrefs.GetFloat("TeamA_TotalScore").ToString();
        teamBScoreText.text = PlayerPrefs.GetFloat("TeamB_TotalScore").ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
