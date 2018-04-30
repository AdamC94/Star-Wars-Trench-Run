using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour 
{
	public GameObject ship;

	public int index;

	public enum type{FreeCam, ShipCam, TargetCam};
	public type camerType;

	Camera myCam;

	public float movementSpeed;
	public float speed;
	private float yaw;
	private float pitch;

	public CameraPositions shipCameraPositions;
	public int shipCameraIndex;

	public Transform lookAtTarget;
	public float smoothSpeed;

	// Use this for initialization
	void Start () 
	{
		myCam = this.GetComponent<Camera>();
	}

	void CameraTargetLook()
	{
		Vector3 lookDirection;
		lookDirection = lookAtTarget.position - this.transform.position;
		transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lookDirection), smoothSpeed * Time.fixedDeltaTime);
	}

	void CameraMouseLook()
	{
		yaw += speed * Input.GetAxis("Mouse X");
		pitch -= speed * Input.GetAxis("Mouse Y");

		transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
	}

	void CameraFreeMovement()
	{
		Vector3 pos = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);

		if(Input.GetKey(KeyCode.W))
		{
			//forward
			pos += transform.forward * movementSpeed * Time.deltaTime;
		}

		if(Input.GetKey(KeyCode.S))
		{
			//backwards
			pos -= transform.forward * movementSpeed * Time.deltaTime;
		}

		if(Input.GetKey(KeyCode.A))
		{
			//left
			pos -= transform.right * movementSpeed * Time.deltaTime;
		}

		if(Input.GetKey(KeyCode.D))
		{
			//right
			pos += transform.right * movementSpeed * Time.deltaTime;
		}

		if(Input.GetKey(KeyCode.E))
		{
			//up
			pos += transform.up * movementSpeed * Time.deltaTime;
		}

		if(Input.GetKey(KeyCode.Q))
		{
			//down
			pos -= transform.up * movementSpeed * Time.deltaTime;
		}

		transform.position = pos;
	}

	void CameraChangeShipCam()
	{
		if(Input.GetKeyDown(KeyCode.Keypad1))
		{
			transform.position = shipCameraPositions.camerPositions[0].position;
			transform.rotation = shipCameraPositions.camerPositions[0].rotation;
			shipCameraIndex = 0;
		}
		if(Input.GetKeyDown(KeyCode.Keypad2))
		{
			transform.position = shipCameraPositions.camerPositions[1].position;
			transform.rotation = shipCameraPositions.camerPositions[1].rotation;
			shipCameraIndex = 1;
		}
		if(Input.GetKeyDown(KeyCode.Keypad3))
		{
			transform.position = shipCameraPositions.camerPositions[2].position;
			transform.rotation = shipCameraPositions.camerPositions[2].rotation;
			shipCameraIndex = 2;
		}
		if(Input.GetKeyDown(KeyCode.Keypad4))
		{
			transform.position = shipCameraPositions.camerPositions[3].position;
			transform.rotation = shipCameraPositions.camerPositions[3].rotation;
			shipCameraIndex = 3;
		}

		//toggles between all cameras
		if(Input.GetKeyDown(KeyCode.Keypad0))
		{
			shipCameraIndex ++;

			if(shipCameraIndex >= shipCameraPositions.camerPositions.Length)
			{
				shipCameraIndex = 0;
			}

			transform.position = shipCameraPositions.camerPositions[shipCameraIndex].position;
			transform.rotation = shipCameraPositions.camerPositions[shipCameraIndex].rotation;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if(Input.GetKeyDown(KeyCode.F))
		{
			camerType = type.FreeCam;
		}

		if(camerType == type.ShipCam)
		{
			CameraChangeShipCam();
		}

		if(camerType == type.FreeCam)
		{
			CameraMouseLook();
			CameraFreeMovement();
		}

		if(camerType == type.TargetCam)
		{
			CameraTargetLook();
		}
	}
}
