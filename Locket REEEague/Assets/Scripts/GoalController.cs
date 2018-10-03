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

    private Vector3 ballResetPosition;
    private Vector3 thisTeamResetPosition;
    private Vector3 opposingTeamResetPosition;

    private GameObject ball;
    private GameObject thisTeamVehicle;
    private GameObject opposingTeamVehicle;

    private Rigidbody2D ballRigidBody;
    private Rigidbody2D thisTeamRigidBody;
    private Rigidbody2D opposingTeamRigidBody;

    // Use this for initialization
    void Start () {
        matchTimeText = GameObject.Find("MatchTime").GetComponent<Text>();
        thisTeamScoreText = GameObject.Find(thisTeamName+"_Score").GetComponent<Text>();
        opposingTeamScoreText = GameObject.Find(opposingTeamName+"_Score").GetComponent<Text>();
        gameBall = GameObject.Find("GameBall");

        ball = GameObject.Find("GameBall");
        thisTeamVehicle = GameObject.Find(thisTeamName + "_Vehicle");
        opposingTeamVehicle = GameObject.Find(opposingTeamName + "_Vehicle");

        ballRigidBody = ball.GetComponent<Rigidbody2D>();
        thisTeamRigidBody = thisTeamVehicle.GetComponent<Rigidbody2D>();
        opposingTeamRigidBody = opposingTeamVehicle.GetComponent<Rigidbody2D>();

        ballResetPosition = ball.transform.position;
        thisTeamResetPosition = thisTeamVehicle.transform.position;
        opposingTeamResetPosition = opposingTeamVehicle.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // This could probably be put into a Coroutine
    void resetPositions()
    {
        ball.transform.position = ballResetPosition;
        thisTeamVehicle.transform.position = thisTeamResetPosition;
        opposingTeamVehicle.transform.position = opposingTeamResetPosition;
        ballRigidBody.velocity = new Vector2(0, 0);
        thisTeamRigidBody.velocity = new Vector2(0, 0);
        opposingTeamRigidBody.velocity = new Vector2(0, 0);
        ball.transform.rotation = new Quaternion(0, 0, 0, 0);
        thisTeamVehicle.transform.rotation = new Quaternion(0, 0, 0, 0);
        opposingTeamVehicle.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Collision check for game ball and the goal this script is attached to
        if (collision.gameObject.layer == LayerMask.NameToLayer("GameBall"))
        {
            GetComponent<AudioSource>().Play();
            opposingTeamScore++;
            opposingTeamScoreText.text = opposingTeamName+": "+opposingTeamScore.ToString();
            resetPositions();
        }
    }
}
