using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class carte : MonoBehaviour {

	public string name;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter(Collider c)
	{
		if (c.tag == "Player") {
			c.gameObject.GetComponent<FirstPersonController> ().listkey.Add (name);
			GetComponent<AudioSource> ().Play ();
			gameObject.transform.position = new Vector3 (0, -300, 0);
//			gameObject.SetActive (false);
		}
	}
}
