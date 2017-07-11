using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rotateText : MonoBehaviour {

	public Text textRotate;
	private Transform _transform;
	public float angle;
	public float speed;
	float t;
	bool down;

	// Use this for initialization
	void Start () {
		_transform = textRotate.transform;
		t = 0;
		down = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (down) {
			if (t <= -angle)
				down = false;
			else
				t -= speed;
		} else if (!down) {
			if (t >= angle)
				down = true;
			else
				t += speed;
		}
		_transform.Rotate (0, 0, t);
	}
}
