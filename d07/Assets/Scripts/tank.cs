using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tank : MonoBehaviour {
	public int life;
	public canon c;
	public roue r;
	public commun c2;
	public int nbmissile;
	CharacterController ch;
	public bool nul;
	public Text ammo;
	public Text HP;
	public Image cursor;
	// Use this for initialization
	void Start () {
		ch = GetComponent<CharacterController>();
		
	}
	
	// Update is called once per frame
	void Update () {

		// partie canon

		Camera.main.transform.position = c.transform.position;
		Camera.main.transform.rotation = c.transform.rotation;
		Camera.main.transform.position -= c.transform.forward * 5;
		Camera.main.transform.position += c.transform.up * 2;
		if (Input.mousePosition.x - (Screen.width / 2) > 10)
			c.transform.eulerAngles += new Vector3 (0, (Input.mousePosition.x - (Screen.width / 2)) * 0.01f, 0);
		else if (Input.mousePosition.x - (Screen.width / 2) < -10)
			c.transform.eulerAngles += new Vector3 (0, (Input.mousePosition.x - (Screen.width / 2)) * 0.01f, 0);
		if (!nul && Input.mousePosition.y - (Screen.height / 2) > 10 && (c.transform.eulerAngles.x < 40 || c.transform.eulerAngles.x > 330))
			c.transform.eulerAngles -= new Vector3 ((Input.mousePosition.y - (Screen.height / 2)) * 0.01f, 0, 0);
		else if (!nul && Input.mousePosition.y - (Screen.height / 2) < -10 && (c.transform.eulerAngles.x > 320 || c.transform.eulerAngles.x < 30))
			c.transform.eulerAngles -= new Vector3 ((Input.mousePosition.y - (Screen.height / 2)) * 0.01f, 0, 0);

		if (Input.GetMouseButton (0))
			c.mitraillette ();
		if ((Input.GetMouseButtonDown (1) || Input.GetKeyDown ("z")) && nbmissile > 0) {
			c.missile ();
			nbmissile--;
		}


		// roue

		float x = Input.GetAxis("Vertical") * r.speed;
		float y = Input.GetAxis("Horizontal") * 4;

		if (Input.GetKeyDown ("left shift") && r.canboost)
			StartCoroutine (r.boost ());

		ch.SimpleMove(transform.forward * x);
		transform.Rotate(0, y, 0);
		ammo.text = "ammo = " + nbmissile;
		HP.text = "HP = " + c2.life;
	}
}
