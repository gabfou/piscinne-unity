using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_Handler : MonoBehaviour {

	public	Transform	target;
	private	Vector3 	cameraOffset;

	// Use this for initialization
	void Start () {
		cameraOffset = transform.position - target.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = target.position + cameraOffset;
	}
}
