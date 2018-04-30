using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadronManager : MonoBehaviour 
{

	public GameObject[] shipsInSquad;

	// Use this for initialization
	void Start () 
	{
		for(int i = 0; i < shipsInSquad.Length; i ++)
		{
			GameObject shipPosition;

			shipPosition = new GameObject(shipsInSquad[i].name + "_Position");

			Instantiate(shipPosition, shipsInSquad[i].transform.position, Quaternion.identity, this.transform);

			shipsInSquad[i].GetComponent<Seeker>().targetGameObject = shipPosition.gameObject;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
