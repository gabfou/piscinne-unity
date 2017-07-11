using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret : MonoBehaviour {
	public GameObject bullet;
	float timesinceprevious;
	public float spawntime;
	LayerMask l;
	// Use this for initialization
	void Start () {
		l = ~((1 << 8) | (1 << 9) | (1 << 10));
	}
	
	// Update is called once per frame
	void Update () {
		timesinceprevious += Time.deltaTime;

		if (timesinceprevious > spawntime)
		{
			RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.Normalize(Camera.main.transform.position - transform.position), 100, l);
			if (hit.collider != null && hit.collider.tag == "Player") {
				Debug.Log ("Fdsf");
				GameObject.Instantiate (bullet, transform.position, Quaternion.identity);
			}
			timesinceprevious = 0;
		}
	}
}
