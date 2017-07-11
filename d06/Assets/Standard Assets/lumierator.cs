using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class lumierator : MonoBehaviour {

	// Use this for initialization
	public Vector3[] poslist;
	int i = 0;
	float deplacex;
	float deplacey;
	float deplacez;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
//		if (Vector3.Distance (transform.eulerAngles, poslist [i]) > 0.1) {
//			i++;
//			if (i > poslist.Length - 1)
//				i = 0;
//			deplacex = poslist [i].x - transform.eulerAngles.x;
//			deplacey = poslist [i].y - transform.eulerAngles.y;
//			deplacez = poslist [i].z - transform.eulerAngles.z;
//		}
//		transform.transform.eulerAngles += new Vector3 (deplacex, deplacey, deplacez);
	}

	void OnTriggerStay(Collider c)
	{
		if (c.tag == "Player") {
			if (c.gameObject.GetComponent<FirstPersonController> ().protect == false)
				c.gameObject.GetComponent<FirstPersonController> ().lumiere += 3;
			else
				c.gameObject.GetComponent<FirstPersonController> ().lumiere += 0.5f;
		}
	}
}
