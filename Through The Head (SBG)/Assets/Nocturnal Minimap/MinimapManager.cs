using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapManager : MonoBehaviour {
	public static MinimapManager Instance;

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

	public Transform pPosition;
	public RectTransform image;
	public float detectionDistance;
	public float minimapMultiplier;

	[HideInInspector]
	public Vector3 referencePosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void LateUpdate()
	{
		referencePosition = pPosition.localPosition;

		var e = transform.GetChild(0).eulerAngles;
		e.z = pPosition.eulerAngles.y;
		transform.eulerAngles = e;
	}

	public GameObject UpdatePosition(Vector2 pos, GameObject image)
	{
		var obj = Instantiate(image, (new Vector3(pos.x, pos.y)) * minimapMultiplier, 
			Quaternion.identity, transform.GetChild(0));
		return obj;
	}

	public void UpdatePositionNoInstantiate(Vector2 pos, GameObject refObject)
	{
		refObject.transform.localPosition = (new Vector3(pos.x, pos.y)) * minimapMultiplier;
	}
}
