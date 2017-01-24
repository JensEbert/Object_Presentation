using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour 
{
	public GameObject go_Camera;
	
	// Update is called once per frame
	void Update () 
	{
		transform.LookAt (go_Camera.transform.position);
	}
}
