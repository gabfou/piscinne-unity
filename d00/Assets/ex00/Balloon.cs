using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour {
	int niark;
	float lifetime;
	int niark2;
	int niark3;
	int niark4;
	// Use this for initialization
	void Start () {
		niark = 0;
		lifetime = 0;
		niark2 = 0;
		niark3 = 0;
		niark4 = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		lifetime += Time.deltaTime;
		if (niark4 == 1)
			return;
		if (Input.GetKeyDown ("space") && niark3 == 0)
		{
			niark = 0;
			niark4 = 0;
			transform.localScale += new Vector3 (0.6F, 0.6F, 0);
			niark2 += 23;
		}
		else
		{
			if (niark2 > 0)
				niark2--;
			if (transform.localScale.x > 1 && transform.localScale.y > 1)
			{
				niark = 0;
				transform.localScale += new Vector3 (-0.05F, -0.05F, 0);
			}
			else
				niark++;
		}
		if (niark > 160)
		{
			niark4 = 1;
			Debug.Log("Balloon life time: " + Mathf.RoundToInt(lifetime) + "s");
		}
		if (transform.localScale.x > 7)
		{
			GameObject.Destroy (gameObject);
			Debug.Log("Balloon life time: " + Mathf.RoundToInt(lifetime) + "s");
		}
		if (niark2 > 1000)
		{
			niark3 = 180;
			niark2 = 0;
		}
		if (niark3 > 0) {
			niark3--;
		}
	}
}
