using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour 
{

	public GameObject battleConsole;
	public bool active;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.LeftShift))
		{
			active = !active;
		}

		battleConsole.SetActive(active);
	}
}
