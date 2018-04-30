using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadranManager : MonoBehaviour 
{

	public GameObject[] shipsInSquad;
	public GameObject[] squadPositions;

	public GameObject emptyPosition;

	// Use this for initialization
	void Start () 
	{
		foreach(GameObject ship in shipsInSquad)
		{
			GameObject emptyPositionInstance;

			emptyPositionInstance = Instantiate(emptyPosition, ship.transform.position, Quaternion.identity, this.transform) as GameObject;

			ship.GetComponent<Seeker>().targetGameObject = emptyPositionInstance.gameObject;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
