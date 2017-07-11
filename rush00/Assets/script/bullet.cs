using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

	public bool follow;
	public float vitesse;
	Vector2 dir;
	Vector2 cible;
	float lifetime;
	LayerMask l;
	Rigidbody2D rigid;
	public	Vector2	v;
	public	Vector3 StartPosition;
	public	float	range;
//	public GameObject cible2;
	// Use this for initialization
	void Start () {
		cible = Camera.main.transform.position;
		StartPosition = transform.localPosition;
		lifetime = 0;
		rigid = GetComponent<Rigidbody2D>();
		changecible (v);
		Destroy (gameObject, 10);
	}

	public void changecible (Vector2 c)
	{
		rigid = GetComponent<Rigidbody2D>();
		cible = c;
		dir = (c - (Vector2)transform.position).normalized;
		Vector2 dirspe =  dir / (Mathf.Abs(dir.x) + Mathf.Abs(dir.y));
		transform.eulerAngles= new Vector3 (0, 0, ((dirspe.y + 1) * 90 * ((dirspe.x > 0) ? 1 : -1)) + 90);
		//rigid.velocity = dir * vitesse * 10000;

//		Debug.Log (dir.y);
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.tag != "bullet")
			GameObject.Destroy(gameObject);
	}

	void OnCollisionStay2D (Collision2D col)
	{
		if (col.gameObject.tag != "bullet")
			GameObject.Destroy(gameObject);
	}

	// Update is called once per frame
	void Update () {
//		lifetime += Time.deltaTime;

//		if (lifetime > 10)
//			GameObject.Destroy(gameObject);
		rigid.velocity = dir * vitesse;
		if (follow)
			dir = ((Vector2)(Camera.main.transform.position - transform.position)).normalized;
//		Debug.Log (dir.y);
//		transform.localPosition = (Vector2)transform.localPosition + (Vector2)((Vector2)dir * vitesse);
		if (transform.localPosition.y < StartPosition.y + (-range) || transform.localPosition.x < StartPosition.x + (-range) || transform.localPosition.y > StartPosition.y + range || transform.localPosition.x > StartPosition.x + range)
			GameObject.Destroy(gameObject);

	}
}
