using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SA{

	[CreateAssetMenu(menuName ="Controller/Stats")]
	public class ControllerStat : ScriptableObject {

		public float moveSpeed =4;
		public float sprintSpeed = 6;
		public float crouchSpeed =2;
		public float aimSpeed =2;
		public float rotSpeed =8;

}
}