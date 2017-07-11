using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript_ex00 : MonoBehaviour {

	Vector3 posinit;
	public bool victory;
	bool isgrounded;
	public float vitesse;
	public float sautesse;
	public Color color;
	LayerMask la;
	private    float    distToGround;
	// Use this for initialization
	void Start () {
		posinit = transform.localPosition;
		distToGround = GetComponent<Collider2D> ().bounds.extents.y;
		la = ~((1 << 8) | (1 << 9) | (1 << 10) | (1 << this.gameObject.layer));
		la = la | (1 << (this.gameObject.layer - 3));
	}

	public void reinit()
	{
		victory = false;
		isgrounded = false;
		transform.localPosition = posinit;
	}
		

	public bool gisgrounded(){return isgrounded;}

	public bool Isgrounded()
	{
		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distToGround + 0.1F, la);
		return  (hit.collider != null);
	}

	void OnTriggerStay2D(Collider2D c)
	{
		if (c.tag == "tp")
			transform.localPosition = ((c.gameObject)).GetComponent<tp>().tpout.transform.localPosition;
		if (c.tag == "levier")
			((c.gameObject)).GetComponent<levier>().activator(this.gameObject);
		else if (Vector2.Distance (c.bounds.extents, this.GetComponent<Collider2D> ().bounds.extents) < 0.1F && Vector2.Distance (c.bounds.center, this.GetComponent<Collider2D> ().bounds.center) < 0.1F) {
			victory = true;
		}
	}

	void OnTriggerExit2D(Collider2D c)
	{
		victory = false;
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.collider.tag == "bullet") {
			reinit ();
			return;
		}
		RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, (distToGround + 0.001F), 0), Vector2.down, 0.1f);
		bool right = col.contacts[0].point.x > transform.localPosition.x;
		bool top = col.contacts[0].point.y > transform.localPosition.y;
		float disth = (right) ? col.contacts [0].point.x - transform.localPosition.x : transform.localPosition.x - col.contacts [0].point.x;
		float distv = (top) ? col.contacts [0].point.y - transform.localPosition.y : transform.localPosition.y - col.contacts [0].point.y;


		if (top == false && disth < distv  && hit.collider != null && disth < this.GetComponent<Collider2D> ().bounds.extents.x + 0.01F)
			isgrounded = true;
		//		if (col.contacts.GetLowerBo > 0.1)
		//			isgrounded = false;
	}

	void OnCollisionStay2D (Collision2D col)
	{
		if (col.collider.tag == "bullet") {
			reinit ();
			return;
		}
		RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, (distToGround + 0.001F), 0), Vector2.down, 0.1f);
		bool right = col.contacts[0].point.x > transform.localPosition.x;
		bool top = col.contacts[0].point.y > transform.localPosition.y;
		float disth = (right) ? col.contacts [0].point.x - transform.localPosition.x : transform.localPosition.x - col.contacts [0].point.x;
		float distv = (top) ? col.contacts [0].point.y - transform.localPosition.y : transform.localPosition.y - col.contacts [0].point.y;

		if (top == false && disth < distv && hit.collider != null && disth < this.GetComponent<Collider2D> ().bounds.extents.x + 0.01F)
			isgrounded = true;
	}

	void OnCollisionExit2D (Collision2D collisionInfo)
	{
		isgrounded = false;
	} 
	// Update is called once per frame
	void Update () {
		
	}
}
