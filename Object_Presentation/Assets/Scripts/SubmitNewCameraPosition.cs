using UnityEngine;
using System.Collections;

public class SubmitNewCameraPosition : MonoBehaviour 
{
	public float f_newCameraX, f_newCameraY, f_newCameraZ;
	private GameObject go_newTarget;
	private GameObject go_newTargetChild;
	public LookAtTarget s_lookAtTargetScript;

	// Use this for initialization
	void Start ()
	{
		go_newTarget = gameObject;
		go_newTargetChild = gameObject.transform.GetChild (0).gameObject;
	}

	//When object is clicked, zoom in on it
	/*void OnMouseDown()
	{
		SetNewTarget ();
	}*/

	public void SetNewTarget ()
	{
		s_lookAtTargetScript.LookAtNewTarget(go_newTarget, go_newTargetChild, f_newCameraX, f_newCameraY, f_newCameraZ);
	}
}
