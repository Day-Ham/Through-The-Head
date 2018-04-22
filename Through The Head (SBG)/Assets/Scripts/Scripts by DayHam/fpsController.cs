using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fpsController : MonoBehaviour {

	private Animator anim;
	public float speed = 10.0f;
	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		anim = GetComponent<Animator> ();
		
	}
	
	// Update is called once per frame
	void Update () {

		float forbackward = Input.GetAxisRaw("Vertical") *speed;
		float  Leright= Input.GetAxisRaw("Horizontal") * speed;
		forbackward *= Time.deltaTime;
		Leright	*= Time.deltaTime;

		transform.Translate (Leright, 0, forbackward);

		if (Input.GetKeyDown ("escape")) {
			Cursor.lockState = CursorLockMode.None;
		}

		if (anim == null) return;
		var y= Input.GetAxisRaw("Vertical");
		var x= Input.GetAxisRaw("Horizontal");
		Move (x,y);



		
		
	}

	private void Move(float x,float y){
	
		anim.SetFloat ("MoveX",x);
		anim.SetFloat ("MoveY",y);

	}
}
