using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class prote : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	void Update () {
	}

	void OnTriggerEnter(Collider c)
	{
		if (c.tag == "Player")
			c.gameObject.GetComponent<FirstPersonController> ().protect = true;
	}

	void OnTriggerExit(Collider c)
	{
		if (c.tag == "Player")
			c.gameObject.GetComponent<FirstPersonController> ().protect = false;
	}
}

