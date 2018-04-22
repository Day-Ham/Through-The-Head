using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour {
	public int damage;
	public float killTime;

	private void OnCollisionEnter(Collision collision)
	{
		Destroy(gameObject, killTime);
	}
}
