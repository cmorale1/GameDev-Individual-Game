using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementScript : MonoBehaviour {

    public float walkSpeed = 10f;
    public float jumpPower = 300f;
    public string teamName = "";

    public LayerMask groundMask;
    public float groundRadius = 0.1f;

    private Rigidbody2D theRigidbody;
    private Transform groundCheckLeft;
    private Transform groundCheckRight;

    private Vector3 resetPosition;
    private Quaternion groundedRotationPosition;
    private float originalAngle = 0.0f;
    
    //private Animator anim;

	// Use this for initialization
	void Start () {
        resetPosition = transform.position;
        groundedRotationPosition = transform.rotation;
        theRigidbody = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        groundCheckLeft = transform.Find("LeftGround");
        groundCheckRight = transform.Find("RightGround");
	}
	
	// Update is called once per frame
	void Update () {
        float inputX = Input.GetAxis(teamName+"_Horizontal");
        theRigidbody.velocity = new Vector2(inputX * walkSpeed, theRigidbody.velocity.y);

        bool grounded = Physics2D.OverlapCircle(groundCheckLeft.position, groundRadius, groundMask) && Physics2D.OverlapCircle(groundCheckRight.position, groundRadius, groundMask);
        bool jumping = Input.GetButtonDown(teamName+"_Jump");
        if (grounded && jumping)
        {
            theRigidbody.velocity = new Vector2(theRigidbody.velocity.x, 0f);
            theRigidbody.AddForce(new Vector2(0, jumpPower));
        }
        if(!grounded && jumping)
        {
            transform.rotation = new Quaternion(groundedRotationPosition.x, groundedRotationPosition.y, groundedRotationPosition.z, groundedRotationPosition.w);
        }
        //anim.SetFloat("speed", Mathf.Abs(theRigidbody.velocity.x));
    }
}
