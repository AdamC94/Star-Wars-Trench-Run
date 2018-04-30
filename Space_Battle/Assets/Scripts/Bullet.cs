using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
	public float damage;
	public string shipName;
	public string shipTag;
	public int shipLayer;

	// Use this for initialization
	void Start () 
	{
		//Physics.IgnoreLayerCollision(this.gameObject.layer, shipLayer);
	}

	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject)
		{
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
