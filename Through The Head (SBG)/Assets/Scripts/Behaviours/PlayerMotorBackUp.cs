using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/*
using TTH;

namespace TTH.Behaviour
{
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(CapsuleCollider))]
	public class PlayerMotor : MonoBehaviour
	{
		private Animator anim;
		private Rigidbody rb;
		private Transform cam;//Must have a child being the camera
		new private CapsuleCollider collider;

		[Header("Movement")]
		public float rotationSpeed;
		public float movementSpeed;
		public float sprintingMultiplier;
		public float jumpSpeed;
		public float jumpingRaycastDistance;
		public LayerMask jumpingMask;

		private Vector2 CurrentInput;

		[Header("Mouse Look")]
		public MouseControl mouseLook;
		private Quaternion camLocalRot, characterLocalRot;
		private float xRot, yRot;

		[Header("Locking")]
		public bool lockMovement = false;

		// Use this for initialization
		void Start()
		{
			anim = GetComponent<Animator>();
			rb = GetComponent<Rigidbody>();
			cam = GetComponentInChildren<Camera>().transform;
			collider = GetComponent<CapsuleCollider>();

			mouseLook.SetMouseLock(true);
		}

		// Update is called once per frame
		void Update()
		{
			if (!lockMovement)
			{
				//Mouse Look
				mouseLook.mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

				if (Manager.TutorialManager.Instance.currentTutorial == Data.Tutorials.move_2 &&
					(Mathf.Abs(mouseLook.mouseInput.x) > float.Epsilon || Mathf.Abs(mouseLook.mouseInput.y) > float.Epsilon))
				{
					Manager.TutorialManager.Instance.currentTutorial = Data.Tutorials.move_3;
					Manager.TutorialManager.Instance.ChangeValue();
				}

				xRot = mouseLook.mouseInput.y * mouseLook.mouseSpeedY;
				yRot = mouseLook.mouseInput.x * rotationSpeed;

				camLocalRot = cam.localRotation;
				characterLocalRot = transform.localRotation;

				camLocalRot *= Quaternion.Euler(-xRot, 0, 0);
				characterLocalRot *= Quaternion.Euler(0, yRot, 0);

				//clamp x rotation
				camLocalRot = mouseLook.ClampRotationAroundXAxis(camLocalRot);

				cam.localRotation = camLocalRot;
				transform.localRotation = characterLocalRot;
			}

			if (Input.GetKeyDown(KeyCode.Escape))//stop allowing movement
			{
				if (!lockMovement)
				{
					lockMovement = true;
					mouseLook.SetMouseLock(false);
				}
			}else if (Input.GetMouseButtonDown(0))//allow movement
			{
				if (lockMovement)
				{
					lockMovement = false;
					mouseLook.SetMouseLock(true);
				}
			}
		}

		private void FixedUpdate()
		{
			if (!lockMovement)//if locked from moving
			{
				CurrentInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

				if (Manager.TutorialManager.Instance.currentTutorial == Data.Tutorials.move_1 &&
					(Mathf.Abs(CurrentInput.x) > float.Epsilon || Mathf.Abs(CurrentInput.y) > float.Epsilon))
				{
					Manager.TutorialManager.Instance.currentTutorial = Data.Tutorials.move_2;
					Manager.TutorialManager.Instance.ChangeValue();
				}

				//Actual Movement
				rb.AddForce(transform.forward * CurrentInput.y * movementSpeed);
				rb.AddForce(transform.right * CurrentInput.x * movementSpeed);

				//jumping
				if (Input.GetButtonDown("Jump"))
				{
					RaycastHit hit;

					if (Physics.Raycast(transform.position, Vector3.down, out hit, 
						collider.bounds.extents.y + jumpingRaycastDistance, jumpingMask))
					{
						rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);


						//next tutorial
						if (Manager.TutorialManager.Instance.currentTutorial == Data.Tutorials.move_3)
						{
							Manager.TutorialManager.Instance.currentTutorial = Data.Tutorials.weapon_k_1;
							Manager.TutorialManager.Instance.ChangeValue();
						}
					}
				}
			}
		}
	}

	[System.Serializable]
	public class MouseControl
	{
		public float mouseSpeedY;
		public float minXRotation;
		public float maxXRotation;
		[HideInInspector]
		public Vector2 mouseInput;
		[HideInInspector]
		public bool _mouseLocked { get; set; }

		public void SetMouseLock(bool lockMouse)
		{
			switch(lockMouse)
			{
			case true:
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
				_mouseLocked = true;
				break;
			case false:
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
				_mouseLocked = false;
				break;
			}
		}

		public Quaternion ClampRotationAroundXAxis(Quaternion q)
		{
			q.x /= q.w;
			q.y /= q.w;
			q.z /= q.w;
			q.w = 1.0f;

			float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);
			angleX = Mathf.Clamp(angleX, minXRotation, maxXRotation);
			q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

			return q;
		}
	}
}
*/