﻿using UnityEngine;
using System.Collections;

public class LookAtTarget : MonoBehaviour 
{
	public GameObject go_target = null;
	public GameObject go_targetChild = null;
	public float f_movementSpeed = 4f;

	//Scroll variables
	private float f_scrollSpeed = 4f;
	private float f_distance;
	private float f_minDistance = 2f;
	private float f_maxDistance = 5f;
	private Vector3 v3_currentPosition;

	//LookAt variables
	public float f_lookAtSpeed = 4.0f;
	public float f_moveToNewPositionSpeed = 4.0f;
	private bool b_targetSwitched = false;
	private bool b_rotatingCamera = false;
	private bool b_userInputEnabled = true;
	private bool b_enableWait = false;
	private bool b_canSwitch = true;
	private Vector3 v3_newPosition;

	// Use this for initialization
	void Start () 
	{
		v3_currentPosition = transform.position;
	}
		
	void Update()
	{
		transform.position = new Vector3(Mathf.Clamp(transform.position.x, -10f, 10f), Mathf.Clamp(transform.position.y, 0.25f, 10f), Mathf.Clamp(transform.position.z, -10f, 10f));
	}

	void LateUpdate ()
	{
		if (go_target != null)
		{
			v3_currentPosition = transform.position;

			//Setting the rotation of the camera depending on which target to focus
			if (b_targetSwitched == true && b_rotatingCamera == false) 
			{
				Vector3 v3_dirFromCameraToTarget = go_target.transform.position - transform.position;
				v3_dirFromCameraToTarget.y = go_target.transform.position.y - transform.position.y;
				Quaternion q_lookRotation = Quaternion.LookRotation (v3_dirFromCameraToTarget);
				transform.rotation = Quaternion.Lerp (transform.rotation, q_lookRotation, f_lookAtSpeed * Time.deltaTime);
				transform.position = Vector3.Lerp (transform.position, v3_newPosition, f_moveToNewPositionSpeed * Time.deltaTime);
				if (b_enableWait == true) 
				{
					StartCoroutine("Wait");
				}
			}

			//Rotating the camera around the y-axis and x-axis
			if (b_userInputEnabled == true) 
			{
				if (Input.GetMouseButton (0)) 
				{
					b_rotatingCamera = true;

					//y-axis
					transform.RotateAround (go_target.transform.position, Vector3.up, Input.GetAxis ("Mouse X") * f_movementSpeed);

					//x-axis
					transform.RotateAround (go_targetChild.transform.position, go_targetChild.transform.TransformDirection (Vector3.right), Input.GetAxis ("Mouse Y") * f_movementSpeed);
				} 
				else 
				{
					b_rotatingCamera = false;
				}

			}
				
			//Zooming the camera in and out
			if (Input.GetAxis ("Mouse ScrollWheel") != 0)
			{
				transform.Translate (0, 0, Input.GetAxis ("Mouse ScrollWheel") * f_scrollSpeed);
				f_distance = Vector3.Distance (transform.position, go_target.transform.position);
				if (f_distance > f_maxDistance || f_distance < f_minDistance)
				{
					transform.position = v3_currentPosition;
				}
			}
		}
	}
		
	public IEnumerator Wait() 
	{
		b_enableWait = false;
		b_userInputEnabled = false;
		b_canSwitch = false;
		yield return new WaitForSeconds (2.0f);
		b_targetSwitched = false;
		b_userInputEnabled = true;
		b_canSwitch = true;
	}

	public void LookAtNewTarget (GameObject target, GameObject targetChild, float x, float y, float z)
	{
		if (b_canSwitch == true) 
		{
			go_target = target;
			go_targetChild = targetChild;
			b_targetSwitched = true;
			b_enableWait = true;
			v3_newPosition = new Vector3 (x, y, z);
		}
	}
}
