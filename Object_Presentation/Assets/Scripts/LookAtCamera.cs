using UnityEngine;
using System.Collections;

public class LookAtCamera : MonoBehaviour {

	public GameObject go_Camera;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (go_Camera.transform.position);
	}
}
