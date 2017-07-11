using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongBall : MonoBehaviour {
	Vector3 direction;
	int playerapoint;
	int playerbpoint;
	public GameObject p1;
	public GameObject p2;
	float vitesse;

	// Use this for initialization
	void Start () {
		engage ();
		playerapoint = 0;
		playerbpoint = 0;
		vitesse = 0.1F;
	}

	void engage()
	{
		float x = (Random.Range(-2, 2) > 0F) ? 1F : -1F;
		float y = (Random.Range(-2, 2) > 0F) ? 1F : -1F;

		transform.localPosition = new Vector3 (0, 0, 0);

		direction = new Vector3 (x, y, 0);
	}

	// Update is called once per frame
	void OnCollision2D (Collision2D col)
	{
		direction.x = -direction.x;
	}
	void Update ()
	{
		vitesse += 0.0001F;
		if (transform.localPosition.y > 4.8 || transform.localPosition.y < -4.8)
		{
			direction.y = -direction.y;
		}
		if (transform.localPosition.x > 10)
		{
			playerapoint++;
			Debug.Log("Player 1: " + playerapoint + " | Player 2: " + playerbpoint);
			engage ();
		}
		else if (transform.localPosition.x < -10)
		{
			playerbpoint++;
			Debug.Log("Player 1: " + playerapoint + " | Player 2: " + playerbpoint);
			engage ();
		}
		if ((transform.localPosition.x < -9.3F && transform.localPosition.x > -9.7F
				&& p1.transform.localPosition.y > transform.localPosition.y - 1.7 && p1.transform.localPosition.y < transform.localPosition.y + 1.7)
			|| (transform.localPosition.x > 9.3F && transform.localPosition.x < 9.7F
				&& p2.transform.localPosition.y > transform.localPosition.y - 1.7 && p2.transform.localPosition.y < transform.localPosition.y + 1.7))
		{
			direction.x = -direction.x;
		}

		transform.localPosition += direction * vitesse;
	}
}
