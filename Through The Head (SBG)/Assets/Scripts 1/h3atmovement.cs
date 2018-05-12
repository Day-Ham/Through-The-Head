using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
public class h3atmovement : MonoBehaviour {

		float horizontal;
		float vertical;

		bool aimInput;
		bool sprintInput;
		bool shootInput;
		bool crouchInput;
		bool reloadInput;
		bool pivotInput;

		bool isInit;

		float delta;
		public StageManager states;
		public Transform camHolder;

		void Start(){
		
			InitInGame ();
			
		}

		public void InitInGame(){
			isInit = true;

		}


		void FixedUpdate(){
			if (!isInit) {
				return;

				delta = Time.deltaTime;
				InGame_UpdateStates_FixedUpdate ();
				states.FixedTick (delta);
				
			}
		}
		void GetInput_FixedUpdate(){
			vertical = Input.GetAxis ("Vertical");
			horizontal = Input.GetAxis ("Horizontal");
		
		}

		void InGame_UpdateStates_FixedUpdate(){
			states.imp.Horizontal = horizontal;
			states.imp.Vertical = vertical;
			states.imp.moveAmount = Mathf.Clamp01 (Mathf.Abs (horizontal) + Mathf.Abs (vertical));

			Vector3 moveDir = camHolder.forward * vertical;
			moveDir += camHolder.right * horizontal;
			moveDir.Normalize ();
			states.imp.moveDirection = moveDir;


		}

		void Update(){
			return;

			delta = Time.deltaTime;
			states.Tick (delta);
		}


}
	public enum GamePhase{

		inGame,inMenu
	}
}