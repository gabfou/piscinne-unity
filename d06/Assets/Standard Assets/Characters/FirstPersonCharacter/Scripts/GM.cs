using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour {

	// Use this for initialization
	public AudioClip musicalm;
	public AudioClip musicpascalm;
	public AudioClip alarm;
	AudioSource audio;
	bool _calmitude = false;
	int		max = 0;
	public bool calmitude{get {
			return _calmitude;
//			return calmitude;
		} set{
			_calmitude = value;
			if (_calmitude == true) {
				GetComponent<AudioSource> ().clip = musicpascalm;
				GetComponent<AudioSource> ().Play ();
				GetComponents<AudioSource> ()[1].PlayOneShot (alarm);
			} else {
				GetComponent<AudioSource> ().clip = musicalm;
				GetComponent<AudioSource> ().Play ();
				GetComponents<AudioSource> () [1].Stop ();
			}
		}
	}


	void Start () {
		calmitude = false;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
