using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TTH.Data;
using TTH.Manager;

namespace TTH.Behaviour
{
	#region Property Drawers
	[System.Serializable]
	public class WeaponInfo
	{
		public Weapon currentWeapon;
		public KeyCode WeaponSwitchKey;
		public GameObject BulletPrefab;
	}

	[System.Serializable]
	public class WeaponStats
	{
		public float bulletSpeed;
		public int bulletDamage;

		[HideInInspector]
		public bool disableSwapping = false;
		[HideInInspector]
		public bool showGunOnce = true;
		[HideInInspector]
		public int kHeldTicker = 0;
		[HideInInspector]
		public WeaponState weaponState = WeaponState.Idle;
	}
	#endregion

	public class WeaponBehaviour : MonoBehaviour
	{
		private Transform currentWeaponTransform;
		private Transform cam;

		[Header("From Scene")]
		public Transform handPosition;
		public CanvasGroup slotHolder;
		public Image[] slots;
		public RectTransform selected;
		public Sprite noItem;
		public Text BulletAmount;
		public Text GrenadeAmount;
		public Text WeaponName;
		[Space]
		public WeaponStats weaponStats;
		[Space]
		public WeaponInfo weaponOptions;

		// Use this for initialization
		void Start()
		{
			//get cam
			cam = GetComponentInChildren<Camera>().transform;

			//set initial weapon
			SetWeapon();
			SetSlots();
		}

		// Update is called once per frame
		void Update()
		{
			#region Weapon Switching
			if (Input.GetKeyUp(weaponOptions.WeaponSwitchKey))
			{
				if (!weaponStats.disableSwapping)
				{
					SwitchWeapon();

					//complete tutorial
					if (TutorialManager.Instance.currentTutorial == Tutorials.weapon_k_1)
					{
						TutorialManager.Instance.currentTutorial = Tutorials.weapon_k_2;
						TutorialManager.Instance.ChangeValue();
					}
				}
				else
				{
					weaponStats.disableSwapping = false;
				}

				//fix ui showing
				HideGunUI();
				weaponStats.showGunOnce = true;
				weaponStats.kHeldTicker = 0;
			}

			if (Input.GetKey(weaponOptions.WeaponSwitchKey))
			{
				if (weaponStats.kHeldTicker >= 10)
				{
					//show ui
					if (weaponStats.showGunOnce)
					{
						ShowGunUI();
						weaponStats.showGunOnce = false;

						//complete tutorial
						if (TutorialManager.Instance.currentTutorial == Tutorials.weapon_k_2)
						{
							TutorialManager.Instance.currentTutorial = Tutorials.weapon_k_3;
							TutorialManager.Instance.ChangeValue();
						}
					}

					//allow for number switching
					NumberSwitching();
				}
				else
				{
					weaponStats.kHeldTicker++;
				}
			}
			#endregion

			//Usage
			if (Input.GetButtonDown("Fire1"))
			{
				Fire();
			}

			if (Input.GetMouseButtonDown(1))
			{
				Aim();
			}
			else if (Input.GetMouseButtonUp(1))
			{
				CancelAim();
			}
		}

		#region Number Switching Ability
		void NumberSwitching()
		{
			switch (Input.inputString)
			{
				case "1":
					//check weapons to see what slots are usable
					if (InventoryManager.Instance.weapons.Count > 0)
					{
						//set weapon to slot picked
						weaponOptions.currentWeapon = InventoryManager.Instance.weapons[0];
						NumberSelected();
					}
					break;
				case "2":
					if (InventoryManager.Instance.weapons.Count > 1)
					{
						weaponOptions.currentWeapon = InventoryManager.Instance.weapons[1];
						NumberSelected();
					}
					break;
				case "3":
					if (InventoryManager.Instance.weapons.Count > 2)
					{
						weaponOptions.currentWeapon = InventoryManager.Instance.weapons[2];
						NumberSelected();
					}
					break;
				case "4":
					if (InventoryManager.Instance.weapons.Count > 3)
					{
						weaponOptions.currentWeapon = InventoryManager.Instance.weapons[3];
						NumberSelected();
					}
					break;
				default:
					break;
			}
		}

		void NumberSelected()
		{
			SetWeapon();
			SetSlots();
			weaponStats.disableSwapping = true;

			//complete tutorial
			if (TutorialManager.Instance.currentTutorial == Tutorials.weapon_k_3)
			{
				TutorialManager.Instance.currentTutorial = Tutorials.weapon_k_4;
				TutorialManager.Instance.ChangeValue();
			}
		}
		#endregion

		#region UI Handling
		void ShowGunUI()
		{
			slotHolder.alpha = 1;
		}

		void HideGunUI()
		{
			slotHolder.alpha = 0;
		}

		void FixBulletText()
		{
			BulletAmount.text = weaponOptions.currentWeapon.bulletCount.ToString();
		}

		public void SetSlots()
		{
			int i = 0;
			foreach (Image slot in slots)
			{
				slot.sprite = noItem;
				if (InventoryManager.Instance.weapons.Count - 1 >= i)
				{
					slot.sprite = InventoryManager.Instance.weapons[i].icon;
					i++;
				}
			}

			if (weaponOptions.currentWeapon != null)
			{
				selected.position = slots[InventoryManager.Instance.weapons.FindIndex(o => o == weaponOptions.currentWeapon)].rectTransform.position;
			}
		}

