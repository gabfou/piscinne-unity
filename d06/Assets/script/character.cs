using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour {

	// Use this for initialization
	CharacterController c;
	public float speed;

	void Start () {
		c = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
		Camera.main.transform.position = transform.position;
		Camera.main.transform.eulerAngles = transform.eulerAngles;
		var move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		c.Move(new Vector3 (move.x * speed * Time.deltaTime, move.y * speed * Time.deltaTime, move.z * speed * Time.deltaTime));
	}
}
