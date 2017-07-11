using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bumper : MonoBehaviour {

	public Sprite normal;
	public Sprite bumps;
	public float y;
	public float x;
	SpriteRenderer rend;
	AudioSource audio;

	// Use this for initialization
	void Start () {
		rend = GetComponent<SpriteRenderer> ();
		audio = GetComponent<AudioSource> ();
	}

	public IEnumerator bump()
	{
		audio.Play ();
		gameObject.tag = "Untagged";
		rend.sprite = bumps;
		yield return new WaitForSeconds (0.3F);
		gameObject.tag = "bumper";
		rend.sprite = normal;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
