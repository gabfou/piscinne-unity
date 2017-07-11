using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class commun : MonoBehaviour {

	public int life;
	public GameObject p;
	// Use this for initialization
	void Start () {
		
	}

	IEnumerator mort()
	{
		GameObject.Instantiate(p, transform.position, Quaternion.identity);
		yield return new WaitForSeconds (1f);
		if (GetComponent<tank> () != null)
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		else
			GameObject.Destroy (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if (life < 1)
				StartCoroutine(mort());
	}
}
