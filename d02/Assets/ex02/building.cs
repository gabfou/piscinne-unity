using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class building : MonoBehaviour {

	public GameObject obj;
	public int life;
	float timesinceprevious;
	public bool ishotel;
	public AudioClip ouchs;
	public AudioClip die;
	public float spawner;
	public building bspawn;
	float timesincesdead;
	// Use this for initialization
	void Start () {
		spawner = 10;
		timesinceprevious = 0;
		timesincesdead = 0;
	}

	public void ouch(int i)
	{
		life -= i;
//		if (gameObject.tag == "mechanth") {
//			
//		}
		//GetComponent<AudioSource> ().PlayOneShot (ouchs);
	}


	void OnMouseOver()
	{
		if (Input.GetMouseButtonDown (0)) {
			Camera.main.GetComponent<controler> ().CHARGE (this.gameObject);
		}
	}


	// Update is called once per frame
	void Update () {
		if (Camera.main.GetComponent<controler> ().end)
			return ;
		timesinceprevious += Time.deltaTime;
		if (ishotel && timesinceprevious > 10) {
			GameObject.Instantiate (obj, transform.position, Quaternion.identity);
			timesinceprevious = 0;
		}

		if (life < 1) {
			if (timesincesdead == 0)
				GetComponent<AudioSource> ().PlayOneShot (die);
			timesincesdead += Time.deltaTime;
			if (timesincesdead < 1)
				return;
			if (bspawn != null)
				bspawn.spawner += 2.5F;
			GameObject.Destroy (gameObject);
		}
	}
}
