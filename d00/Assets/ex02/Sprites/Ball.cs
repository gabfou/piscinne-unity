using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
	public Club club;
	float vitesse;
	Vector3 direction;
	float puissance;
	int score;
	// Use this for initialization
	void Start () {
		vitesse = 0;
		direction = new Vector3 (0F, 1F, 0F);
		puissance = 0;
		score = -15;
	}

	void updateclub()
	{
		if (transform.localPosition.y > 3.5F)
		{
			direction = new Vector3 (0F, -1F, 0F);
			club.changepos(new Vector3(0, 1.5F, 0) + transform.localPosition);
			club.changerotat(new Quaternion(0, 180F, 0, 0));
		}
		else
		{
			direction = new Vector3 (0F, 1F, 0F);
			club.changepos(new Vector3(0, 0F, 0) + transform.localPosition);
			club.changerotat(new Quaternion(0, 0, 0, 0));
		}
	}

	// Update is called once per frame
	void Update () {
		if (vitesse < 0)
			vitesse = 0;
		if (vitesse == 0)
			updateclub ();

		transform.localPosition += direction * vitesse;

		if (transform.localPosition.y > 4.8)
			direction = new Vector3 (0F, -1F, 0F);
		if (transform.localPosition.y < -4.8)
			direction = new Vector3 (0F, 1F, 0F);
		if (transform.localPosition.y > 3.4 && transform.localPosition.y < 3.6 && vitesse < 0.06)
		{
			transform.localPosition += new Vector3 (0, 0, 1000);
		}
		if (vitesse > 0)
			vitesse -= 0.001F;
		if (Input.GetKey ("space")) {
			puissance++;
			if (transform.localPosition.y > 3.5F) {
				club.changepos (new Vector3 (0, 1.5F + puissance / 100F, 0) + transform.localPosition);
			} else {
				club.changepos (new Vector3 (0, 0F - puissance / 100F, 0) + transform.localPosition);
			}
		}
		else if (puissance != 0) {
			Debug.Log ("Score: " + score);
			score += 5;
			vitesse = puissance / 100F;
			puissance = 0;
		}
	}
}