		#endregion

		#region Weapon Handling

		public void SetWeapon()
		{
			if (weaponOptions.currentWeapon != null)
			{
				if (currentWeaponTransform)
				{
					Destroy(currentWeaponTransform.gameObject);
				}

				var weapon = Instantiate(weaponOptions.currentWeapon.prefab, handPosition);
				currentWeaponTransform = weapon.transform;
				weapon.transform.position = handPosition.position;
				weapon.transform.rotation = handPosition.rotation;

				WeaponName.text = weaponOptions.currentWeapon.name;

				if (weaponOptions.currentWeapon.firesBullets && weaponOptions.currentWeapon.bulletCount >= 0)
				{
					BulletAmount.text = weaponOptions.currentWeapon.bulletCount.ToString();
				}
				else
				{
					BulletAmount.text = "∞";
				}

				//if aiming set position
				if (weaponStats.weaponState == WeaponState.Aiming)
				{
					Aim();
				}
			}
		}

		public void SwitchWeapon()
		{
			if (weaponOptions.currentWeapon != null)
			{
				if (InventoryManager.Instance.weapons.FindIndex(o => o == weaponOptions.currentWeapon) + 1 <= InventoryManager.Instance.weapons.Count - 1)
				{
					weaponOptions.currentWeapon =
						InventoryManager.Instance.weapons[InventoryManager.Instance.weapons.FindIndex(o => o == weaponOptions.currentWeapon) + 1];
				}
				else
				{
					//end of index
					weaponOptions.currentWeapon = InventoryManager.Instance.weapons[0];
				}

				SetWeapon();
				SetSlots();
			}
			else
			{
				if (InventoryManager.Instance.weapons.Count > 0)
				{
					weaponOptions.currentWeapon = InventoryManager.Instance.weapons[0];

					SetWeapon();
					SetSlots();
				}
			}
		}
		#endregion

		#region Weapon Usage

		void Aim()
		{
			if (weaponOptions.currentWeapon != null)
			{
				if (weaponOptions.currentWeapon.Aims)
				{
					weaponStats.weaponState = WeaponState.Aiming;
					var pos = cam.GetChild(0).position;
					currentWeaponTransform.parent = cam.GetChild(0);
					currentWeaponTransform.position = pos;
					currentWeaponTransform.rotation = cam.GetChild(0).rotation;
				}
				else
				{
					weaponStats.weaponState = WeaponState.Idle;
					CancelAim();
				}
			}
		}

		void CancelAim()
		{
			if (weaponOptions.currentWeapon != null)
			{
				//Stop aiming
				weaponStats.weaponState = WeaponState.Idle;
				currentWeaponTransform.parent = cam.parent;
				currentWeaponTransform.position = handPosition.position;
				currentWeaponTransform.rotation = handPosition.rotation;
			}
		}

		void Fire()
		{
			if (weaponOptions.currentWeapon != null)
			{
				var rot = handPosition.transform.forward;
				bool e = false; //was aiming
				if (weaponStats.weaponState == WeaponState.Aiming)
				{
					e = true;
				}
				//play shooting animations and fire bullets
				if (weaponOptions.currentWeapon.firesBullets)
				{
					if (weaponOptions.currentWeapon.bulletCount > 0)
					{
						//shoot gun
						//set weapon to firing state

						WeaponState s = weaponStats.weaponState;

						weaponStats.weaponState = WeaponState.Firing;

						//instantiate bullet
						var bullet = Instantiate(weaponOptions.BulletPrefab,
							currentWeaponTransform.position + weaponOptions.currentWeapon.bulletOffset,
							weaponOptions.BulletPrefab.transform.rotation);


						if (s == WeaponState.Aiming)
						{
							//set velocity
							bullet.GetComponent<Rigidbody>().velocity = weaponStats.bulletSpeed * cam.forward;

							//set direction
							bullet.transform.up = bullet.GetComponent<Rigidbody>().velocity.normalized;
						}
						else
						{
							//set velocity
							bullet.GetComponent<Rigidbody>().velocity = weaponStats.bulletSpeed * -handPosition.transform.right;

							//set direction
							bullet.transform.up = bullet.GetComponent<Rigidbody>().velocity.normalized;
						}

						//destroy after timed interval
						Destroy(bullet, weaponOptions.currentWeapon.bulletDestroyTime);

						//set bullet damage
						bullet.GetComponent<BulletBehaviour>().damage = weaponStats.bulletDamage;

						//decrease bullet count
						weaponOptions.currentWeapon.bulletCount--;
						//fix bullet text
						FixBulletText();
					}
					else
					{
						Debug.Log("Not enough Bullets");
					}
				}
				else
				{
					//not a weapon you fire

				}

				//TODO: set weapon to idle after animation
				if (e)
				{
					weaponStats.weaponState = WeaponState.Aiming;
				}
				else
				{
					weaponStats.weaponState = WeaponState.Idle;
				}

				//complete tutorial
				if (TutorialManager.Instance.currentTutorial == Tutorials.weapon_k_4)
				{
					TutorialManager.Instance.currentTutorial = Tutorials.weapon_k_5;
					TutorialManager.Instance.ChangeValue();
				}
			}
		}

		#endregion
	}
}