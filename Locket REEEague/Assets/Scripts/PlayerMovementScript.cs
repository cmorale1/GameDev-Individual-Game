using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementScript : MonoBehaviour {

    public float walkSpeed = 10f;
    private float boostSpeed = 20f;
    public float jumpPower = 500f;
    public string teamName = "";

    public LayerMask groundMask;
    public float groundRadius = 0.1f;

    private Rigidbody2D theRigidbody;
    private Transform groundCheckLeft;
    private Transform groundCheckRight;

    private Vector3 resetPosition;
    private Quaternion groundedRotationPosition;
    private float originalAngle = 0.0f;

    private bool firstJump;
    private bool boosting;

    //private Animator anim;

	// Use this for initialization
	void Start () {
        firstJump = false;
        boosting = false;
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
            theRigidbody.velocity = new Vector2(theRigidbody.velocity.x, 0f);
            theRigidbody.AddForce(new Vector2(0, jumpPower));
            firstJump = false;
        }
        if (Input.GetButtonDown(teamName + "_Boost") && !boosting)
        {
            boosting = true;
        }
        if (Input.GetButtonUp(teamName + "_Boost") && boosting)
        {
            boosting = false;
        }
        if (boosting)
        {
            theRigidbody.velocity = new Vector2(inputX * boostSpeed, theRigidbody.velocity.y);
        }
        if (!boosting)
        {
            theRigidbody.velocity = theRigidbody.velocity = new Vector2(inputX * walkSpeed, theRigidbody.velocity.y);
        }
        if (!grounded && jumping)
        {
            transform.rotation = new Quaternion(groundedRotationPosition.x, groundedRotationPosition.y, groundedRotationPosition.z, groundedRotationPosition.w);
        }
    }
}
