using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCon : MonoBehaviour {

	private Animator anim;
	/*
	public float movementSpeed = 1f;
	public float sprintSpeed =4f;
	public float crouchSpeed = .01f;
	private float verticalVelocity;
	private float gravity = 4.0f;
	private float jumpForce = 2.0f;
	*/

	public bool isSprint;
	public bool isCrouch;
	public bool isJumping;


	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		anim = GetComponent<Animator> ();
		isSprint = false;
		isJumping = false;
		isCrouch = false;

	}

	// Update is called once per frame
	void Update () {
		float currentSpeed = 0;
		//Declare theres a crouch
		isCrouch = Input.GetKey(KeyCode.Z);
		isSprint = Input.GetKey(KeyCode.LeftShift);

		if (isCrouch) {

			isCrouch = true;
		} else {

			isCrouch = false;

		}

		if (isSprint) {

		
			isSprint = true;

			if (Input.GetKey(KeyCode.S)) {

			
			}

		} else {
			
		}


		//Basic Movement 
		/*
		float forbackward = Input.GetAxisRaw("Vertical") *currentSpeed;
		float  Leright= Input.GetAxisRaw("Horizontal") * currentSpeed;
		forbackward *= Time.deltaTime;
		Leright	*= Time.deltaTime;

		transform.Translate (Leright, 0, forbackward);

		*/

		//Jump
		if (Input.GetButtonDown("Jump")) {


				isJumping = true;

		
		} else {


			isJumping = false;

		}

	


		// Animator
		if (anim == null) return;
		var y= Input.GetAxisRaw("Vertical");
		var x= Input.GetAxisRaw("Horizontal");
		anim.SetBool ("isSprint", isSprint);
		anim.SetBool ("isJump", isJumping);
		anim.SetBool ("isCrouch", isCrouch);
		Move (x,y);






	}

	private void Move(float x,float y){

		anim.SetFloat ("MoveX",x);
		anim.SetFloat ("MoveY",y);

	}
}
