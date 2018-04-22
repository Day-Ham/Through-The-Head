using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TTH.Data;

namespace TTH.Manager
{
	public class InventoryManager : MonoBehaviour
	{
		public static InventoryManager Instance;

		public List<Weapon> weapons = new List<Weapon>();
		public int weaponLimit;

		private void Awake()
		{
			if (Instance == null)
			{
				Instance = this;
			}
			else
			{
				Destroy(gameObject);
			}
		}

		public bool AddWeapon(Weapon w)
		{
			if (weapons.Count < weaponLimit)
			{
				weapons.Add(w);

				//set that weapon to current
				

				return true;
			}
			return false;
		}
	}
}