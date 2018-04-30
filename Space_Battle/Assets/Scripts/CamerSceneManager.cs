using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerSceneManager : MonoBehaviour 
{
	public enum scene{Scene1, Scene2, Scene3, Scene4, Scene5, Scene6, Scene7, Scene8, Scene9, Scene10, Scene11, Scene12, Scene13, Scene14, Scene15, Scene16, Scene17, Scene18}
	public List<GameObject> rebelShips = new List<GameObject>();
	public GameObject[] ships;
	public GameObject leader;
	public scene currentScene;

	[Header("Scene1 Variables")]
	public float distanceTrigger;

	[Header("Scene2 Variables")]
	public float shipCameraTimer;
	public float startShipCameraTimer;
	public int count;
	public int countAmount;

	public GameObject standingBySoundEffect;
	public Transform SquadronPosition;

	[Header("Scene3&4 Variables")]

	public float timeTillNextScene;
	public GameObject deathStar;
	public Vector3[] deathStarScale;

	[Header("Scene5 Variables")]

	public float timeUntilNextScene;
	public GameObject theSizeOfThatThingSFX;

	[Header("Scene6 Variables")]

	public float timeUnityNextScene_6;

	[Header("Scene7 Variables")]

	public float timeUntilNextScene_7;
	public GameObject divePosition;

	[Header("Scene8 Variables")]

	public GameObject deathStarSurface;
	public GameObject deathStarSurfaceTarget;

	[Header("Scene9 Variables")]

	public float timeUntilNextScene_9;
	public GameObject EnemiesIncomingSFX;

	[Header("Scene10 Variables")]

	public GameObject tiefighterDivePosition;

	public GameObject tieFighterSquadron;
	public GameObject tieFighterLeader;

	public List<GameObject> empireShips = new List<GameObject>();
	public GameObject[] tieFighters;

	public float timeUntilNextScene_10;

	[Header("Scene12 Variables")]

	public bool battle;

	public float battleCameraSwitchTime;
	public float startBattleCameraSwitchTime;

	public int shipExplodeCount;
	public int maxShipExplodeCount;

	public float noCombatTimer;
	public float startNoCombatTimer;

	public List<GameObject> totalShips = new List<GameObject>();
	public List<BehaviourController> attackingShips = new List<BehaviourController>();

	[Header("Scene13 Variables")]

	public int trenchRunWayPointsIndex;
	public GameObject[] trenchRunWayPoints;

	public GameObject chosenShip;
	public GameObject chosenShip1;

	public float cameraSwitchTimer_13;
	public float startCameraSwitchTimer_13;

	public GameObject deathStarExplodeTrigger;

	[Header("Scene15 Variables")]

	public GameObject homePlanet;

	public float timeUntilNextScene_15;

	[Header("Scene16 Variables")]

	public GameObject finalCameraPosition;

	[Header("Scene17 Variables")]

	public Vector3 deathStarStartScale;

	public float timeUntilNextScene_17;

	[Header("Scene18 Variables")]

	public float deathStarExplosionTimer;

	CameraController camerController;


	public 
	// Use this for initialization
	void Start () 
	{

		camerController = this.GetComponent<CameraController>();

		startShipCameraTimer = shipCameraTimer;

		ships = GameObject.FindGameObjectsWithTag("Rebel");

		for(int i = 0; i < ships.Length; i ++)
		{
			rebelShips.Add(ships[i].gameObject);
		}

		deathStarStartScale = new Vector3(deathStar.transform.localScale.x, deathStar.transform.localScale.y, deathStar.transform.localScale.z);
	}

	void TotalShips()
	{
		//adding rebel Ships
		for(int i = 0; i < rebelShips.Count; i ++)
		{
			if(!totalShips.Contains(rebelShips[i]))
			{
				totalShips.Add(rebelShips[i]);
			}
		}

		//adding empire Ships
		for(int i = 0; i < empireShips.Count; i ++)
		{
			if(!totalShips.Contains(empireShips[i]))
			{
				totalShips.Add(empireShips[i]);
			}
		}

		//removing null ships
		for(int i = 0; i < totalShips.Count; i ++)
		{
			if(totalShips[i] == null)
			{
				totalShips.Remove(totalShips[i]);
			}
		}
	}

	void AttackingShips()
	{
		for(int i = 0; i < totalShips.Count; i ++)
		{
			BehaviourController behaviourController;
			behaviourController = totalShips[i].GetComponent<BehaviourController>();

			if(behaviourController.currentAttackOrFlee == BehaviourController.Attack_Flee.Attacking)
			{
				if(!attackingShips.Contains(behaviourController))
				{
					attackingShips.Add(behaviourController);
				}
			}
		}

		for(int i = 0; i < attackingShips.Count; i ++)
		{
			if(attackingShips[i].currentAttackOrFlee != BehaviourController.Attack_Flee.Attacking || attackingShips[i] == null)
			{
				attackingShips.Remove(attackingShips[i]);
			}
		}
	}

	void RemoveTotalShips()
	{
		for(int i = 0; i < totalShips.Count; i ++)
		{
			if(totalShips[i] == null)
			{
				totalShips.Remove(totalShips[i]);
			}
		}
	}

	void RemoveEmpireShips()
	{
		for(int i = 0; i < empireShips.Count; i ++)
		{
			if(empireShips[i] == null)
			{
				shipExplodeCount ++;
				empireShips.Remove(empireShips[i]);
			}
		}
	}

	void RemoveRebelShips()
	{
		for(int i = 0; i < rebelShips.Count; i ++)
		{
			if(rebelShips[i] == null)
			{
				shipExplodeCount ++;
				rebelShips.Remove(rebelShips[i]);
			}
		}
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		TotalShips();
		AttackingShips();

		RemoveRebelShips();
		RemoveEmpireShips();
		RemoveTotalShips();

		if(currentScene == scene.Scene1)
		{
			if(Vector3.Distance(this.transform.position, this.GetComponent<CameraController>().lookAtTarget.transform.position) > distanceTrigger)
			{
				currentScene = scene.Scene2;
			}
		}

		if(currentScene == scene.Scene2)
		{
			shipCameraTimer -= 1 * Time.fixedDeltaTime;

			if(shipCameraTimer <= 0)
			{
				if(count == 0)
				{
					GameObject standingBySoundEffectInstance;
					standingBySoundEffectInstance = Instantiate(standingBySoundEffect, transform.position, Quaternion.identity, this.transform) as GameObject;
				}

				count ++;

				int randomShip;
				randomShip = Random.Range(0, ships.Length);

				this.transform.parent = ships[randomShip].transform;

				CameraPositions randomShipCamerPositions;
				randomShipCamerPositions = ships[randomShip].GetComponent<CameraPositions>();

				this.transform.position = randomShipCamerPositions.camerPositions[camerController.shipCameraIndex].transform.position;
				this.transform.rotation = randomShipCamerPositions.camerPositions[camerController.shipCameraIndex].transform.rotation;

				camerController.camerType = CameraController.type.ShipCam;
				camerController.shipCameraPositions = randomShipCamerPositions;

				shipCameraTimer = startShipCameraTimer;
			}

			if(count >= countAmount)
			{
				currentScene = scene.Scene3;
			}
		}

		if(currentScene == scene.Scene3)
		{
			this.transform.parent = SquadronPosition.transform;
			camerController.camerType = CameraController.type.TargetCam;

			for(int i = 0; i < ships.Length; i ++)
			{
				Health shipHealth;
				shipHealth = ships[i].GetComponent<Health>();
	
				if(shipHealth.typeOfShip == Health.shipType.X_Wing)
				{ 
					Animator shipAnimator;
					shipAnimator = ships[i].GetComponent<Animator>();
					
					shipAnimator.SetBool("AttackPosition", true);

				}
			}
				
			timeTillNextScene -= 1 * Time.fixedDeltaTime;

			if(timeTillNextScene <= 0)
			{
				currentScene = scene.Scene4;
			}
		}

		if(currentScene == scene.Scene4)
		{
			int randShip = 0;
			if(randShip == 0)
			{
				randShip = Random.Range(0, ships.Length);
			}
			CameraPositions shipCamPositions; 
			shipCamPositions = ships[randShip].GetComponent<CameraPositions>();

			this.transform.position = shipCamPositions.camerPositions[0].transform.position;
			this.transform.rotation = shipCamPositions.camerPositions[0].transform.rotation;
			this.transform.parent = ships[randShip].transform;

			camerController.shipCameraPositions = shipCamPositions;
			camerController.camerType = CameraController.type.ShipCam;

			Vector3 temp;
			temp = deathStar.transform.localScale;
			temp = new Vector3(temp.x + deathStarScale[0].x, temp.y + deathStarScale[0].y, temp.z + deathStarScale[0].z);
			deathStar.transform.localScale = temp;
			currentScene = scene.Scene5;
		}

		if(currentScene == scene.Scene5)
		{
			timeUntilNextScene -= 1 * Time.fixedDeltaTime;

			if(timeUntilNextScene <= 0)
			{
				GameObject theSizeOfThatThingInstance;
				theSizeOfThatThingSFX = Instantiate(theSizeOfThatThingSFX, this.transform.position, Quaternion.identity, this.transform) as GameObject;

				currentScene = scene.Scene6;
			}
		}

		if(currentScene == scene.Scene6)
		{
			timeUnityNextScene_6 -= 1 * Time.fixedDeltaTime;

			if(timeUnityNextScene_6 <= 0)
			{
				for(int i = 0; i < ships.Length; i ++)
				{
					Boid b = ships[i].GetComponent<Boid>();
					b.maxSpeed = 1500;
					b.maxForce = 1500;
				}

				currentScene = scene.Scene7;
			}
		}

		if(currentScene == scene.Scene7)
		{
			leader.GetComponent<Seeker>().targetGameObject = divePosition;

			this.transform.parent = null;
			this.transform.position = divePosition.transform.position;
			camerController.camerType = CameraController.type.TargetCam;

			timeUntilNextScene_7 -= 1 * Time.fixedDeltaTime;

			if(timeUntilNextScene_7 <= 0)
			{
				currentScene = scene.Scene8;
			}
		}

		if(currentScene == scene.Scene8)
		{
			for(int i = 0; i < ships.Length; i ++)
			{
				Boid b = ships[i].GetComponent<Boid>();

				b.maxForce = 500;
				b.maxSpeed = 500f;
			}
				
			deathStarSurface.SetActive(true);
			deathStar.SetActive(false);
			leader.GetComponent<Seeker>().targetGameObject = deathStarSurfaceTarget;

			int randomShip = 0;

			randomShip = Random.Range(0, ships.Length);

			CameraPositions shipCamPositions = ships[randomShip].GetComponent<CameraPositions>();
			camerController.shipCameraPositions = shipCamPositions;

			this.transform.position = shipCamPositions.camerPositions[0].transform.position;
			this.transform.rotation = shipCamPositions.camerPositions[0].transform.rotation;

			this.transform.parent = ships[randomShip].transform;

			camerController.camerType = CameraController.type.ShipCam;

			currentScene = scene.Scene9;
		}

		if(currentScene == scene.Scene9)
		{
			for(int i = 0; i < ships.Length; i ++)
			{
				BehaviourController behaviourController;
				behaviourController = ships[i].GetComponent<BehaviourController>();
				behaviourController.currentBehaviour = BehaviourController.Behaviour.Wander;

				Boid b = ships[i].GetComponent<Boid>();

				b.damping = 15f;
				b.maxForce = 1500f;
				b.maxSpeed = 1500f;
			}

			timeUntilNextScene_9 -= 1 * Time.fixedDeltaTime;

			if(timeUntilNextScene_9 <= 0)
			{
				GameObject enemiesIncomingSFXInstance;
				enemiesIncomingSFXInstance = Instantiate(EnemiesIncomingSFX, this.transform.position, Quaternion.identity, this.transform) as GameObject;
				currentScene = scene.Scene10;
			}
		}

		if(currentScene == scene.Scene10)
		{
			tieFighterSquadron.SetActive(true);

			this.transform.position = divePosition.transform.position;

			this.transform.parent = null;

			camerController.camerType = CameraController.type.TargetCam;
			camerController.lookAtTarget = tieFighterLeader.gameObject.transform;

			timeUntilNextScene_10 -= 1 * Time.fixedDeltaTime;

			if(timeUntilNextScene_10 <= 0)
			{
				currentScene = scene.Scene11;
			}
		}

		if(currentScene == scene.Scene11)
		{
			for(int i = 0; i < tieFighters.Length; i ++)
			{
				BehaviourController behaviourController = tieFighters[i].GetComponent<BehaviourController>();

				behaviourController.currentBehaviour = BehaviourController.Behaviour.Wander;

				currentScene = scene.Scene12;
			}
		}

		if(currentScene == scene.Scene12)
		{
			if(attackingShips.Count == 0)
			{
				noCombatTimer -= 1 * Time.deltaTime;
			}

			if(noCombatTimer <= 0)
			{
				for(int i = 0; i < totalShips.Count; i ++)
				{
					BehaviourController bController;
					Seeker seeker;

					bController = totalShips[i].GetComponent<BehaviourController>();
					seeker = totalShips[i].GetComponent<Seeker>();

					bController.currentAttackOrFlee = BehaviourController.Attack_Flee.Attacking;

					int randomEnemy;

					if(totalShips[i].gameObject.tag == "Rebel")
					{
						randomEnemy = Random.Range(0, empireShips.Count);

						seeker.targetGameObject = empireShips[randomEnemy].gameObject;
					}

					if(totalShips[i].gameObject.tag == "Empire")
					{
						randomEnemy = Random.Range(0, rebelShips.Count);

						seeker.targetGameObject = rebelShips[randomEnemy].gameObject;
					}
				}

				noCombatTimer = startNoCombatTimer;
			}

			battle = true;

			battleCameraSwitchTime -= 1 * Time.deltaTime;

			if(attackingShips == null)
			{
				Debug.Log("No Attacking Ships");
				return;
			}

			if(battleCameraSwitchTime <= 0 && attackingShips.Count != null)
			{
				int randomShip = Random.Range(0, attackingShips.Count);

				CameraPositions cameraPositions;
				cameraPositions = attackingShips[randomShip].GetComponent<CameraPositions>();

				camerController.shipCameraPositions = cameraPositions;
				camerController.camerType = CameraController.type.ShipCam;

				this.transform.position = cameraPositions.camerPositions[0].transform.position;
				this.transform.rotation = cameraPositions.camerPositions[0].transform.rotation;

				this.transform.parent = attackingShips[randomShip].transform;

				battleCameraSwitchTime = startBattleCameraSwitchTime;
			}
		}

		if(shipExplodeCount >= maxShipExplodeCount && currentScene == scene.Scene12)
		{
			if(chosenShip1 == null)
			{
				for(int i = 0; i < rebelShips.Count; i ++)
				{
					if(rebelShips[i].GetComponent<Health>().typeOfShip == Health.shipType.X_Wing)
					{
						chosenShip1 = rebelShips[i].gameObject;
					}
				}
			}

			BehaviourController behaviourController;
			behaviourController = chosenShip1.GetComponent<BehaviourController>();

			behaviourController.currentAttackOrFlee = BehaviourController.Attack_Flee.Trench_Run;

			CameraPositions cameraPositions;
			cameraPositions = chosenShip1.GetComponent<CameraPositions>();

			camerController.shipCameraPositions = cameraPositions;
			camerController.camerType = CameraController.type.ShipCam;

			this.transform.position = cameraPositions.camerPositions[0].transform.position;
			this.transform.rotation = cameraPositions.camerPositions[0].transform.rotation;

			this.transform.parent = chosenShip1.transform;

			PathFollowing path;

			path = behaviourController.GetComponent<PathFollowing>();

			path.path = new GameObject[trenchRunWayPoints.Length];

			for(int i = 0; i < trenchRunWayPoints.Length; i ++)
			{
				path.path[i] = trenchRunWayPoints[i];
			}

			Boid b;
			b = chosenShip1.GetComponent<Boid>();

			b.maxSpeed = 15000f;
			b.maxForce = 15000f;

			currentScene = scene.Scene13;
		}

		if(currentScene == scene.Scene13 && chosenShip1 == null)
		{
			currentScene = scene.Scene12;
		}

		if(currentScene == scene.Scene13)
		{
			chosenShip1.GetComponent<BehaviourController>().currentAttackOrFlee = BehaviourController.Attack_Flee.Trench_Run;

			this.transform.position = camerController.shipCameraPositions.camerPositions[0].transform.position;
			this.transform.rotation = camerController.shipCameraPositions.camerPositions[0].transform.rotation;

			if(deathStarExplodeTrigger == null)
			{
				currentScene = scene.Scene14;
			}
		}

		if(currentScene == scene.Scene14)
		{
			this.transform.parent = null;
			camerController.camerType = CameraController.type.TargetCam;
			camerController.lookAtTarget = chosenShip1.transform;

			BehaviourController bController;
			bController = chosenShip1.GetComponent<BehaviourController>();
			bController.currentAttackOrFlee = BehaviourController.Attack_Flee.Attacking;
			bController.currentBehaviour = BehaviourController.Behaviour.Seek;
			bController.seeker.targetGameObject = divePosition.gameObject;

			currentScene = scene.Scene15;
		}

		if(currentScene == scene.Scene15)
		{
			for(int i = 0; i < rebelShips.Count; i ++)
			{
				BehaviourController bController;
				bController = rebelShips[i].GetComponent<BehaviourController>();

				bController.currentAttackOrFlee = BehaviourController.Attack_Flee.Attacking;
				bController.currentBehaviour = BehaviourController.Behaviour.Seek;

				Boid b;
				b = rebelShips[i].GetComponent<Boid>();

				b.maxForce = 15000f;
				b.maxSpeed = 15000f;

				bController.seeker.targetGameObject = homePlanet;
			}

			timeUntilNextScene_15 -= 1 * Time.deltaTime;

			if(timeUntilNextScene_15 <= 0)
			{
				currentScene = scene.Scene16;
			}
		}

		if(currentScene == scene.Scene16)
		{
			this.transform.position = finalCameraPosition.transform.position;

			int randomShip;
			randomShip = Random.Range(0, rebelShips.Count);

			camerController.lookAtTarget = rebelShips[randomShip].transform;

			currentScene = scene.Scene17;
		}

		if(currentScene == scene.Scene17)
		{
			deathStarSurface.SetActive(false);
			deathStar.SetActive(true);

			deathStar.transform.localScale = deathStarStartScale;

			Vector3 temp;
			temp = new Vector3(deathStar.transform.position.x, -17000f, deathStar.transform.position.z);

			deathStar.transform.position = temp;

			timeUntilNextScene_17 -= 1 * Time.deltaTime;

			if(timeUntilNextScene_17 <= 0)
			{
				currentScene = scene.Scene18;
			}
		}

		if(currentScene == scene.Scene18)
		{
			deathStarExplosionTimer -= 1 * Time.deltaTime;

			camerController.lookAtTarget = deathStar.transform;

			if(deathStarExplosionTimer <= 0)
			{
				Health deathStarHealth;
				deathStarHealth = deathStar.GetComponent<Health>();
				deathStarHealth.Dead();
			}
		}
	}
}
