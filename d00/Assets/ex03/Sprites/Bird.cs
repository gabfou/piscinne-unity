using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {
	public Pipe pipe;
	int pastp;

	// Use this for initialization
	void Start () {
		pastp = 0;		
	}
	
	// Update is called once per frame
	void Update () {
		if ((transform.localPosition.y < -4.21 || transform.localPosition.y > 4.7)
			|| ((transform.localPosition.y < -1.8 + pipe.gpseudorand () || transform.localPosition.y > 1.3 + pipe.gpseudorand ())
		    && pipe.transform.localPosition.x < -4.2 && pipe.transform.localPosition.x > -6)) {
			return;
		}
		if (Input.GetKeyDown ("space"))
		{
			pastp = 7;
		}
		if (pastp > 0) {
			pastp--;
			transform.localPosition += new Vector3 (0, 0.3F, 0);
		}
		else
		transform.localPosition += new Vector3 (0, -0.09F, 0);
	}
}
