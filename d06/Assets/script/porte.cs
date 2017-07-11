using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class porte : MonoBehaviour {

	// Use this for initialization
	public bool canopen;
	public GameObject e;
	public AudioClip open;
	public AudioClip close;
	public AudioClip deblock;
	public AudioClip denied;
	AudioSource audio;

	Animator a;

	void Start () {
		a = e.GetComponent<Animator> ();
		audio = gameObject.GetComponent<AudioSource>();
	}

	void OnTriggerEnter(Collider c)
	{
		if (c.tag == "Player" && canopen) {
			audio.PlayOneShot (open);
			a.SetBool ("ouvert", true);
		}
		else if (c.tag == "Player")
			audio.PlayOneShot (denied);
	}

	void OnTriggerExit(Collider c)
	{
		if (c.tag == "Player" && canopen) {
			audio.PlayOneShot (close);
			a.SetBool ("ouvert", false);
		}
	}

	// Update is called once per frame
	void Update () {
		
	}


}
