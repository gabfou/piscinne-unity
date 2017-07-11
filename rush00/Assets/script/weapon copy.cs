//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class weapon : MonoBehaviour {
//	float timesinceprevious;
//	public bullet bullet;
//	public float rateoffire;
//	// Use this for initialization
//	void Start () {
//		timesinceprevious = 0;
//	}
//
//	public void tryshot(Vector2 v, Vector2 dir)
//	{
//		timesinceprevious += Time.deltaTime;
//		if (timesinceprevious > rateoffire) {
//			bullet b = bullet.Instantiate (bullet, (Vector2)transform.position + dir, Quaternion.identity);
//			if (bullet != null)
//				b.changecible (v);
//			timesinceprevious = 0;
//		}
//	}
//	// Update is called once per frame
//	void Update () {
//		timesinceprevious += Time.deltaTime;
//	}
//}
