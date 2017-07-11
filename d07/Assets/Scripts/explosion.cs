using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour {

	float t;
	// Use this for initialization
	void Start () {
		
	}

	void OnTriggerEnter(Collider c)
	{
		if (c.gameObject.tag == "tank" && Vector3.Distance(c.gameObject.transform.position, transform.position) < 3)
			c.gameObject.GetComponent<commun>().life -= 50;
	}

	// Update is called once per frame
	void Update () {
		t += Time.deltaTime;
		if (t > 0.5)
			GameObject.Destroy (gameObject);
	}
}
