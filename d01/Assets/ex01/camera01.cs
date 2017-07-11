using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class camera01 : MonoBehaviour {
	public playerScript_ex01 Claire, John, thomas;

	int playernb;
	bool victory;
	// Use this for initialization
	void Start ()
	{
		playernb = -1;
		victory = false;
	}

	void reinit()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}

	playerScript_ex01 pnb(int i){
		if (i == 1)
			return Claire;
		if (i == 2)
			return John;
		if (i == 3)
			return thomas;
		return null;
	}

	void checkvictory()
	{
		if (Claire.victory && John.victory && thomas.victory) {
			if (SceneManager.GetActiveScene().name == "ex01")
				SceneManager.LoadScene("ex02");
			else if (SceneManager.GetActiveScene().name == "ex02")
				SceneManager.LoadScene("ex03");
			else if (SceneManager.GetActiveScene().name == "ex03")
				SceneManager.LoadScene("ex04");
			else if (SceneManager.GetActiveScene().name == "ex04")
				SceneManager.LoadScene("ex05");
			else
				victory = true;
		}
		if (victory == true) {
			Debug.Log ("victoire!");
		}
	}

	// Update is called once per frame
	void Update ()
	{
		playerScript_ex01 current;

		if (Input.GetKeyDown ("r"))
			reinit();
		if (victory)
			return;
		if (Input.GetKeyDown ("1"))
			playernb = 1;
		if (Input.GetKeyDown ("2"))
			playernb = 2;
		if (Input.GetKeyDown ("3"))
			playernb = 3;
		if (playernb == -1)
			return;
		current = pnb(playernb);
		Vector2 vel = current.GetComponent<Rigidbody2D> ().velocity;
		transform.localPosition = current.transform.localPosition - new Vector3 (0, 0, 10);
		if (Input.GetKeyDown("space") && current.Isgrounded())
		{
			vel.y = current.sautesse;//rigidbody.velocity.y = 10;
		}
		if (Input.GetKey ("right") && current.GetComponent<Rigidbody2D>().velocity.x < current.vitesse) {
			vel.x += 0.5F;
		}
		if (Input.GetKey ("left") && current.GetComponent<Rigidbody2D>().velocity.x > -current.vitesse) {
			vel.x -= 0.5F;
		}
		current.GetComponent<Rigidbody2D> ().velocity = vel;
		checkvictory ();
	}
}
