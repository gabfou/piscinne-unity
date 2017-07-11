using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spinText : MonoBehaviour {

	public Text textRotate;
	private Transform _transform;
	public float speed;

	// Use this for initialization
	void Start () {
		_transform = textRotate.transform;
	}

	// Update is called once per frame
	void Update () {
		_transform.Rotate (0, 0, speed);
	}
}
