using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalController : MonoBehaviour {

    public string opposingTeamName;
    public string thisTeamName;

    public delegate void OnGoal(GoalController goal);
    public event OnGoal onGoal;

    private int opposingTeamScore;
    private Text opposingTeamScoreText;

    private GameObject ball;
    private GameObject thisTeamVehicle;
    private GameObject opposingTeamVehicle;

    private Vector3 ballResetPosition;
    private Vector3 thisTeamResetPosition;
    private Vector3 opposingTeamResetPosition;

    private Animator ballAnim;
    private Animation explosionAnim;

    private SpriteRenderer mainNetSpRend;
    private SpriteRenderer topNetSpRend;

    private Rigidbody2D ballRigidBody;
    private Rigidbody2D thisTeamRigidBody;
    private Rigidbody2D opposingTeamRigidBody;

    private float rVal, gVal, bVal;

    private ParticleSystem leftPSystem, rightPSystem;

    // Use this for initialization
    void Start () {
        opposingTeamScore = 0;
        opposingTeamScoreText = GameObject.Find(opposingTeamName+"_Score").GetComponent<Text>();

        ball = GameObject.Find("GameBall");
        ballAnim = ball.GetComponent<Animator>();
        explosionAnim = ball.GetComponent<Animation>();
        ballAnim.SetBool("goalMade", false);
        thisTeamVehicle = GameObject.Find(thisTeamName + "_Vehicle");
        opposingTeamVehicle = GameObject.Find(opposingTeamName + "_Vehicle");

        ballRigidBody = ball.GetComponent<Rigidbody2D>();
        thisTeamRigidBody = thisTeamVehicle.GetComponent<Rigidbody2D>();
        opposingTeamRigidBody = opposingTeamVehicle.GetComponent<Rigidbody2D>();

        ballResetPosition = ball.transform.position;
        thisTeamResetPosition = thisTeamVehicle.transform.position;
        opposingTeamResetPosition = opposingTeamVehicle.transform.position;

        leftPSystem = GameObject.Find("LeftParticleSystem").GetComponent<ParticleSystem>();
        rightPSystem = GameObject.Find("RightParticleSystem").GetComponent<ParticleSystem>();

        if (thisTeamName == "TeamA")
        {
            mainNetSpRend = GameObject.Find("mainLeftNet").GetComponent<SpriteRenderer>();
            topNetSpRend = GameObject.Find("topLeftNet").GetComponent<SpriteRenderer>();       
        }
        if(thisTeamName == "TeamB")
        {
            mainNetSpRend = GameObject.Find("mainRightNet").GetComponent<SpriteRenderer>();
            topNetSpRend = GameObject.Find("topRightNet").GetComponent<SpriteRenderer>();
        }

        rVal = PlayerPrefs.GetFloat(thisTeamName+"_RedValue");
        gVal = PlayerPrefs.GetFloat(thisTeamName+"_GreenValue");
        bVal = PlayerPrefs.GetFloat(thisTeamName+"_BlueValue");

        mainNetSpRend.color = new Color(rVal, gVal, bVal);
        topNetSpRend.color = new Color(rVal, gVal, bVal);
	}

    // Method used for placing game objects (ball and players) back to their starting positions
    void ResetPositions()
    {
        ball.transform.position = ballResetPosition;
        ball.GetComponent<CircleCollider2D>().enabled = true;
        ball.GetComponent<Rigidbody2D>().angularVelocity = 0.0f;
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
        // Check to see if the collider is triggered inside of the net
        if (collision.gameObject.layer == LayerMask.NameToLayer("GameBall"))
        {
            StartCoroutine(GoalAndExplodeAndRespawn());
            if(opposingTeamName == "TeamA")
            {
                rightPSystem.Play();
            } else if(opposingTeamName == "TeamB")
            {
                leftPSystem.Play();
            }
            GetComponent<AudioSource>().Play();
            opposingTeamScore++;
            opposingTeamScoreText.text = opposingTeamScore.ToString();
            PlayerPrefs.SetFloat(opposingTeamName + "_TotalScore", opposingTeamScore);
        }
    }

    // Method used to start the ball explosion animation and wait to respawn until animation is finished
    IEnumerator GoalAndExplodeAndRespawn()
    {
        if (onGoal != null)
        {
            onGoal(this);
        }

        ballAnim.SetBool("goalMade", true);
        Debug.Log("Animation Started");
        ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        ball.GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds((float) explosionAnim.clip.length - 0.1f);
        ballAnim.SetBool("goalMade", false);
        Debug.Log("Animation Ended");
        ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;

        ResetPositions();
    }
}
