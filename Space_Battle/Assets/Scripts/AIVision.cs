using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIVision : MonoBehaviour 
{

	public float visionRadius;
	[Range(0, 360)]
	public float visionAngle;

	public bool canSeePlayer;

	public LayerMask playerMask;
	public LayerMask obstructionMask;

	public List<Transform> playerInView = new List<Transform>();

	BehaviourController behaviourController;

	public bool debug;
	// Use this for initialization
	void Start () 
	{
		behaviourController = GetComponentInParent<BehaviourController>();
	}
		
	void FieldOfVision() 
	{
		canSeePlayer = false;
		playerInView.Clear();
		Collider[] playerInTargetRadius = Physics.OverlapSphere(transform.position, visionRadius, playerMask); 

		for(int i = 0; i < playerInTargetRadius.Length; i ++)
		{
			Transform player = playerInTargetRadius[i].transform;
			Vector3 directionToTarget = (player.position - transform.position).normalized;

			if(Vector3.Angle(transform.forward, directionToTarget) < visionAngle / 2)
			{
				float distanceToTarget = Vector3.Distance(transform.position, player.position);

				if(!Physics.Raycast ( transform.position, directionToTarget, distanceToTarget, obstructionMask))
				{
					playerInView.Add(player);

					canSeePlayer = true;

					if(canSeePlayer == true && behaviourController.currentAttackOrFlee != BehaviourController.Attack_Flee.Trench_Run)
					{
						behaviourController.currentAttackOrFlee = BehaviourController.Attack_Flee.Attacking;
						behaviourController.seeker.targetGameObject = player.gameObject;

						//player.GetComponent<BehaviourController>().currentAttackOrFlee = BehaviourController.Attack_Flee.Fleeing;
					}
				}
				else
				{
					canSeePlayer = false;
				}
			}
		}
	}

	public Vector3 directofAngle(float angleConvertToDegrees, bool publicAngle)
	{
		if(!publicAngle)
		{
			angleConvertToDegrees += transform.eulerAngles.y;
		}

		return new Vector3(Mathf.Sin(angleConvertToDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleConvertToDegrees * Mathf.Deg2Rad));
	}
		
	void OnDrawGizmos()
	{
		if(debug == true)
		{
			Vector3 viewAngleLeft;
			Vector3 viewAngleRight;

			viewAngleLeft = directofAngle( -visionAngle / 2, false);
			viewAngleRight = directofAngle( visionAngle / 2, false);

			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(transform.position, visionRadius);

			Gizmos.color = Color.red;
			Gizmos.DrawLine(transform.position, transform.position + viewAngleLeft * visionRadius);
			Gizmos.DrawLine(transform.position, transform.position + viewAngleRight * visionRadius);
		}

		foreach(Transform player in playerInView)
		{
			Gizmos.color = Color.magenta;
			Gizmos.DrawLine(transform.position, player.position);
		}
	}
		

	// Update is called once per frame
	void FixedUpdate ()
	{
		FieldOfVision();
	}
}
