using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missille : MonoBehaviour {

	public float speed;
	public GameObject explosion;
	Rigidbody rigid;
	Vector3 posinit;
	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody> ();
		posinit = transform.position;
	}

	void OnCollisionEnter(Collision c)
	{
		GameObject.Instantiate (explosion, transform.position, Quaternion.identity);
		GameObject.Destroy (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		rigid.velocity = transform.forward * speed;
		if (Vector3.Distance (posinit, transform.position) > 200) {
			GameObject.Instantiate (explosion, transform.position, Quaternion.identity);
			GameObject.Destroy (gameObject);
		}
	}
}
