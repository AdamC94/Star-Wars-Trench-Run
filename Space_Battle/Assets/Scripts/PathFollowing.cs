using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollowing : MonoBehaviour 
{
	public float distanceFromWayPoint;

	public int pathIndex;
	public GameObject[] path;

	Seeker seeker;

	Gun gun;

	// Use this for initialization
	void Start () 
	{
		seeker = this.GetComponent<Seeker>();

		gun = GetComponentInChildren<Gun>();
	}

	void NextWayPoint()
	{
		seeker.targetGameObject = path[pathIndex];

		if(Vector3.Distance(this.transform.position, path[pathIndex].transform.position) <= distanceFromWayPoint)
		{
			if(pathIndex != path.Length - 1)
			{
				pathIndex ++;
				return;
			}
			else if(pathIndex >= path.Length - 1)
			{
				pathIndex = 0;
			}
		}
	}

	// Update is called once per frame
	void Update () 
	{
		NextWayPoint();
	}
}
