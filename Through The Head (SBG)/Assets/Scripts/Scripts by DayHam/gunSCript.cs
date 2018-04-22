using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunSCript : MonoBehaviour {

	public float damage =10f;
	public float range = 100f;

	public Camera camera;

	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)) {
		
			Shoot ();

		}
		
	}
	void Shoot(){
		RaycastHit rayHit;

		if (Physics.Raycast (camera.transform.position, camera.transform.forward, out rayHit, range)) {
			Debug.Log (rayHit.transform.name);
			targetDestroy target = rayHit.transform.GetComponent<targetDestroy> ();

			if (target != null)
			{
				target.TakeDamage (damage);
			}
		}
			
	}
}
