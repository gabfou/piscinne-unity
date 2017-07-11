using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class win : MonoBehaviour {

	public supertext t;
	// Use this for initialization
	void Start () {
		
	}

	IEnumerator win2()
	{
		yield return new WaitForSeconds (3);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
	// Update is called once per frame
	void OnTriggerEnter(Collider c)
	{
		if (c.tag == "Player") {
			StartCoroutine(win2());
			t.sptext("you win");
		}
	}
}
