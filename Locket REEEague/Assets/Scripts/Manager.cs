﻿using System.Collections;
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

	// Use this for initialization
	void Start () {
        matchTimeText = GameObject.Find("MatchTime").GetComponent<Text>();
        teamAScoreText = GameObject.Find("TeamA_Score").GetComponent<Text>();
        teamBScoreText = GameObject.Find("TeamB_Score").GetComponent<Text>();
        teamAScoreText.text = "Team A: " + teamAScore;
        teamBScoreText.text = "Team B: " + teamBScore;
        Debug.Log(teamAScore);
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

    void updateScore()
    {
        // Handles for checking to see whether ball touched Team A or Team B's goal
        // If touched Team A's goal, update Team B's score
        // If touched Team B's goal, update Team A's score
    }
}
