using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour {
	public bullet	bullet;
	public float	rateoffire;
	public Sprite	inhand;
	public	AudioSource	reload;
	public	AudioSource	shootsound;
	public	AudioSource	dryfire;
	public	string	pseudo;
	public	int		ammo;
	public	bool	canshoot;
	public	bool	faitbobo;
	public	bool	truc;
	public	Rigidbody2D	rigid;
	public	Collider2D	collid;

	// Use this for initialization
	void Start () {
		truc = false;
		rigid = gameObject.GetComponent<Rigidbody2D> ();
		collid = gameObject.GetComponent<Collider2D> ();
		canshoot = true;
		gameObject.layer = 9;
	}

	void alertpoto()
	{
		Collider2D[] c  = Physics2D.OverlapCircleAll(transform.position, 10, (1 << 9));
		int i = -1;
		while (++i < c.Length) {
			IA tmp = c [i].gameObject.GetComponent <IA> ();
			if (tmp)
				tmp.agro = true;
		}
	}

	public void tryshot2(Vector2 v, Vector2 dir)
	{
		Debug.Log("ammo :" + ammo + " canshoot" + canshoot);
		if (ammo == 0)
			dryfire.Play ();
		if (canshoot == true && (ammo > 0 || ammo == -1)) {
			bullet b = bullet.Instantiate (bullet, (Vector2)transform.position, Quaternion.identity);
			shootsound.Play ();
			alertpoto ();
			b.v = v;
			b.gameObject.layer = gameObject.layer;
			if (ammo != -1)
				ammo--;
			StartCoroutine (Cooldown ());
		}
	}
	// Update is called once per frame
	void Update () {
		if (rigid && (rigid.velocity.x > 0.5f || rigid.velocity.y > 0.5f)) {
			collid.isTrigger = false;
			gameObject.layer = 8;
			truc = true;
		} else {	
			if (collid && truc == true) {
				collid.isTrigger = true;
				gameObject.layer = 9;
				truc = false;
			}
		}
	}

	public	void	Setactive(bool actifoupas) {
		gameObject.GetComponent<SpriteRenderer> ().enabled = actifoupas;
	}

	IEnumerator		Cooldown() {
		canshoot = false;
		yield return new WaitForSeconds(rateoffire);
		canshoot = true;
	}
		
}
