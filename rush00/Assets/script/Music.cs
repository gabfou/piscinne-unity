using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {

	public	AudioSource	backgroudsound;

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
		backgroudsound.Play ();
	}
	// Use this for initialization
	void Start () {
//		backgroudsound.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
