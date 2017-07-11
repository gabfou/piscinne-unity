using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DataDisplayer : MonoBehaviour {

	public	Text		TotalRings;
	public	Text		TotalDeath;
	public	Text 		HighScore;
	public	GameObject	selected1;
	public	GameObject	selected2;
	public	GameObject	selected3;
	public	GameObject	level1;
	public	GameObject	level2;
	public	GameObject	level3;

	private	int 		selected = 1;

	// Use this for initialization
	void Start () {
		LoadPrefs ();
		PlayerPrefs.Save ();

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			selected -= 1;
			if (selected == 0)
				selected = 3;
		} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			selected += 1;
			if (selected == 4)
				selected = 1;
		} else if (Input.GetKeyDown (KeyCode.Return)) {
			if (selected == 1)
				SceneManager.LoadScene ("Kim Jungle un");
			if (selected == 2 && PlayerPrefs.GetInt ("unlocked") > 1)
				SceneManager.LoadScene ("souslocean");
			if (selected == 3 && PlayerPrefs.GetInt ("unlocked") > 2)
				SceneManager.LoadScene ("batarus");
		}
		SelectLevel ();
		UpdateScore ();
	}


	void	LoadPrefs() {
		//		if (PlayerPrefs.GetInt ("TotalRings") == null)
		//			PlayerPrefs.SetString ("TotalRings" , "0");
		Debug.Log(PlayerPrefs.GetInt ("TotalDeath").ToString());
		TotalRings.GetComponent<Text>().text = PlayerPrefs.GetInt ("TotalRings").ToString();
		TotalDeath.GetComponent<Text>().text = PlayerPrefs.GetInt ("TotalDeath").ToString();
		if (PlayerPrefs.GetInt ("unlocked") == 0)
			PlayerPrefs.SetInt ("unlocked", 1);
		Debug.Log (PlayerPrefs.GetFloat ("HighScore1"));
		//		if (PlayerPrefs.GetFloat ("HighScore1") == 0)
		//			PlayerPrefs.SetFloat ("HighScore1", 0);
		HighScore.GetComponent<Text> ().text = PlayerPrefs.GetFloat ("HighScore1").ToString();
		Debug.Log (PlayerPrefs.GetInt ("unlocked"));
		if (PlayerPrefs.GetInt ("unlocked") < 2)
			level2.GetComponent<SpriteRenderer> ().color = Color.red;
		if (PlayerPrefs.GetInt ("unlocked") < 3)
			level3.GetComponent<SpriteRenderer> ().color = Color.red;

	}
	void	SelectLevel() {
		if (selected == 1) {
			selected1.SetActive (true);
			selected2.SetActive (false);
			selected3.SetActive (false);
		}
		else if (selected == 2) {
			selected2.SetActive (true);
			selected1.SetActive (false);
			selected3.SetActive (false);
		}
		else if (selected == 3) {
			selected3.SetActive (true);
			selected2.SetActive (false);
			selected1.SetActive (false);
		}
	}

	void	UpdateScore() {
		//		Debug.Log ("Mais LOL");
		//		Debug.Log (PlayerPrefs.GetString ("HighScore" + selected.ToString()));
		HighScore.GetComponent<Text> ().text = PlayerPrefs.GetFloat ("HighScore" + selected.ToString()).ToString();
	}


}
