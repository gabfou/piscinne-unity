using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fliker : MonoBehaviour {

	float time = 0;
	Light l;
	bool b = false;
	// Use this for initialization
	void Start () {
		l = GetComponent<Light> ();
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time > 0.1) {
			if (b) {
				l.range = 0;
				b = false;
			} else {
				l.range = 6;
				b = true;
			}
			time = 0;
		}
	}
}
