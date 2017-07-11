using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Sonic : MonoBehaviour
{
	[HideInInspector]public int			rings;
	public float		speedFactor;
	[HideInInspector]public float		speed;
	public float		maxSpeed;
	public float		jumpHeight;
	public float		rollingBoost;

	private Animator	animator;
	private Rigidbody2D	rbody;
	private Vector2 velocity;
	private float vMagnitude;
	private float acceleration;
	private float launchTime;
	[HideInInspector]public bool isGrounded;
	private bool isOnGroundNow;
	[HideInInspector]public bool isCharging;
	[HideInInspector]public bool isRolling;
	[HideInInspector]public bool isJumpball;
	[HideInInspector]public bool isAirborne;
	[HideInInspector]public bool isHit;
	[HideInInspector]public bool isDead;
	[HideInInspector]public bool isInvincible;
	[HideInInspector]public bool isShielded;
	[HideInInspector]public float charge;
	public GameObject currentShield;
	public PhysicsMaterial2D standardMat;
	public PhysicsMaterial2D rollMat;
//	private PhysicsMaterial2D currentMat;
	public GameObject checkpoint;
	public AudioSource aRoll;
	public AudioSource aJump;
	public AudioSource aCharge;
	public AudioSource aDestroy;
	public AudioSource aLoseRings;
	public AudioSource aSpike;
	public AudioSource aDeath;
	public AudioClip aring;
	public AudioClip aend;
	public GM gm;
	GameObject tmpshield;


	bool GROUND(Collider2D collision)
	{
		return (collision.gameObject.tag == "ground" || collision.gameObject.tag == "moving"
			|| collision.gameObject.tag == "ouch" || collision.gameObject.tag == "bumper");
	}

	void Awake()	{
		animator = GetComponent<Animator>();
		rbody = GetComponent<Rigidbody2D>();
//		currentMat = GetComponent<CircleCollider2D>().sharedMaterial;
	}

	void FixedUpdate() 	{
		accelerate();
		checkRoll();
	}

	void Update() {
		if (tmpshield != null)
			tmpshield.transform.position = transform.position;
		if (gm.end > 0)
			return;
		calcAcceleration ();
		checkCharge ();
		lookUpAndDown ();
		checkSpaceButton ();
		checkFalling ();
	}

	void Start() {
		respawn();
	}

	void checkFalling() {
		if (transform.position.y < -15 && isDead == false)
			dead ();
	}

	void calcAcceleration() {
		acceleration = Input.GetAxis("Horizontal") * speedFactor;
		if (isRolling == true) {
			if (isGrounded == false)
				acceleration = acceleration / 2;
			else
				acceleration = 0;
		}
		if (acceleration != 0 && isHit == false && isCharging == false)
			transform.localScale = new Vector3(Mathf.Sign (rbody.velocity.x) * Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
		velocity = rbody.velocity;
		vMagnitude = velocity.magnitude;
		animator.SetFloat("speed", Mathf.Abs(vMagnitude) / 1.5f);
	}

	void accelerate() {
		if (vMagnitude < maxSpeed && isHit == false && isCharging == false)
			rbody.AddForce(new Vector2(acceleration, 0));
	}

	void lookUpAndDown() {
		if (Input.GetAxis("Vertical") < 0) {
			if (vMagnitude == 0)
				animator.SetBool("down", true);
		}
		else
			animator.SetBool("down", false);
	}

	void checkRoll() {
		if (Input.GetAxis("Vertical") < 0 && vMagnitude > 5 && isGrounded == true) {
			isRolling = true;
			GetComponent<CircleCollider2D>().sharedMaterial  = rollMat;
			rbody.drag = 0;
			animator.SetBool ("rolling", true);
			if (!aRoll.isPlaying)
				aRoll.Play();
		}
		if (vMagnitude < 5 && Time.time > launchTime ) {
			isRolling = false;
			GetComponent<CircleCollider2D>().sharedMaterial = standardMat;
			rbody.drag = 2f;
			animator.SetBool ("rolling", false);
		}
	}


	void checkCharge() {
		if ((Input.GetKeyUp("s") || Input.GetKeyUp(KeyCode.DownArrow)) && isCharging == true) {
			animator.SetBool ("rolling", true);
			animator.SetBool ("charge", false);
			aRoll.Play();
			isRolling = true;
			isCharging = false;
			launchTime = Time.time + 1;
			rbody.AddForce(new Vector2(charge * maxSpeed * transform.localScale.x, 0), ForceMode2D.Impulse);
			charge = 0;
		}
	}

	void checkSpaceButton() {
		if (Input.GetKeyDown("space") && isGrounded == true && rbody.velocity.y < 5 && isHit == false) {
			if (vMagnitude == 0 && Input.GetAxis("Vertical") < 0 )
				chargeRoll();
			else
				jump ();
		}
	}

	void jump() {
		isRolling = false;
		isJumpball = true;
		isGrounded = false;
		aJump.Play();
		animator.SetBool("rolling", false);
		animator.SetBool("jumpball", true);
		rbody.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
	}

	void chargeRoll() {
		aCharge.Play ();
		animator.SetBool("charge", true);
		isCharging = true;
		charge += 0.75f;
		if (charge >= 2.25f)
			charge = 2.25f;
	}


	public void rampBoost(float direction, float boost) {
		if (rbody.velocity.x > 2.5f) {
			rbody.AddForce(new Vector2(boost * transform.localScale.x, 1), ForceMode2D.Impulse);
		}
	}

	public void Bumper(float boostX, float boostY) {
		if (boostY  > 0) {
			isJumpball = false;
			isAirborne = true;
			animator.SetBool("jumpball", false);
			animator.SetBool("airborne", true);
		}
		if (boostY != 0)
			rbody.velocity = new Vector2(rbody.velocity.x, 0);
		if (boostX != 0)
			rbody.velocity = new Vector2(0, rbody.velocity.y);
		transform.localScale = new Vector3(Mathf.Sign (rbody.velocity.x) * Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
		rbody.AddForce(new Vector2(boostX * 4, boostY * 4), ForceMode2D.Impulse);
	}

	IEnumerator turnpanneaux(GameObject p)
	{
//		Quaternion tmp = p.transform.localRotation;
		while (true) {
			yield return new WaitForSeconds (0.01F);
			p.transform.Rotate (0, 10, 0);
		}
	}

	IEnumerator supervitesse()
	{
		this.maxSpeed = 30;
		gm.musicgame.pitch = 1.2f;
		yield return new WaitForSeconds (15f);
		this.maxSpeed = 20;
		gm.musicgame.pitch = 1;
	}

	void OnCollisionEnter2D(Collision2D c)
	{
		if (c.gameObject.tag == "tv") {
			this.destroy ();
			if (c.gameObject.GetComponent<tv> ().type == tv.type_tv.shield) {
				tmpshield =  GameObject.Instantiate (currentShield, this.transform.position, Quaternion.identity);
				isShielded = true;
			}
			else if (c.gameObject.GetComponent<tv>().type == tv.type_tv.vitesse)
				StartCoroutine(supervitesse());
			c.gameObject.GetComponent<tv> ().Destroy();
		}
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (GROUND(collision)) {
			isJumpball = false;
			animator.SetBool("jumpball", false);
			isAirborne = false;
			animator.SetBool("airborne", false);
		}
		if (collision.tag == "ring") {
			gm.addring ();
			aDeath.PlayOneShot (aring);
			collision.gameObject.SetActive(false);
		}
		if (collision.gameObject.tag == "panneau") {
			gm.end = 1;
			StartCoroutine (turnpanneaux (collision.gameObject));
			aDeath.PlayOneShot (aend);
		}

		if (collision.gameObject.tag == "bumper") {
			bumper tmp = collision.gameObject.GetComponent<bumper> ();
//			tmp.x++;
			Bumper (tmp.x, tmp.y);
			StartCoroutine(tmp.bump());

		}
	}

	void OnTriggerStay2D(Collider2D collision) {
		if (GROUND(collision)) {
			isGrounded = true;
			isOnGroundNow = true;
			if (isHit == false)
				animator.SetBool("getHit", false);
			if (collision.gameObject.tag == "ouch") {
				getHit ();
			}
		}
		if (collision.gameObject.tag == "moving")
			transform.parent = collision.transform;
	}

	void OnTriggerExit2D(Collider2D collision) {
		if (collision.gameObject.tag == "ground") {
			isOnGroundNow = false;
			Invoke("deGround", 0.4f);
		}

		if (collision.gameObject.tag == "moving") {
			isOnGroundNow = false;
			Invoke("deGround", 0.4f);
			transform.parent = null;
		}
	}

	void deGround() {
		if (isOnGroundNow == false)
			isGrounded = false;
	}

	public void destroy() {
		rbody.AddForce(new Vector2(0, 12), ForceMode2D.Impulse);
		aDestroy.Play ();
	}

	IEnumerator Kaibutnotkai()
	{
		isInvincible = true;
		SpriteRenderer tmp = GetComponent<SpriteRenderer> ();
		tmp.color = new Color (1, 1, 1, 0.5F);
		yield return new WaitForSeconds (0.5F);
		tmp.color = new Color (1, 1, 1, 1F);
		yield return new WaitForSeconds (0.5F);
		tmp.color = new Color (1, 1, 1, 0.5F);
		yield return new WaitForSeconds (0.5F);
		tmp.color = new Color (1, 1, 1, 1F);
		yield return new WaitForSeconds (0.5F);
		tmp.color = new Color (1, 1, 1, 0.5F);
		yield return new WaitForSeconds (0.5F);
		tmp.color = new Color (1, 1, 1, 1F);
		yield return new WaitForSeconds (0.5F);
		tmp.color = new Color (1, 1, 1, 0.5F);
		yield return new WaitForSeconds (0.5F);
		tmp.color = new Color (1, 1, 1, 1F);
		yield return new WaitForSeconds (0.5F);
		tmp.color = new Color (1, 1, 1, 0.5F);
		yield return new WaitForSeconds (0.5F);
		tmp.color = new Color (1, 1, 1, 1F);
		yield return new WaitForSeconds (0.5F);

		isInvincible = false;
	}

	public void getHit() {
		if (isInvincible) {
			return;
		}
		if (gm.ringnb < 1 && isShielded == false)
			dead ();
		else
		{
			Vector2 newv = new Vector2 (-5 * Mathf.Sign (rbody.velocity.x), 5);
			rbody.velocity = newv;
			isHit = true;
			StartCoroutine(stopHit ());
			animator.SetBool("getHit", true);
			StartCoroutine(Kaibutnotkai());
			if (isShielded) {
				isShielded = false;
				GameObject.Destroy (tmpshield);
			} else {
				aLoseRings.Play ();
				gm.instantring (transform.position);
			}

		}
	}

	IEnumerator stopHit() {
		yield return new WaitForSeconds (2);
		isHit = false;
	}

	void dead(){
		Debug.Log ( PlayerPrefs.GetInt ("TotalDeath"));
		PlayerPrefs.SetInt ("TotalDeath", PlayerPrefs.GetInt ("TotalDeath") + 1);
		Debug.Log ( PlayerPrefs.GetInt ("TotalDeath"));
		aDeath.Play ();
		animator.SetBool("dead", true);
		isDead = true;
		rbody.AddForce (new Vector2(0, 15), ForceMode2D.Impulse);
		GetComponent<CircleCollider2D>().enabled = false;
		Invoke("newLife", 2);
	}

	void newLife() {
		isHit = false;
		isDead = false;
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

	void respawn() {
		rbody.velocity = Vector2.zero;
		transform.position = checkpoint.transform.position;
	}

	IEnumerator invincible() {
		isInvincible = true;
		SpriteRenderer sr = GetComponent<SpriteRenderer>();
		for (int i = 0; i < 15; i++) {
			sr.color = Color.clear;
			yield return new WaitForSeconds(0.1f);
			sr.color = Color.white;
			yield return new WaitForSeconds(0.1f);
		}
		isInvincible = false;
	}
}
