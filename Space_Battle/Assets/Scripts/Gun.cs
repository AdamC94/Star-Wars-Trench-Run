using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour 
{
	public bool player;

	public float damage;

	public Rigidbody bulletPrefab;
	public Transform[] firePoints;
	public GameObject []muzzleFlashs;

	public GameObject fireSoundEffect;

	public float force;

	public bool debug;
	public float debugFirePointRadius;

	public float fireRate;
	public float startFireRate;

	public bool fire;

	public AIVision vision;

	// Use this for initialization
	void Start () 
	{
		vision = this.GetComponentInChildren<AIVision>();
	}

	void SpawnBullet(Transform _firePoint)
	{
		Rigidbody bulletPrefabInstance;
		bulletPrefabInstance = Instantiate(bulletPrefab, _firePoint.position, transform.rotation) as Rigidbody;

		Bullet bullet;
		bullet = bulletPrefabInstance.GetComponent<Bullet>();
		bullet.damage = damage;
		bullet.shipName = this.gameObject.name;
		bullet.shipTag = this.gameObject.tag;
		bullet.shipLayer = this.gameObject.layer;

		Physics.IgnoreCollision(bulletPrefabInstance.GetComponent<Collider>(), this.GetComponent<Collider>());

		GameObject fireSoundEffectInstance;
		fireSoundEffectInstance = Instantiate(fireSoundEffect, _firePoint.position, Quaternion.identity) as GameObject;

		Rigidbody rb = bulletPrefabInstance.GetComponent<Rigidbody>();
		rb.AddForce(_firePoint.forward * force);
	}
	void MuzzleFlash(GameObject _muzzleFlash)
	{
		_muzzleFlash.SetActive(true);
	}

	void Shoot()
	{
		if(fireRate <= 0)
		{
			foreach(GameObject muzzleFlash in muzzleFlashs)
			{
				MuzzleFlash(muzzleFlash);
			}
			foreach(Transform firePoint in firePoints)
			{
				SpawnBullet(firePoint);
			}

			fireRate = startFireRate;
		}
	}

	public void Fire()
	{
		foreach(GameObject muzzleFlash in muzzleFlashs)
		{
			MuzzleFlash(muzzleFlash);
		}
		foreach(Transform firePoint in firePoints)
		{
			SpawnBullet(firePoint);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(vision.canSeePlayer == true)
		{
			fire = true;
			fireRate -= 1 * Time.deltaTime;
		}
		if(vision.canSeePlayer == false)
		{
			fire = false;
			fireRate = 0;
		}

		if(fire == true)
		{
			Shoot();
		}

		if(Input.GetMouseButtonDown(0) && player == true)
		{
			foreach(GameObject muzzleFlash in muzzleFlashs)
			{
				MuzzleFlash(muzzleFlash);
			}

			foreach(Transform firePoint in firePoints)
			{
				SpawnBullet(firePoint);
			}
		}
	}

	void OnDrawGizmos()
	{
		if(debug)
		{
			foreach(Transform firePoint in firePoints)
			{
				Gizmos.color = Color.blue;
				Gizmos.DrawWireSphere(firePoint.position, debugFirePointRadius);
				Debug.DrawRay(firePoint.position, firePoint.forward * debugFirePointRadius, Color.red);
			}
		}
	}
}