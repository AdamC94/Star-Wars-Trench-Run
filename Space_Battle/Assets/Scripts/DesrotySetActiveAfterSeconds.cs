using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesrotySetActiveAfterSeconds : MonoBehaviour 
{
	public enum types {Destroy, SetActive};
	public types type; 

	public float seconds;
	private float startSeconds;

	// Use this for initialization
	void Start () 
	{
		startSeconds = seconds;
	}

	void ObjectDestroy()
	{
		Destroy(gameObject, seconds);
	}

	void ObjectSetActive()
	{
		seconds -= 1 * Time.fixedDeltaTime;
		if(seconds <= 0)
		{
			this.gameObject.SetActive(false);
			seconds = startSeconds;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if(type == types.Destroy)
		{
			ObjectDestroy();
		}
		if(type == types.SetActive)
		{
			ObjectSetActive();
		}	
	}
}
