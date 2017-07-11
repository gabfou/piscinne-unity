using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class tankmechent : MonoBehaviour {

	public int life;
	public canon c;
	public roue r;
	public int nbmissile;
	CharacterController ch;
	public bool nul;
	GameObject cible;
	float timingshoot;
	float timingmissile;
	// Use this for initialization
	void Start () {
		ch = GetComponent<CharacterController>();

	}

	void OnTriggerStay(Collider c)
	{
		if (c.tag == "tank" && (cible == null || Vector3.Distance(transform.position, cible.transform.position) > Vector3.Distance(transform.position, c.transform.position)))
			cible = c.gameObject;
	}

	// Update is called once per frame
	void Update () {
		if (cible != null) {
			Vector3 v = transform.eulerAngles;
			Vector3 targetDir = (cible.transform.position - transform.position).normalized;
			// partie canon
			Vector3 newDir = Vector3.RotateTowards (c.transform.forward, targetDir, 0.01f, 0F);
			c.transform.rotation = Quaternion.LookRotation (newDir);
			v.y = c.transform.eulerAngles.y;
			c.transform.eulerAngles = v;

			if (Vector3.Distance (transform.position, cible.transform.position) < 150 && timingshoot < 0.2) {
				c.mitraillette ();
				if (timingshoot < 0)
					timingshoot = 1;
			}
//			if (Vector3.Distance (transform.position, cible.transform.position) < 200 && nbmissile > 0 &&  timingmissile < 0 && Physics.Raycast(transform.position, cible.transform.position)){
//				c.missile ();
//				nbmissile--;
//				timingmissile = 5;
//			}
			timingshoot -= Time.deltaTime;
			timingmissile -= Time.deltaTime;

			// roue
			GetComponent<NavMeshAgent> ().destination = cible.transform.position;
			if (Vector3.Distance (transform.position, cible.transform.position) < 20)
				GetComponent<NavMeshAgent> ().destination = transform.position;

		}
	}
}
