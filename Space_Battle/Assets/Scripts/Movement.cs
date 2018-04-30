using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour 
{
	public float speed;

	Rigidbody myRB;

	// Use this for initialization
	void Start () 
	{
		myRB = this.GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		myRB.velocity = transform.forward * speed * Time.fixedDeltaTime;
	}
}
