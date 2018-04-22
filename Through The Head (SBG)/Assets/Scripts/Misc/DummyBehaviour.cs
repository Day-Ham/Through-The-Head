using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TTH.Manager;
using TTH.Data;

namespace TTH.Misc
{
	public class DummyBehaviour : MonoBehaviour {
		public float height = 10;//max height of Box's movement
		public float speed;
		private float yCenter;
		public Bounds bounds;

		private void Start()
		{
			yCenter = transform.position.y;
		}

		void Update() {
			transform.position =
				new Vector3(
					transform.position.x,
					yCenter + Mathf.PingPong(Time.time * speed, height) - height / 2f,
					transform.position.z);
		}

		private void OnCollisionEnter(Collision other)
		{
			if (other.collider.tag == "Bullet")
			{
				Instantiate(gameObject, new Vector3(
					Random.Range(bounds.min.x, bounds.max.x),
					Random.Range(yCenter - bounds.min.y, yCenter + bounds.max.y),
					Random.Range(bounds.min.z, bounds.max.z)),
					Quaternion.identity);

				//complete tutorial
				if (TutorialManager.Instance.currentTutorial == Tutorials.weapon_k_5)
				{
					TutorialManager.Instance.currentTutorial = Tutorials.Congrats;
					TutorialManager.Instance.ChangeValue();
				}

				Destroy(gameObject);
			}
		}
	}
}