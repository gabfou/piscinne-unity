using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BernardManager : MonoBehaviour {

	public	float		orientation;
	public	float		speed;
	private	Rigidbody2D	rigid;
	private	Sprite	contactarm;
	public	GameObject	equiped;
	private weapon		Arme;
	[HideInInspector]
	public weapon		Armeequiped;
	public	AudioSource	eject;
	public	AudioSource	diesound;
	public	Text		nameDisplay;
	public	Text		ammoDisplay;
	public	Text		typeDisplay;
	public	bool		death;
	public	checkpoint	coordonees;
	public bool			victory;
	public GameObject gameOverPannel;

	// Use this for initialization
	void Start () {
		death = false;
		rigid = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {
		if (!death && !victory) {
			TurnAround ();
			Move();
			Action ();
			Vector3 v;
			v = transform.position;
			v.z = -10;
			Camera.main.transform.position = v;
			if (Armeequiped != null) {
				Armeequiped.transform.position = transform.position;
				nameDisplay.GetComponent<Text> ().text = Armeequiped.pseudo.ToString ();
				ammoDisplay.GetComponent<Text> ().text = (Armeequiped.ammo == -1) ? "Inf-" : Armeequiped.ammo.ToString ();
				typeDisplay.GetComponent<Text> ().text = "Single";
			} else {
				nameDisplay.GetComponent<Text> ().text = "No weapon";
				ammoDisplay.GetComponent<Text> ().text = "";
				typeDisplay.GetComponent<Text> ().text = "";
			}
		}
	}

	void	TurnAround() {
		Vector3 tmp = gameObject.transform.position;
		Vector3 tmp2 = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0f));
		tmp2.z = 0;
		tmp.z = 0;
		Vector3 v = (tmp2 - tmp);
		v = v / (Mathf.Abs(v.x) + Mathf.Abs(v.y));

		orientation = ((v.y + 1) * 90 * ((v.x > 0) ? 1 : -1)) + (((v.x > 0) ? 0 : 0)); // terro le ternaire zero // NE PAS EFFACER !!!!!


		transform.eulerAngles= new Vector3 (0, 0, orientation);
	}

	void dead()	{
		StartCoroutine (dying());
	}

	IEnumerator dying() {
		death = true;;
		diesound.Play ();
		yield return new WaitForSeconds (1f);
		gameOverPannel.gameObject.SetActive(true);
	}



	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.collider.tag == "bullet") {
			dead();
			return;
		}
	}

	void OnCollisionStay2D (Collision2D col)
	{
		if (col.collider.tag == "bullet") {
			dead();
			return;
		}
	}

	void	Move() {
		var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		rigid.velocity += new Vector2 (move.x * speed * Time.deltaTime, move.y * speed * Time.deltaTime);
	}

	void	Action() {
		if (Input.GetKeyDown ("e")) {
			if (contactarm != null) {
				equiped.GetComponent<SpriteRenderer> ().sprite = contactarm;
				if (Armeequiped != null) {
					DropWeapon ();
				}
				Debug.Log ("LODSGSGSDL");
				if (Armeequiped != null && Armeequiped.GetInstanceID() != Arme.GetInstanceID())
					Arme.reload.Play ();
				Arme.gameObject.layer = 8;
				Arme.Setactive (false);
				Armeequiped = Arme;
			}
		}

		if (Input.GetKeyDown (KeyCode.Mouse0)) {
			if (Armeequiped != null) {
				Vector2 mouseWP = Camera.main.ScreenToWorldPoint (new Vector2 (Input.mousePosition.x, Input.mousePosition.y));
				Armeequiped.tryshot2 (mouseWP, (mouseWP - new Vector2 (transform.position.x, transform.position.y)).normalized * 0.75f);
			}
		}

		if (Input.GetKeyDown (KeyCode.Mouse1)) {
			if (Armeequiped != null) {
				equiped.GetComponent<SpriteRenderer> ().sprite = null;
				DropWeapon ();
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Weapon") {
			Debug.Log (other.gameObject.name);
			Arme = other.gameObject.GetComponent<weapon> ();
			contactarm = Arme.inhand;
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		if (other.tag == "checkpoint") {
			coordonees = other.gameObject.GetComponent<checkpoint>();
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Weapon")
			contactarm = null;
	}

	void	DropWeapon() {
		Armeequiped.gameObject.layer = 8;
		Armeequiped.transform.position = transform.position;
		Armeequiped.Setactive (true);
		Armeequiped.GetComponent<Rigidbody2D> ().velocity = (Camera.main.ScreenToWorldPoint (new Vector2 (Input.mousePosition.x, Input.mousePosition.y)) - transform.position).normalized * 40f;
//		Armeequiped.amm;
		Armeequiped = null;
		eject.Play ();
	}
}
