using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mechants_Controlleur : MonoBehaviour {


	public		Maya_Controlleur	gentil;
	public		bool				aggro;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (aggro) {
//			navmeshAgent.SetDestination (gentil.position);
		}
	}

}
