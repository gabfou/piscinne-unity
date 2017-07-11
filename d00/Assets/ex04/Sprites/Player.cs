using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public KeyCode up;
	public KeyCode down;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (up))
		{
			if (transform.localPosition.y < 4.5)
				transform.localPosition += new Vector3 (0, 0.3F, 0);
		}
		if (Input.GetKey (down)) {
			if (transform.localPosition.y > -4.5)
				transform.localPosition += new Vector3 (0, -0.3F, 0);
		}
	}
}
