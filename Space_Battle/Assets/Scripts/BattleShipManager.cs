using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleShipManager : MonoBehaviour 
{

	public float spawnRadius;

	public GameObject tieFighterPrefab;

	CamerSceneManager sceneManager;

	// Use this for initialization
	void Start () 
	{
		sceneManager = GameObject.FindObjectOfType<CamerSceneManager>();
	}

	public void SpawnTieFighter()
	{

		GameObject tieFighterInstance;
		tieFighterInstance = Instantiate(tieFighterPrefab, transform.position, Quaternion.identity) as GameObject;

		sceneManager.empireShips.Add(tieFighterInstance.gameObject);

		foreach(GameObject ship in tieFighterPrefab.transform.GetComponentsInParent<GameObject>())
		{
			sceneManager.totalShips.Add(ship.gameObject);
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(this.transform.position, spawnRadius);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(sceneManager.empireShips.Count == 0 && sceneManager.currentScene == CamerSceneManager.scene.Scene12)
		{
			SpawnTieFighter();
		}
	}
}
