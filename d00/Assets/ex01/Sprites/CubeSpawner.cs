using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour {
	public GameObject obj;
	float timesinceprevious;
	float spawntime;
	// Use this for initialization
	void Start () {
		timesinceprevious = 0;
		spawntime = Random.Range (0.3f, 3); 
	}
	
	// Update is called once per frame
	void Update () {
		timesinceprevious += Time.deltaTime;
		if (timesinceprevious > spawntime)
		{
			spawntime = Random.Range (0.3f, 3);
			timesinceprevious = 0;
			GameObject.Instantiate (obj, transform.position, Quaternion.identity);
		}
		
	}
}
