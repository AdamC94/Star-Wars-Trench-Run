using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourController : MonoBehaviour 
{
	public enum Attack_Flee{Attacking, Fleeing, Trench_Run,  None}
	public Attack_Flee currentAttackOrFlee;

	public enum Behaviour{Pursue, OffsetPursue , Wander, Seek}
	public Behaviour currentBehaviour;

	public int previousBehaviour;

	public Pursue pursue;
	public Seeker seeker;
	public NoiseWander wander;
	public OffsetPursue offsetPursue;
	public PathFollowing path;

	public GameObject centre;
	public float distanceFromCentre;

	CamerSceneManager cameraSceneManager;

	// Use this for initialization
	void Start () 
	{
		cameraSceneManager = Camera.main.GetComponent<CamerSceneManager>();

		pursue = this.GetComponent<Pursue>();
		seeker = this.GetComponent<Seeker>();
		wander = this.GetComponent<NoiseWander>();
		offsetPursue = this.GetComponent<OffsetPursue>();
		path = this.GetComponent<PathFollowing>();

		path.enabled = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if(cameraSceneManager.battle == true)
		{
			if(Vector3.Distance(centre.transform.position, this.transform.position) > distanceFromCentre && currentAttackOrFlee != Attack_Flee.Trench_Run && cameraSceneManager.currentScene == CamerSceneManager.scene.Scene12)
			{
				seeker.targetGameObject = centre;
				currentBehaviour = Behaviour.Seek;
			}
			else if(Vector3.Distance(centre.transform.position, this.transform.position) < distanceFromCentre)
			{
				currentBehaviour = Behaviour.Wander;
			}
		}
			
		if(currentAttackOrFlee == Attack_Flee.Attacking)
		{
			currentBehaviour = Behaviour.Seek;
		}

		if(currentAttackOrFlee == Attack_Flee.Fleeing)
		{
			currentBehaviour = Behaviour.Wander;
		}

		if(currentAttackOrFlee == Attack_Flee.Trench_Run)
		{
			currentBehaviour = Behaviour.Seek;
			path.enabled = true;
		}

		if(seeker.targetGameObject == null && currentAttackOrFlee == Attack_Flee.Attacking)
		{
			currentAttackOrFlee = Attack_Flee.Fleeing;
			//currentBehaviour = Behaviour.OffsetPursue;
		}


		if(currentBehaviour == Behaviour.Pursue)
		{
			pursue.enabled = true;
			seeker.enabled = false;
			wander.enabled = false;
			offsetPursue.enabled = false;
			path.enabled = false;
		}

		if(currentBehaviour == Behaviour.Seek)
		{
			pursue.enabled = false;
			seeker.enabled = true;
			wander.enabled = false;
			offsetPursue.enabled = false;

			if(currentAttackOrFlee != Attack_Flee.Trench_Run)
			{
				path.enabled = false;
			}
		}

		if(currentBehaviour == Behaviour.Wander)
		{
			pursue.enabled = false;
			seeker.enabled = false;
			wander.enabled = true;
			offsetPursue.enabled = false;
			path.enabled = false;
		}

		if(currentBehaviour == Behaviour.OffsetPursue)
		{
			pursue.enabled = false;
			seeker.enabled = false;
			wander.enabled = false;
			offsetPursue.enabled = true;
			path.enabled = false;
		}
	}
}
