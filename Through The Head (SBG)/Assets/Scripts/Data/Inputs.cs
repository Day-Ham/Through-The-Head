using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TTH.Data
{
	public enum InputType
	{
		keyboard,
		controller,
		touchScreen
	}

	public class Inputs : MonoBehaviour
	{
		#region Singleton
		public static Inputs Instance;

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
		#endregion

		[HideInInspector]
		public InputType current;

		private void Start()
		{
			if (Input.mousePresent)
			{
				current = InputType.keyboard;
			}

			//joystick (controller)
			if (Input.GetJoystickNames().Length > 0)
			{
				current = InputType.controller;
			}
			//mobile
			else if (
				Application.platform == RuntimePlatform.Android ||
				Application.platform == RuntimePlatform.IPhonePlayer)
			{
				current = InputType.touchScreen;

				if (Input.mousePresent)
				{
					current = InputType.keyboard;
				}
			}
		}
	}
}