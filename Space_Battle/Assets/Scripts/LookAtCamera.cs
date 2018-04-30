using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LookAtCamera : MonoBehaviour 
{

	public GameObject mainCamera;
	public float smoothSpeed;

	public float distance;

	// Use this for initialization
	void Start () 
	{
		mainCamera = GameObject.FindWithTag("MainCamera");
	}

	void SetActiveFromDistance()
	{
		if(Vector3.Distance(this.transform.position, mainCamera.transform.position) <= distance)
		{
			this.gameObject.GetComponent<Canvas>().enabled = true;
		}
		else
		{
			this.gameObject.GetComponent<Canvas>().enabled = false;
		}
	}

	// Update is called once per frame
	void Update () 
	{
		SetActiveFromDistance();

		Vector3 lookDirection;
		lookDirection = mainCamera.transform.position - this.transform.position;
		lookDirection.y = 0;
		transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lookDirection), smoothSpeed * Time.fixedDeltaTime);
	}
}
