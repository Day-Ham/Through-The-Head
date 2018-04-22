using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TTH.Data;
using TTH.Manager;

namespace TTH.Behaviour
{
	public class Interactor : MonoBehaviour
	{
		public WeaponBehaviour Player;

		void OnTriggerStay(Collider other)
		{
			if (Input.GetButtonDown("PickUpItem"))
			{
				var interactive = other.GetComponent<Interactive>();

				if (interactive != null)
				{
					//pick up the Item
					//check if enough room
					if (PickUp(interactive))
					{
						//check if weapon and set it to inventory current if first in list
						if (interactive.isWeapon)
						{
							if (InventoryManager.Instance.weapons.Count == 1)
							{
								Player.SwitchWeapon();
							}
						}
						Destroy(other.gameObject);
					}
				}
			}
		}

		bool PickUp(Interactive item)
		{
			if (item.isWeapon)
			{
				return TTH.Manager.InventoryManager.Instance.AddWeapon(item.weapon);
			}
			return false;
		}
	}
}