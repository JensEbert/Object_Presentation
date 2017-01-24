using UnityEngine;
using System.Collections;

public class ButtonLookAtCamera : MonoBehaviour 
{
	public GameObject go_Camera;

	// Update is called once per frame
	void Update () 
	{
		transform.rotation = Quaternion.LookRotation (transform.position - go_Camera.transform.position);
	}
}
