using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour {

	public float seeface;
	public float seebehind;
	public float angleview;
	public float vitesse;
	Vector2 dir;
	public weapon w;
	public bool agro;
	Rigidbody2D rigid;
	LayerMask l;
	public Vector2 lastsee;
	public List<Vector2> checkpoint;
	public List<Vector2> path;
	bool onsearch;
	public	bool	stun;
	int tour = 1;
	int listindex = 0;
	BernardManager bernard;
	public checkpoint check;
	bool search = false;
	checkpoint nnn;
	bool		lol = true;

	public List<checkpoint> list;
	// Use this for initialization
	void Start () {
		search = false;
		angleview = angleview / 180f;
		agro = false;
		stun = false;
		l = ~((1 << 9) | (1 << 2) | (1 << 10));
		rigid = GetComponent<Rigidbody2D>();
		onsearch = false;
		checkpoint.Add (transform.position);
		bernard = FindObjectOfType<BernardManager> ();
	}

	void move(Vector2 dir)
	{
		rigid.velocity = dir * vitesse;
//		transform.position = (Vector2)transform.position + ((Vector2)dir * vitesse * Time.deltaTime);
		Vector2 dirspe = dir / (Mathf.Abs(dir.x) + Mathf.Abs(dir.y));;
		transform.eulerAngles = new Vector3 (0, 0, ((dirspe.y + 1) * 90 * ((dirspe.x > 0) ? 1 : -1)));
	}

	void OnTriggerStay2D(Collider2D c)
	{

		if (c.tag == "Player") {
			Vector2 tmp2 = Camera.main.transform.position;
			Vector2 tmp = ((Vector2)tmp2 - (Vector2)transform.position).normalized;
			RaycastHit2D hit = Physics2D.Raycast ((Vector2)transform.position, tmp,Mathf.Infinity, l);
			if (hit.collider && hit.collider.tag == "Player") {
				agro = true;
				onsearch = false;
				lastsee = tmp2;
			}
		} else if (c.tag == "checkpoint"){
			check = c.gameObject.GetComponent<checkpoint> ();
			if (lol)
				{
					nnn = c.gameObject.GetComponent<checkpoint> ();
		lol = false;
				}
	}
	}


	IEnumerator oucquilet()
	{
//		searching ();
		yield return new WaitForSeconds (5f);
	}

	// Update is called once per frame
	void Update () {
		if (!stun) {
			if (agro) {
				Vector2 tmp2 = Camera.main.transform.position;
				Vector2 tmp = ((Vector2)tmp2 - (Vector2)transform.position).normalized;
				if (onsearch == false)
					lastsee = tmp2;
				onsearch = true;

				RaycastHit2D hit = Physics2D.Raycast ((Vector2)transform.position + dir * 2, tmp, Mathf.Infinity, l);
				if (hit.collider && hit.collider.tag == "Player") {
					search = false;
					move (tmp);
					lastsee = tmp2;
					//debug.log ("enemy tire");
					if (w.ammo == 0)
						w.ammo++;
					w.tryshot2 (tmp2, tmp/*, ~((1 << 9))*/);
				} else {
					hit = Physics2D.Raycast ((Vector2)transform.position + dir * 2, tmp, Mathf.Infinity, l);
					if (hit.collider && hit.collider.tag == "Player")
					{
						agro = true;
						return ;
					}
					if (search == false) {
						path = check.pathto (bernard.coordonees, lastsee);
						search = true;
						listindex = 0;
					}
					Vector2 tmp3 = (lastsee - (Vector2)transform.position).normalized;
					supermove (true);
//					if (Vector2.Distance (nnn.transform.position, transform.position) < 0.2)
//						path = check.pathto (nnn);
					if (Vector2.Distance (lastsee, transform.position) < 0.2) {
						agro = false;
						//path = check.pathto (nnn);
						//path.Add (checkpoint [0]);
//						listindex = 0;
//						StartCoroutine (oucquilet ());
					}
				}
			} else {
//				supermove ();
				onsearch = false;
			}
		}
	}


	void    supermove(bool destroy = false) {
		List<Vector2> tmp = (destroy) ? path : checkpoint;
		if (listindex > tmp.Count - 1)
		{
			listindex = 0;
			return ;
		}
		move ((tmp [listindex] - (Vector2)transform.position).normalized);
		if (Vector2.Distance (transform.position, tmp [listindex]) < 0.2f) {
			listindex += 1;
		}
//		if (listindex == tmp.Count && destroy) {
//			path.Add (lastsee);
//		}
		if (listindex > tmp.Count - 1)
			listindex = 0;
//		else if (listindex == tmp.Count) {
//			if (agro)
//				move (((Vector2)Camera.main.gameObject.transform.position - (Vector2)transform.position).normalized);
//			else
//				move ((checkpoint[0] - (Vector2)transform.position).normalized);
//		}
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.collider.tag == "bullet") {
			dead();
			return;
		} else if (col.collider.tag == "Weapon") {
			if (col.gameObject.GetComponent<weapon> ().faitbobo == true) {
				//debug.log (col.gameObject.GetComponent<weapon> ().faitbobo);
				dead ();
				return;
			} else {
				//debug.log ("cuicui");
				stun = true;
				StartCoroutine(stuned ());
			}
		}	
	}

	IEnumerator	stuned() {
		yield return new WaitForSeconds (2f);
		stun = false;
	}

	void	dead() {
		GameObject.Destroy (gameObject);
	}
}
