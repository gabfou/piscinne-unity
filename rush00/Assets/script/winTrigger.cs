using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class winTrigger : MonoBehaviour {

	public GameObject victoryPannel;

	void OnTriggerEnter2D(Collider2D hit) {
		Debug.Log ("hit");
		Debug.Log (hit.GetComponent<Collider2D>().tag);
		if (hit.gameObject.CompareTag ("Player")) {
			victoryPannel.gameObject.SetActive(true);
			hit.gameObject.GetComponent<BernardManager>().victory = true;
		}
	}
}