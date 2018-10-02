using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoalController : MonoBehaviour {

    private GameObject gameBall;
    public string opposingTeamName;
    public string thisTeamName;

    private Text matchTimeText;
    public int thisTeamScore = 0;
    public int opposingTeamScore = 0;
    private Text thisTeamScoreText;
    private Text opposingTeamScoreText;

    // Use this for initialization
    void Start () {
        matchTimeText = GameObject.Find("MatchTime").GetComponent<Text>();
        thisTeamScoreText = GameObject.Find(thisTeamName+"_Score").GetComponent<Text>();
        opposingTeamScoreText = GameObject.Find(opposingTeamName+"_Score").GetComponent<Text>();
        gameBall = GameObject.Find("GameBall");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("GameBall"))
        {
            GetComponent<AudioSource>().Play();
            opposingTeamScore++;
            opposingTeamScoreText.text = opposingTeamName+": "+opposingTeamScore.ToString();
        }
    }
}
