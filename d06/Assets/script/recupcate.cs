using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class recupcate : MonoBehaviour {

	public string carte;
	public porte p;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider c)
	{
		if (c.tag == "Player") {
			if (c.gameObject.GetComponent<FirstPersonController> ().listkey.Contains (carte)) {
				c.gameObject.GetComponent<FirstPersonController> ().listkey.Remove (carte);
				p.canopen = true;
				GetComponent<AudioSource> ().Play();
			}
		}
	}
}
