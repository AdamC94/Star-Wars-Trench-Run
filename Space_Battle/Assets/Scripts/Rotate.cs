using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour 
{
	public float rotationSpeed;

	// Use this for initialization
	void Start () 
	{
	}

	void RotateObject()
	{
		transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
	}
	
	// Update is called once per frame
	void Update () 
	{
		RotateObject();
	}
}
