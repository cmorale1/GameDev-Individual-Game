using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour {

    public string teamName;
    private float movementSpeed;
    private float jumpPower;
    private float rotationSpeed;
    private float rotationRight;
    private float rot;

    public LayerMask groundMask;
    private float groundRadius;

    private Rigidbody2D theRigidbody;
    private Transform groundCheckLeft;
    private Transform groundCheckRight;

    private Quaternion groundedRotationPosition;

    private bool firstJump, airRolling, falling;

    private SpriteRenderer vehicleSpRend;
    private float rVal, gVal, bVal;

	// Use this for initialization
	void Start () {
        movementSpeed = 10f;
        jumpPower = 380f;
        rotationSpeed = 400f;
        rotationRight = 360f;
        rot = 0f;
        groundRadius = 0.1f;
        firstJump = false;
        airRolling = false;
        falling = false;
        groundedRotationPosition = transform.rotation;
        theRigidbody = GetComponent<Rigidbody2D>();
        groundCheckLeft = transform.Find("LeftGround");
        groundCheckRight = transform.Find("RightGround");
        vehicleSpRend = GetComponent<SpriteRenderer>();
        rVal = PlayerPrefs.GetFloat(teamName+"_RedValue");
        gVal = PlayerPrefs.GetFloat(teamName+"_GreenValue");
        bVal = PlayerPrefs.GetFloat(teamName+"_BlueValue");
        vehicleSpRend.color = new Color(rVal, gVal, bVal);
	}
	
	// Update is called once per frame
	void Update () {
        MovePlayer();
    }

    void MovePlayer()
    {
        float inputX = Input.GetAxis(teamName + "_Horizontal");
        bool grounded = Physics2D.OverlapCircle(groundCheckLeft.position, groundRadius, groundMask) && Physics2D.OverlapCircle(groundCheckRight.position, groundRadius, groundMask);
        bool jumping = Input.GetButtonDown(teamName + "_Jump");

        if (grounded && jumping)
        {
            theRigidbody.velocity = new Vector2(theRigidbody.velocity.x, 0f);
            theRigidbody.AddForce(new Vector2(0, jumpPower));
            firstJump = true;
        }
        if (firstJump && jumping && !grounded)
        {
            falling = true;
            GetComponent<AudioSource>().Play();
            theRigidbody.velocity = new Vector2(theRigidbody.velocity.x, 0f);
            theRigidbody.AddForce(new Vector2(0, jumpPower));
            firstJump = false;
        }
        if (Input.GetButtonDown(teamName + "_Boost") && !airRolling)
        {
            airRolling = true;
        }
        if (Input.GetButtonUp(teamName + "_Boost") && airRolling)
        {
            airRolling = false;
        }
        if (airRolling)
        {
            if(teamName == "TeamA")
            {
                transform.Rotate(Vector3.back * Time.deltaTime * 400f);
            }
            if(teamName == "TeamB")
            {
                transform.Rotate(Vector3.forward * Time.deltaTime * 400f);
            }
        }
        if (!airRolling)
        {
            theRigidbody.velocity = theRigidbody.velocity = new Vector2(inputX * movementSpeed, theRigidbody.velocity.y);
        }
        if (!grounded && jumping && !firstJump)
        {
            transform.rotation = new Quaternion(groundedRotationPosition.x, groundedRotationPosition.y, groundedRotationPosition.z, groundedRotationPosition.w);
        }
        if (grounded)
        {
            falling = false;
            transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
        }
        if (falling)
        {
            if(teamName == "TeamA")
            {
                rot = rotationSpeed * Time.deltaTime;
                rotationRight -= rot;
                transform.Rotate(0, 0, -rot);
            }
            if(teamName == "TeamB")
            {
                rot = rotationSpeed * Time.deltaTime;
                rotationRight -= rot;
                transform.Rotate(0, 0, rot);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Checks for the player's collision with the ground, wall, and other player to stop their rotation velocity
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("Wall") || collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            theRigidbody.angularVelocity = 0f;
            falling = false;
        }
    }

}
