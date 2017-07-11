using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Maya_Controlleur : MonoBehaviour {

	NavMeshAgent			navmeshAgent;
//	Rigidbody				rbody;
	Animator				animator;
	RaycastHit				hit;
	public		bool 		dead = false;
	perso		cible;
	bool mouse = false;
	float timeattack = 0;
	public	perso stat;
	public		GameObject	mCompetances;
	public		AudioSource	BGmusic;
	private		bool 		mCopen;
	public		Image 		HealthBar;
	public		Image 		XPBar;
	public		Image 		EnemyBar;
	public		Text 		HPtxt;
	public		Text 		XPtxt;
	public		Text		LVLtxt;
	public		Text		EnemyName;
	public		Text 		GameOver;
	public		Text		mname;
	public		Text 		mSTR;
	public		Text 		mAGI;
	public		Text 		mCON;
	public		Text 		mdamage;
	public		Text 		marmor;
	public		Text 		mexp;
	public		Text 		mcredit;
	public		Text 		mcptpts;
	private		Text		t;
	private		bool		b;
	private		bool		end = false;
	public		int 		cptpts;

	[HideInInspector]
	public Vector3 			position;

	void Awake() {
		GameState.mayaController = this;
	}

	void	Start() {
		BGmusic.Play ();
		navmeshAgent = GetComponent< NavMeshAgent > ();
//		rbody = GetComponent< Rigidbody > ();
		animator = GetComponent< Animator > ();
		stat = GetComponent< perso> ();
		t = GameOver.GetComponent<Text> ();
		mname.text = name;
	}

	void	Update () {
		GameState.mayaPosition = transform.position;
			CheckDeath ();
		if (!dead) {
			Move ();
			UpdateGUI ();
		} else if (dead == true && end == false) {
			end = true;
			StartCoroutine (endgame());
		}
	}

	void	Move() {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit2;
		if (Physics.Raycast (ray, out hit2, Mathf.Infinity))
		{
			if (hit2.collider.tag == "mechant")
				hit2.collider.gameObject.GetComponent<perso> ().brillance ();
			if (Input.GetMouseButtonDown (0) && !mCopen)
			 {
				if (hit2.collider.tag == "mechant"){
					cible = hit2.collider.gameObject.GetComponent<perso>();
					// if (ti)
					// timeattack = 0;
				}
				mouse = true;
				hit = hit2;
			}
		}
		if (Input.GetMouseButtonUp (0)) {
			mouse = false;
		}
		if (Input.GetKeyDown ("c")) {
			if (!mCopen) {
				mCopen = true;
				mCompetances.SetActive (true);

			} else {
				mCopen = false;
				mCompetances.SetActive (false);
			}
		}

		Vector3 wantedPosition;
		if (cible == null)
			wantedPosition = new Vector3 (hit.point.x, hit.point.y, hit.point.z);
		else
			wantedPosition = cible.transform.position;

		navmeshAgent.SetDestination (wantedPosition);

		position = transform.position;

		if (cible != null && Vector3.Distance(transform.position, cible.transform.position) <= 4f)
		{
			timeattack += Time.deltaTime;
			navmeshAgent.SetDestination (position);
			if (timeattack > 0.25) {
				stat.attack(cible);
				if (mouse == false)
				{
					cible = null;
					timeattack = 0;
				}
				else
					timeattack = 0;
			}
		}

//		Debug.Log (navmeshAgent.remainingDistance.ToString());

		animator.SetBool ("attack", cible != null && navmeshAgent.remainingDistance <= 4f);

		animator.SetBool ("run", navmeshAgent.remainingDistance > 0.5);

	}

	void OnDrawGizmos()
	{
		Gizmos.DrawSphere(hit.point, 1f);
		Gizmos.color = Color.blue;
	}

	private	void	CheckDeath() {
		if (stat.HP <= 0 && dead == false) {
			animator.SetTrigger ("dead");
			dead = true;
		}
	}

	void	UpdateGUI() {
		if (stat.xp >= stat.nextxp)
			levelup ();
		float curhp = stat.HP / stat.MaxHP;
		float curxp = stat.xp / stat.nextxp;
		HealthBar.fillAmount = curhp;
		XPBar.fillAmount = curxp;
		HPtxt.text = stat.HP.ToString();
		XPtxt.text = stat.xp.ToString() + " / " + stat.nextxp.ToString();
		LVLtxt.text = "LVL " + stat.level;
		if (cible != null) {
			EnemyBar.fillAmount = cible.HP / cible.MaxHP;
			EnemyName.text = cible.name;
		} else {
			EnemyBar.fillAmount = 0;
			EnemyName.text = "";
		}

		mSTR.text = "STR : " + stat.STR;
		mAGI.text = "AGI : " + stat.AGI;
		mCON.text = "CON : " + stat.CON;
		mdamage.text = "damage = " + stat.minDamage + " / " + stat.maxDamage;
		marmor.text = "armor = " + stat.Armor;
		mexp.text = "exp = " + stat.xp.ToString() + " / " + stat.nextxp.ToString();
		mcredit.text = "credit = " + stat.money;
		mcptpts.text = "(+"+ cptpts.ToString() +")";
	}

	public IEnumerator FadeTextToFullAlpha(float t, Text i)
	{
		i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
		while (i.color.a < 1.0f)
		{
			i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + 0.01f);
			yield return new WaitForSeconds(t);
		}
	}

	public IEnumerator FadeTextToZeroAlpha(float t, Text i)
	{
		i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
		while (i.color.a > 0.0f)
		{
			i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - 0.01f);
			yield return new WaitForSeconds(t);
		}
	}

	IEnumerator re()
	{
		yield return new WaitForSeconds(3);
		b = false;
		StartCoroutine (FadeTextToZeroAlpha(0.001f, t));
	}

	public void sptext(string s)
	{
		t.text = s;
		b = true;
		StartCoroutine (FadeTextToFullAlpha (0.001f, t));
		StartCoroutine (re ());
	}

	IEnumerator endgame () {
		yield return new WaitForSeconds (5f);
		sptext("GAME OVER");
		yield return new WaitForSeconds (5f);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}

	public	void	UPSTAT(string str) {
		if (cptpts > 0) {
			if (str == "STR") {
				stat.STR += 1;
				cptpts -= 1;
			} else if (str == "AGI") {
				stat.AGI += 1;
				cptpts -= 1;
			} else if (str == "CON") {
				stat.HP = (stat.HP == stat.MaxHP) ? stat.MaxHP + 5 : stat.HP ;
				stat.CON += 1;
				cptpts -= 1;
			}
		}
	}

	void	levelup() {
		stat.xp = 0;
		stat.nextxp *= 1.5f;
		stat.nextxp = Mathf.Floor (stat.nextxp);
		stat.level += 1;
		cptpts += 5;
		stat.HP = stat.MaxHP;
		StartCoroutine (noticemesempai(LVLtxt));
	}

	IEnumerator	noticemesempai(Text lvl) {
		while (lvl.fontSize < 50) {
			lvl.fontSize += 1;
			yield return new WaitForSeconds(0.01f);
		}
		while (lvl.fontSize > 25) {
			lvl.fontSize -= 1;
			yield return new WaitForSeconds(0.01f);
		}	
	}

	void	OnTriggerEnter(Collider Col) {
		if (Col.CompareTag ("popohp")) {
			stat.HP += stat.MaxHP * 0.3f;
			Destroy (Col.gameObject);
		}
	}
}