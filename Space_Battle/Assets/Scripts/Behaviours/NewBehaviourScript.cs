using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehvaiourScript : MonoBehaviour {

	List<SteeringBehaviour> behaviours = new List<SteeringBehaviour>();
	#region Global V3: force, velocity, acceleration. FT: mass, maxSpeed
	public Vector3 force = Vector3.zero;
	public Vector3 velocity = Vector3.zero;
	public Vector3 acceleration = Vector3.zero;
	public float mass = 1;
	public float maxSpeed = 5.0f;
	#endregion

	void Start () {
		SteeringBehaviour[] behaviours = GetComponents<SteeringBehaviour>();
		foreach(SteeringBehaviour b in behaviours)
		{
			this.behaviours.Add(b);
		}
	}

	void Update () {
		force = Calculate();

		Banking();
	}
	Vector3 Calculate()
	{
		force = Vector3.zero;

		foreach (SteeringBehaviour b in behaviours)
		{
			if (b.isActiveAndEnabled)
			{
				force += b.Calculate() * b.weight;
			}
		}
		return force;
	}

	#region Banking
	void Banking()
	{
		AccelerationSmoothing();
		ApplyAccelerationToVelocity();
		ApplyLerpAndLookAt();
	}
	void AccelerationSmoothing()
	{
		Vector3 tempAcceleration = force / mass;
		float smoothRate = Mathf.Clamp(9.0f * Time.deltaTime, 0.15f, 0.4f) / 2.0f;
		acceleration = Vector3.Lerp(acceleration, tempAcceleration, smoothRate);
	}
	void ApplyAccelerationToVelocity()
	{
		velocity += acceleration * Time.deltaTime;
		velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
	}
	void ApplyLerpAndLookAt()
	{
		Vector3 globalUp = new Vector3(0, 0.2f, 0);
		Vector3 accelUp = acceleration * 0.05f;
		Vector3 bankUp = accelUp + globalUp;
		float smoothRate = Time.deltaTime * 3.0f;
		Vector3 tempUp = transform.up;
		tempUp = Vector3.Lerp(tempUp, bankUp, smoothRate);

		if(velocity.magnitude > 0.0001f)
		{
			transform.LookAt(transform.position + velocity, tempUp);
			velocity *= 0.99f;
		}
		transform.position += velocity * Time.deltaTime;
	}
	#endregion
}