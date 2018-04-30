using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour 
{
	public enum armyType{Rebel, Imperial};
	public armyType alegiance;

	public enum shipType{X_Wing, Tie_Fighter, Y_Wing};
	public shipType typeOfShip;

	public float health;
	public float startHealth;
	public GameObject explosionParticleEffect;
	public GameObject damagedParticleEffect;
	public GameObject hitParticleEffect;

	private int explosionSfxIndex;
	public GameObject[] explosionSFX;

	public GameObject explodedShip;
	public float explosionForce;
	public float explosionRadius;

	public string ship;

	// Use this for initialization
	void Start () 
	{
		startHealth = health;
	}

	public void Dead()
	{
		GameObject explosionParticleEffectInstance;
		explosionParticleEffectInstance = Instantiate(explosionParticleEffect, transform.position, Quaternion.identity) as GameObject;

		explosionSfxIndex = Random.Range(0, explosionSFX.Length);
		GameObject explosionSFXInstance;
		explosionSFXInstance = Instantiate(explosionSFX[explosionSfxIndex], transform.position, Quaternion.identity) as GameObject;

		GameObject explodedShipInstance;
		explodedShipInstance = Instantiate(explodedShip, transform.position, transform.rotation) as GameObject;

		foreach(Rigidbody childRB in explodedShipInstance.GetComponentsInChildren<Rigidbody>())
		{
			childRB.AddExplosionForce(explosionForce, this.transform.position, explosionRadius, 3.0f,  ForceMode.Impulse);
		}

		Camera camera;
		camera = GetComponentInChildren<Camera>();

		if(camera == null)
		{
			Debug.Log("I do not have a camera");
		}

		if(camera != null)
		{
			camera.transform.parent = null;
		}

		Destroy(gameObject);
	}

	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Bullet")
		{
			Bullet bullet = other.gameObject.GetComponent<Bullet>();

			if(bullet.shipTag != this.gameObject.tag)
			{
				if(health > bullet.damage)
				{
					ContactPoint contact = other.contacts[0];
					Quaternion rot = Quaternion.FromToRotation(Vector3.back, contact.normal);
					Vector3 pos = contact.point;

					GameObject hitParticleEffectInstance;
					hitParticleEffectInstance = Instantiate(hitParticleEffect, pos, rot) as GameObject;
				}

				health -= bullet.damage;
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(health <= 50)
		{
			damagedParticleEffect.SetActive(true);
		}

		if(health <= 0)
		{
			Dead();
		}
	}
}
