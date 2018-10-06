using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementScript : MonoBehaviour {

    public float walkSpeed = 10f;
    public float jumpPower = 500f;
    private float rotationSpeed = 0f;
    private float rotationRight = 360f;
    private float rot = 0f;
    public string teamName;

    public LayerMask groundMask;
    public float groundRadius = 0.1f;

    private Rigidbody2D theRigidbody;
    private Transform groundCheckLeft;
    private Transform groundCheckRight;

    private Quaternion groundedRotationPosition;

    private bool firstJump;
    private bool airRolling;
    private bool falling;

    private SpriteRenderer vehicleSpRend;
    private float rVal, gVal, bVal;

	// Use this for initialization
	void Start () {
        firstJump = false;
        airRolling = false;
        falling = false;
        rotationRight = 360f;
        groundedRotationPosition = transform.rotation;
        theRigidbody = GetComponent<Rigidbody2D>();
        groundCheckLeft = transform.Find("LeftGround");
        groundCheckRight = transform.Find("RightGround");
        vehicleSpRend = GetComponent<SpriteRenderer>();
        if(teamName == "TeamA")
        {
            rVal = PlayerPrefs.GetFloat("LeftImage_RedValue");
            gVal = PlayerPrefs.GetFloat("LeftImage_GreenValue");
            bVal = PlayerPrefs.GetFloat("LeftImage_BlueValue");
        }
        if(teamName == "TeamB")
        {
            rVal = PlayerPrefs.GetFloat("RightImage_RedValue");
            gVal = PlayerPrefs.GetFloat("RightImage_GreenValue");
            bVal = PlayerPrefs.GetFloat("RightImage_BlueValue");
        }

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
                transform.Rotate(Vector3.back * Time.deltaTime * 200f);
            }
            if(teamName == "TeamB")
            {
                transform.Rotate(Vector3.forward * Time.deltaTime * 200f);
            }
        }
        if (!airRolling)
        {
            theRigidbody.velocity = theRigidbody.velocity = new Vector2(inputX * walkSpeed, theRigidbody.velocity.y);
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
                rotationSpeed = 400f;
                rot = rotationSpeed * Time.deltaTime;
                rotationRight -= rot;
                transform.Rotate(0, 0, -rot);
            }
            if(teamName == "TeamB")
            {
                rotationSpeed = 400f;
                rot = rotationSpeed * Time.deltaTime;
                rotationRight -= rot;
                transform.Rotate(0, 0, rot);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            theRigidbody.angularVelocity = 0f;
            falling = false;
        }
        if(collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            theRigidbody.angularVelocity = 0f;
            falling = false;
        }
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            theRigidbody.angularVelocity = 0f;
            falling = false;
        }
    }

}
