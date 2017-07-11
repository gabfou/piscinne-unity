using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Club : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	public void changepos(Vector3 v)
	{
		transform.localPosition = v;
	}

	public void changerotat(Quaternion v)
	{
		transform.localRotation = v;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
