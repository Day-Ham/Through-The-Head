using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TTH.Data
{
	static class Tutorials
	{
		#region Movement
		public static string move_1 = "Use WASD keys to move around.";
		public static string move_2 = "Use the mouse to look.";
		public static string move_3 = "Press space to jump.";
		#endregion

		#region Weapon Interaction
		public static string weapon_k_1 = "Press Q to switch weapons.";
		public static string weapon_k_2 = "Now hold Q to open the current weapon UI.";
		public static string weapon_k_3 = "Press a number to choose a weapon.";
		public static string weapon_k_4 = "Click the fire button to use the weapon.";
		public static string weapon_k_5 = "Now shoot one of the dummies.";
		#endregion

		public static string Congrats = "Congrats! You know the basics!";
	}
}

namespace TTH.Manager
{
	public class TutorialManager : MonoBehaviour
	{
		public static TutorialManager Instance;

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

		public UnityEngine.UI.Text tutorialText;

		[HideInInspector]
		public string currentTutorial;

		private void Start()
		{
			currentTutorial = Data.Tutorials.move_1;
			ChangeValue();
		}

		public void ChangeValue()
		{
			tutorialText.text = currentTutorial;
		}

		/*
		//complete tutorial
		if (TutorialManager.Instance.currentTutorial == Tutorials.weapon_k_4)
		{
			TutorialManager.Instance.currentTutorial = Tutorials.Congrats;
			TutorialManager.Instance.ChangeValue();
		}
		*/
	}
}
 