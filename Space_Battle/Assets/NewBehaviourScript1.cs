using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript1 : MonoBehaviour 
{

	public int numberOfOrbitRings;
	public int[] numberOfObjectsPerRing;
	public float radiusOffset;
	public float rotYOffset;

	public int numberOfObjects;
	public float radius;

	Vector3 pos;

	public GameObject prefab;

	public float speed;

	float angle;

	// Use this for initialization
	void Start () 
	{
	}

	void SpawnObjects()
	{
		pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

		for(int i = 0; i < numberOfOrbitRings; i ++)
		{
			for(int j = 0; j < numberOfObjectsPerRing[i]; j ++)
			{
				angle = j * Mathf.PI * 2 / numberOfObjectsPerRing[i] + i;

				Vector3 temp;
				temp = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
				Vector3 objectPos = new Vector3(pos.x + temp.x, pos.y + temp.y, pos.z + temp.z);

				GameObject prefabInstance;
				prefabInstance = Instantiate(prefab, objectPos, Quaternion.identity) as GameObject;
				prefabInstance.transform.parent = this.transform;

			}

			radius += radiusOffset;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetMouseButtonDown(0))
		{
			SpawnObjects();
		}
	}
}
