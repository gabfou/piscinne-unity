using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {
	public KeyCode key;
	float v;
	// Use this for initialization
	void Start () {
		v = Random.Range (0.5F, 0.2F);
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.localPosition -= new Vector3(0F, v, 0F);
		if (Input.GetKeyDown(key) && transform.localPosition.y > -5.2F && transform.localPosition.y < -2.8F)
		{
			Debug.Log("Precision: " + (transform.localPosition.y + 4));
			GameObject.Destroy(gameObject);

		}
		if (transform.localPosition.y < -100)
			GameObject.Destroy(gameObject);
	}
}
