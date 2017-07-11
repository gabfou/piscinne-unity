using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canon : MonoBehaviour {

	// Use this for initialization
	public AudioClip boom;
	public AudioClip piou;
	public AudioClip piouboom;
	public int missilenb;
//	public GameObject impact;
	AudioSource t;
	ParticleSystem impact;
	public missille missille;
	AudioSource audio;

	void Start () {
		impact = GetComponent<ParticleSystem> ();
		audio = GetComponent<AudioSource> ();
	}
		

	public void mitraillette()
	{
		RaycastHit hit;
		ParticleSystem.EmitParams param = new ParticleSystem.EmitParams ();

		Physics.Raycast(transform.position, transform.forward, out hit, 150);
		if (hit.collider == null)
			return;
		param.position = hit.point;
		param.rotation3D = hit.normal;
		impact.Emit (param, 1);
		if (hit.collider.tag == "tank" && hit.collider.gameObject.GetComponent<commun>())
			hit.collider.gameObject.GetComponent<commun>().life -= 1;
		audio.PlayOneShot(piou);
		AudioSource.PlayClipAtPoint(piouboom, hit.point);
		//GameObject.Instantiate (impact, hit.point, Quaternion.identity);
	}

	public void missile()
	{
		GameObject.Instantiate (missille, transform.position + transform.forward * 3, transform.rotation);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
