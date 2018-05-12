using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapObject : MonoBehaviour {
	public GameObject image;
	private GameObject myInstance;
	MinimapManager man;

	// Use this for initialization
	void Start () {
		man = MinimapManager.Instance;
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(transform.position, man.referencePosition) < man.detectionDistance)
		{
			var x = transform.position.x - man.referencePosition.x;
			var y = transform.position.z - man.referencePosition.z;
			if (!myInstance)
			{
				myInstance = man.UpdatePosition(new Vector2(x, y), image);
			}
			else
			{
				man.UpdatePositionNoInstantiate(new Vector2(x, y), myInstance);
			}
		}
		else
		{
			Destroy(myInstance);
		}
	}
}
