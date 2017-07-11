using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cabouge : MonoBehaviour {

	// Use this for initialization
	public float timer;
	float roundtime;
	public Vector3 dir;
	Vector3 dirinit;
	Vector3 posinit;
	Quaternion rotinit;

	void Start () {
		roundtime = 0;
		posinit = transform.localPosition;
		rotinit = transform.localRotation;
		dirinit = dir;
	}

	public void reinit()
	{
		dir = dirinit;
		transform.localPosition = posinit;
		transform.localRotation = rotinit;
		transform.GetComponent<Rigidbody2D> ().angularVelocity = 0;
		roundtime = 0;
	}

	// Update is called once per frame
	void Update () {
		roundtime += Time.deltaTime;
		if (roundtime > timer) {
			dir = -dir;
			roundtime = 0;
		}
		transform.GetComponent<Rigidbody2D>().velocity = dir * 2F;
	}
}
