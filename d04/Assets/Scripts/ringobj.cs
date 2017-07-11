using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ringobj : MonoBehaviour {

	Rigidbody2D rigid;
	CircleCollider2D b;
	float t;
	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody2D>();
		b = GetComponent<CircleCollider2D> ();
		rigid.velocity = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
		t = 0;
	}
	
	// Update is called once per frame
	void Update () {
		t += Time.deltaTime;
//		Rigidbody2D.Destroy(rigid);
		if (b.isTrigger != true && t > 2) {
			Rigidbody2D.Destroy(rigid);
			gameObject.layer = 0;
			b.isTrigger = true;
		}
		if (t > 6)
			GameObject.Destroy (this);
	}
}
