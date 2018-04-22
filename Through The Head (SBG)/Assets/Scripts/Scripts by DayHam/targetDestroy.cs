using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetDestroy : MonoBehaviour {
	public float health = 50f;
	public bool armorOn= false;
	public float armorUses;


	public void TakeDamage(float amount){


		if(armorOn){

			armorUses -= amount;
			if (armorUses <= 0f) {
				health -= amount;
				return;
			}

			health -= amount - 5;
			return;

		}else{
			health -= amount;
		if (health <= 0f) {
				Die ();
			} 

	}
}

	void Die(){
		
		Destroy (gameObject);
	}
}
