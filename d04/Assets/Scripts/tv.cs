using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tv : MonoBehaviour {

	public Sprite normal;
	public Sprite bzz;
	public Sprite Dead;
	public enum type_tv {piece, vitesse, shield};
	public type_tv type;
	float timesinceprevious;
	bool b;
	SpriteRenderer rend;
	AudioSource audio;
	public ringobj ringobject;
	bool d;


	// Use this for initialization
	void Start () {
		d = false;
		timesinceprevious = 0;
		b = false;
		rend = GetComponent<SpriteRenderer> ();
		audio = GetComponent<AudioSource> ();
	}

	public void Destroy ()
	{
//		audio.Play();
		d = true;
		if (type == type_tv.piece) {
			int ringnb = 10;
			while (--ringnb > 0)
				GameObject.Instantiate (ringobject, transform.position, Quaternion.identity);
		}
		BoxCollider2D.Destroy(GetComponent<BoxCollider2D> ());
		rend.sprite = Dead;
	}

	// Update is called once per frame
	void Update () {
		if (d)
			return;
		timesinceprevious += Time.deltaTime;
		if (timesinceprevious > 1) {
			timesinceprevious = 0;
			if (b == false) {
				b = true;
				rend.sprite = bzz;
			} else {
				b = false;
				rend.sprite = normal;
			}
		}
	}
}
