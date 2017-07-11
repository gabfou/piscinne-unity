using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stupid : MonoBehaviour {

	public float nawak;
	// Use this for initialization
	Rigidbody rigid;
	void Start () {
		rigid = GetComponent<Rigidbody> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		rigid.velocity += new Vector3 (Random.Range (-nawak, nawak), Random.Range (-nawak, nawak), Random.Range (-nawak, nawak));
			
	}
}
