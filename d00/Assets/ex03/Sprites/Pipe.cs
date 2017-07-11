using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour {
	public GameObject bird;
	float pseudorand;
	float vitesse;
	int score;
	float lifetime;
	int finito;
	int niark;

	// Use this for initialization
	void Start () {
		pseudorand = 0;
		vitesse = 0.15F;
		score = 0;
		lifetime = 0;
		finito = 0;
		niark = 0;
	}

	public float gpseudorand()
	{
		return pseudorand;
	}

	public Vector3 pos()
	{
		return transform.localPosition;
	}

	// Update is called once per frame
	void Update () {
		if (finito == 1)
			return ;
		if ((bird.transform.localPosition.y < -4.2 || bird.transform.localPosition.y > 4.7)
		    || ((bird.transform.localPosition.y < -1.8 + pseudorand || bird.transform.localPosition.y > 1.3 + pseudorand)
		    && transform.localPosition.x < -4.2 && transform.localPosition.x > -6)) {
			Debug.Log ("Score: " + score + "\nTime: " + Mathf.RoundToInt (lifetime) + "s");
			finito = 1;
			return;
		} else if (transform.localPosition.x < -4.2 && transform.localPosition.x > -6 && niark == 0) {
			score += 5;
			niark = 1;
		}

		lifetime += Time.deltaTime;
		transform.localPosition -= new Vector3 (vitesse, 0, 0);
		vitesse += 0.0006F;

		if (transform.localPosition.x < -10)
		{
			niark = 0;
			pseudorand = Time.deltaTime * (bird.transform.localPosition.y + 10) * 100F;
			while (pseudorand > 3.5F)
				pseudorand -= 3.5F;
			while (pseudorand < -3.5F)
				pseudorand += 3.5F;
			transform.localPosition = new Vector3 (10, pseudorand, -1);
		}
	}
}
