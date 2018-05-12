using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SA {
public class StageManager : MonoBehaviour {


		public InputVariables imp;
		[System.Serializable]
		public class InputVariables
		{

			public float Horizontal;
			public float Vertical;
			public float moveAmount;
			public Vector3 moveDirection;
			public Vector3 aimPosition;


		}


	public ControllerStates states;
		[System.Serializable]
	public class ControllerStates{
			
			public bool onGround;
			public bool isAiming;
			public bool isCrouching;
			public bool isRunning;
			public bool isInteracting;

		}
			
	
	public ControllerStat stats;

	public Animator anim;
	public GameObject activeModel;
		[HideInInspector]
	public Rigidbody rigid;
		[HideInInspector]
	public Collider controllerCollider;

	List<Collider>ragdollColliders = new List<Collider>();
	List<Rigidbody>ragdollRigids= new List<Rigidbody>();
		[HideInInspector]
	public LayerMask ignoreLayers;
		[HideInInspector]
	public LayerMask ignoreForGround;

	public Transform mTransform;
	public CharState curState;
		public float delta;

	public void Init(){

		mTransform = this.transform;
		SetUpAnimator ();
		rigid = GetComponent<Rigidbody>();
		rigid.isKinematic = false;
		rigid.drag = 4;
		rigid.angularDrag = 999;
		rigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
		controllerCollider = GetComponent<Collider> ();

		SetUpRagdoll ();
		ignoreLayers =~(1 << 9);
		ignoreForGround =~(1 << 9 | 1 << 10);
	}

	void SetUpAnimator(){
	
		if (activeModel == null) {

			anim = GetComponentInChildren<Animator> ();
		}

		if (anim == null) {

			anim = activeModel.GetComponent<Animator> ();

			anim.applyRootMotion = false;
		}
	}

	void SetUpRagdoll(){

		Rigidbody[] rigids = activeModel.GetComponentsInChildren<Rigidbody> ();
			foreach(Rigidbody r in rigids){

				if(r == rigid){

					continue;
				}
			Collider c = r.gameObject.GetComponent<Collider> ();
			c.isTrigger = true;

			ragdollRigids.Add(r);
			ragdollColliders.Add(c);
			r.isKinematic = true;
			r.gameObject.layer= 10;
			}
	
	}

		void MovementNormal(){

			if (imp.moveAmount > 0.05f)
				rigid.drag = 0;
			else
				rigid.drag = 4;
			


			float speed = stats.moveSpeed;
			if (states.isRunning) {
			
				speed = stats.sprintSpeed;
			}
			if (states.isCrouching) {

				speed = stats.crouchSpeed;
			}
			Vector3 dir = Vector3.zero;
			dir = mTransform.forward * (speed * imp.moveAmount);
			rigid.velocity = dir;

			
		}

		void RotationNormal(){
			Vector3 targetDir = imp.moveDirection;

				targetDir.y = 0;
			if(targetDir == Vector3.zero){
				targetDir = mTransform.forward;
				Quaternion lookDir = Quaternion.LookRotation (targetDir);
				Quaternion targetRot = Quaternion.Slerp (mTransform.rotation, lookDir, stats.rotSpeed * delta);
				mTransform.rotation = targetRot;
			}
		}

		void MovementAiming(){
			
		}

		void HandleAnimationNormal(){

			float anim_v = imp.moveAmount;

			anim.SetFloat ("vertical", anim_v, 0.15f, delta);
		}

		public void FixedTick(float d){
			delta = d;
			switch (curState) {


			case CharState.normal: 
				states.onGround = OnGround ();

				if (states.isAiming) 
				{
					
				} else {

					RotationNormal ();
					MovementNormal ();
				}

				break;
			case CharState.onAir: 
				rigid.drag = 0;
				states.onGround = OnGround();
				break;
			case CharState.cover: 
				break;
			case CharState.vaulting: 
				break;
			default:
				break;

			}
	}

		public void Tick(float d){
		
			delta = d;

			switch (curState) {

			case CharState.normal: 
				states.onGround = OnGround ();
				HandleAnimationNormal ();
				break;
			case CharState.onAir: 
				states.onGround = OnGround();
				break;
			case CharState.cover: 
				break;
			case CharState.vaulting: 
				break;
			default:
				break;

			}

	}

		bool OnGround(){
	
		Vector3 origin = mTransform.position;
		origin.y += 0.6f;
		Vector3 dir = -Vector3.up;
		float dis = 0.7f;
		RaycastHit hit;
		if (Physics.Raycast (origin, dir, out hit, dis, ignoreForGround)) {

			Vector3 tp = hit.point;
			mTransform.position = tp;
			return true;
		}
		return false;
	}

}
	public enum CharState{

		normal,onAir,cover,vaulting
	}

}
