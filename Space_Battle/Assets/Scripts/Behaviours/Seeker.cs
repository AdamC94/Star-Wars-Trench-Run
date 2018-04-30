using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker : SteeringBehaviour 
{
	public GameObject targetGameObject = null;
	public Vector3 target = Vector3.zero;

	public void OnDrawGizmos()
	{
		if (isActiveAndEnabled && Application.isPlaying)
		{
			Gizmos.color = Color.cyan;
			if (targetGameObject != null)
			{
				target = targetGameObject.transform.position;
			}
			Gizmos.DrawLine(transform.position, target);
		}
	}

	public override Vector3 Calculate()
	{
		return boid.SeekForce(target);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(targetGameObject != null)
		{
			target = targetGameObject.transform.position;
		}
	}
}
