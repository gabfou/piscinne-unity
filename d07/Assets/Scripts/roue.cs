using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roue : MonoBehaviour {
	public float speed;
	public CharacterController papa;
	public bool canboost = true;
	// Use this for initialization
	void Start () {
	}

	public IEnumerator boost()
	{
		speed += 50;
		canboost = false;
		yield return new WaitForSeconds (1f);
		speed -= 50;
		yield return new WaitForSeconds (3f);
		canboost = true;
	}
	
	// Update is called once per frame
	void Update () {

	}
}
