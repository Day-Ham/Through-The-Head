using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor; 
#endif

namespace TTH.Data
{
	[CreateAssetMenu(fileName = "New Weapon", menuName = "Items/Weapon")]
	public class Weapon : ScriptableObject
	{
		new public string name;
		public GameObject prefab;
		public Sprite icon;
		[Space]
		public bool Aims = true;

		[HideInInspector]
		public bool firesBullets = true;
		[HideInInspector]
		public Vector3 bulletOffset;
		[HideInInspector]
		public int bulletCount;
		[HideInInspector]
		public float bulletDestroyTime = 5;
	}

	public enum WeaponState
	{
		Idle,
		Aiming,
		Firing
	}
}

#if UNITY_EDITOR
[CustomEditor(typeof(TTH.Data.Weapon))]
[CanEditMultipleObjects]
public class MyScriptEditor : Editor
{
	override public void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		var myScript = target as TTH.Data.Weapon;

		EditorGUILayout.Space();
		
		myScript.firesBullets = GUILayout.Toggle(myScript.firesBullets, "Fire Bullets");

		if (myScript.firesBullets)
		{
			myScript.bulletOffset = EditorGUILayout.Vector3Field("Bullet Offset:", myScript.bulletOffset);
			myScript.bulletCount = EditorGUILayout.IntField("Bullet Count:", myScript.bulletCount);
			myScript.bulletDestroyTime = EditorGUILayout.FloatField("Bullet Destroy Time:", myScript.bulletDestroyTime);
		}

	}
}
#endif