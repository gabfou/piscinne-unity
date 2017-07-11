using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unit : MonoBehaviour {

	// Use this for initialization
	public Vector3 cible;
	public bool control;
	Vector3 dir;
	public float speed;
	Animator animator;
	public AudioClip ok;
	int life;
	GameObject cibleu;
	bool ascibleu;
	public AudioClip die;
	public AudioClip ouchs;
	float timesincelastbam;
	float timesincesdead;
	public int toutdroit;


	void Start () {
		toutdroit = 0;
		cible = transform.position;
		animator = GetComponent<Animator>();
		dir = new Vector3 (0, 0, 0);
		life = 12;
		ascibleu = false;
		timesincelastbam =  0;
		timesincesdead = 0;
		if (control)
			Camera.main.GetComponent<controler> ().gentil.Add (gameObject);
		else
			Camera.main.GetComponent<controler> ().mechant.Add (gameObject);
	}

	public GameObject getCibleu(){return cibleu;}

	public void ouch(int i)
	{
		life -= i;
		//GetComponent<AudioSource> ().PlayOneShot (ouchs);
	}
	void OnMouseOver()
	{
		if (Input.GetMouseButtonDown (0)) {
			Camera.main.GetComponent<controler> ().changecontrol (this);
		}
	}

	void bam()
	{
		if (cibleu == null) {
			supcibleu ();
			return;
		}
		if (cibleu.GetComponent<unit> () != null) {
			cibleu.GetComponent<unit> ().ouch (4);
		}
		if (cibleu.GetComponent<building> () != null) {
			if (cibleu.tag == "mechanth") {
				foreach (GameObject tmp in Camera.main.GetComponent<controler> ().mechant) {
					if (tmp.GetComponent<unit> () != null && tmp.GetComponent<unit> ().getCibleu () != null && Vector2.Distance (tmp.GetComponent<unit> ().getCibleu ().transform.position, transform.position) > 4) {
						tmp.GetComponent<unit> ().newcibleu (gameObject);
						tmp.GetComponent<unit> ().toutdroit = 1;
					}
				}
			}
			cibleu.GetComponent<building> ().ouch (4);
		}
		if (cibleu == null) {
			supcibleu ();
			return;
		}
	}

	public void newcibleu(GameObject c)
	{
		cibleu = c;
		ascibleu = true;
		timesincelastbam =  0;
//		animator.SetBool("ascibleu") = true;
	}

	public void supcibleu()
	{
		ascibleu = false;
//		animator.SetBool("ascibleu") = false;
	}

	public void sayok()
	{
		GetComponent<AudioSource> ().PlayOneShot (ok);
	}

	public void triggerdepl()
	{
		dir = Vector3.Normalize (cible - transform.position);
		animator.SetFloat ("x", dir.x);
		animator.SetFloat ("y", dir.y);
	}

	// Update is called once per frame
	void Update () {
		float dist = Vector3.Distance (transform.localPosition, cible);

		if (Camera.main.GetComponent<controler> ().end)
			return ;
		if (ascibleu && cibleu == null) {
			supcibleu ();
			return;
		}
		if (control == false && ascibleu == false && Camera.main.GetComponent<controler> ().gentil.Count != 0) {
			newcibleu (Camera.main.GetComponent<controler> ().gentil [0]);
			toutdroit = 0;
		}
		else if (toutdroit == 0 && control == false &&  Vector2.Distance(transform.position, cibleu.transform.position) > 4) {
			foreach (GameObject tmp in Camera.main.GetComponent<controler> ().gentil) {
				if (Vector2.Distance(transform.position, tmp.transform.position) < 4)
					newcibleu (tmp);
			}
		}
		if (life < 1) {
			if (timesincesdead == 0) {
				GetComponent<AudioSource> ().PlayOneShot (die);
				animator.SetBool ("die", true);

			}
			timesincesdead += Time.deltaTime;
			if (timesincesdead < 1)
				return;
			GameObject.Destroy (gameObject);
		}

		if (ascibleu) {
			cible = cibleu.transform.position;
			triggerdepl ();
		}

		if (ascibleu && Vector3.Distance (transform.position, cible) < 0.5) {
			animator.SetBool ("attack", true);
			timesincelastbam += Time.deltaTime;
			if (timesincelastbam > 1) {
				timesincelastbam = 0;
				bam ();
			}
			return;
		}
		else
			animator.SetBool ("attack", false);
		if (dist > 0.2F) {
			triggerdepl();
			transform.Translate (dir * speed / 100);
		}
		else
		{
			animator.SetFloat ("x", 0);
			animator.SetFloat ("y", 0);
		}
	}
}
