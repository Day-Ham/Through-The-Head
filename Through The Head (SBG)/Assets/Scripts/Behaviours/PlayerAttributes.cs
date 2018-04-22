using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TTH.Behaviour
{
	public class PlayerAttributes : MonoBehaviour
	{
		public float maxHealth = 100;
		private float currentHealth;

		public Slider healthBar;
		public float healthDecreaseSpeed;

		private void Start()
		{
			currentHealth = maxHealth;
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.I))
			{
				TakeDamage(10);
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.tag == "Bullet")
			{
				TakeDamage(other.GetComponent<BulletBehaviour>().damage);
			}
		}

		public void TakeDamage(int amount)
		{
			if (currentHealth - amount > 0)
			{
				currentHealth -= amount;
			}
			else
			{
				Debug.Log("Kill/GameOver");
				currentHealth = 0;
			}

			StartCoroutine(DecreaseHealth());
		}

		IEnumerator DecreaseHealth()
		{
			while (healthBar.value > currentHealth)
			{
				healthBar.value -= healthDecreaseSpeed;
				yield return new WaitForEndOfFrame();
			}
			healthBar.value = currentHealth;
		}
	}
}