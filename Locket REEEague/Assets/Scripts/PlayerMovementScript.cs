using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementScript : MonoBehaviour {

    public float walkSpeed = 10f;
    private float boostSpeed = 20f;
    public float jumpPower = 500f;
    private float rotationSpeed = 0f;
    private float rotationRight = 360f;
    private float rot = 0f;
    public string teamName = "";

    public LayerMask groundMask;
    public float groundRadius = 0.1f;

    private Rigidbody2D theRigidbody;
    private Transform groundCheckLeft;
    private Transform groundCheckRight;

    private Vector3 resetPosition;
    private Quaternion groundedRotationPosition;
    private Quaternion originalAngle;

    private bool firstJump;
    private bool airRolling;
    private bool falling;

    //private Animator anim;

	// Use this for initialization
	void Start () {
        firstJump = false;
        airRolling = false;
        falling = false;
        rotationRight = 360f;
        
        originalAngle = transform.rotation;
        resetPosition = transform.position;
        groundedRotationPosition = transform.rotation;
        theRigidbody = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        groundCheckLeft = transform.Find("LeftGround");
        groundCheckRight = transform.Find("RightGround");
	}
	
	// Update is called once per frame
	void Update () {
        movePlayer();
        //anim.SetFloat("speed", Mathf.Abs(theRigidbody.velocity.x));
    }

    void movePlayer()
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
