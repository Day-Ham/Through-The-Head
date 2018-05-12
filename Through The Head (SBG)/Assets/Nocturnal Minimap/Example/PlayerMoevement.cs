using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoevement : MonoBehaviour {
	public float speed;
	public float rSpeed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(0, 0, speed * Input.GetAxis("Vertical") * Time.deltaTime);
		transform.Rotate(transform.up * rSpeed * Input.GetAxis("Horizontal") * Time.deltaTime);
	}
}
