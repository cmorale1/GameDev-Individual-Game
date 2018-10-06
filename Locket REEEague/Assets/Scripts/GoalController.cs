using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalController : MonoBehaviour {

    public string opposingTeamName;
    public string thisTeamName;

    public delegate void OnGoal(GoalController goal);
    public event OnGoal onGoal;

    public int thisTeamScore = 0;
    public int opposingTeamScore = 0;
    private Text opposingTeamScoreText;

    private Vector3 ballResetPosition;
    private Vector3 thisTeamResetPosition;
    private Vector3 opposingTeamResetPosition;

    private GameObject ball;
    private GameObject thisTeamVehicle;
    private GameObject opposingTeamVehicle;

    private Animator ballAnim;
    private Animation explosionAnim;

    private Rigidbody2D ballRigidBody;
    private Rigidbody2D thisTeamRigidBody;
    private Rigidbody2D opposingTeamRigidBody;

    private SpriteRenderer mainNetSpRend;
    private SpriteRenderer topNetSpRend;

    private float rVal, gVal, bVal;

    private ParticleSystem leftPSystem;
    private ParticleSystem rightPSystem;

    public GameObject gameBallPrefab;

    // Use this for initialization
    void Start () {
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
            rVal = PlayerPrefs.GetFloat("LeftImage_RedValue");
            gVal = PlayerPrefs.GetFloat("LeftImage_GreenValue");
            bVal = PlayerPrefs.GetFloat("LeftImage_BlueValue");            
        }
        if(thisTeamName == "TeamB")
        {
            mainNetSpRend = GameObject.Find("mainRightNet").GetComponent<SpriteRenderer>();
            topNetSpRend = GameObject.Find("topRightNet").GetComponent<SpriteRenderer>();
            rVal = PlayerPrefs.GetFloat("RightImage_RedValue");
            gVal = PlayerPrefs.GetFloat("RightImage_GreenValue");
            bVal = PlayerPrefs.GetFloat("RightImage_BlueValue");
        }

        mainNetSpRend.color = new Color(rVal, gVal, bVal);
        topNetSpRend.color = new Color(rVal, gVal, bVal);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // This could probably be put into a Coroutine or Observer to invoke the spawn of the gameBall prefab
    void resetPositions()
    {
        ball.transform.position = ballResetPosition;
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
        // Collision check for game ball and the goal this script is attached to
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

    IEnumerator GoalAndExplodeAndRespawn()
    {
        if (onGoal != null)
        {
            onGoal(this);
        }

        ballAnim.SetBool("goalMade", true);
        ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(explosionAnim.clip.length);
        ballAnim.SetBool("goalMade", false);
        ball.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        resetPositions();
    }
}
