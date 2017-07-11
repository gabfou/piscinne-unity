using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour {

	public    Text        timer;
	public    Text        scoring;
	public    Text        rings;
	public    Text        FScore;
	public    int            end;
	public    AudioSource    musicgame;
	public    AudioSource    musicfin;
	public    int         LevelNumber;
	public		int tmort;
	private    string        minutes;
	public    float        seconds;
	private    string        time;
	public    float        score;
	private    float        finalscore;
	public int	ringnb = 0;
	public ringobj ringobject;
	// Use this for initialization
	void Start () {
		end = 0;
		musicgame.Play();
	}

	// Update is called once per frame
	void Update () {
//		Debug.Log (Time.timeSinceLevelLoad % 60);
		if (end == 0) {
			TimeUpdate ();
			scoring.GetComponent<Text>().text = "Score : " + score.ToString();
		}
		else if (end == 1) {
			score += (int)((600 - Time.timeSinceLevelLoad) > 0 ? (600 - Time.timeSinceLevelLoad) : 0);
			end = 2;
			StartCoroutine(toEnd(6f));
		}
	}

//	void    LevelEnded() {
//		if (Time.timeSinceLevelLoad > seconds + 6) {
//			Debug.Log ("Fin");
//			FScore.GetComponent<Text>().text = score.ToString();
//			if (PlayerPrefs.GetFloat ("HighScore" + LevelNumber.ToString()) < score)
//				PlayerPrefs.SetFloat ("HighScore" + LevelNumber.ToString(), score);
//			if (PlayerPrefs.GetInt ("unlocked") < LevelNumber)
//				PlayerPrefs.SetInt ("unlocked", LevelNumber);
//		}
//	}

	void    TimeUpdate() {
		minutes = Mathf.Floor(Time.timeSinceLevelLoad / 60).ToString("00");
		seconds = Time.timeSinceLevelLoad;
		time = ("TIME : "+ minutes + " : " + (seconds % 60).ToString("00"));
		timer.GetComponent<Text>().text = time;
	}

	public void addring()
	{
//		Debug.Log("score: " + score.ToString());
		score += 5;
		ringnb++;
		rings.GetComponent<Text>().text = ringnb.ToString();
	}

	public void instantring(Vector3 tmp)
	{
		ringnb = ringnb / 2 + 1;
		while (--ringnb > 0)
			GameObject.Instantiate(ringobject, tmp, Quaternion.identity);
		rings.GetComponent<Text>().text = ringnb.ToString();
	}


	IEnumerator toEnd(float time) {
		Debug.Log ("start Coroutine");
		musicgame.Stop ();
		musicfin.Play ();
		yield return new WaitForSeconds (time);
		Debug.Log ("Fin");
		FScore.GetComponent<Text> ().text = score.ToString ();
		if (PlayerPrefs.GetFloat ("HighScore" + LevelNumber.ToString ()) < score)
			PlayerPrefs.SetFloat ("HighScore" + LevelNumber.ToString (), score);
		if (PlayerPrefs.GetInt ("unlocked") <= LevelNumber)
			PlayerPrefs.SetInt ("unlocked", LevelNumber + 1);
		Debug.Log (tmort);
//		PlayerPrefs.SetInt ("TotalDeath", PlayerPrefs.GetInt ("TotalDeath") + tmort);
		PlayerPrefs.SetInt ("TotalRings", PlayerPrefs.GetInt ("TotalRings") + ringnb);
		yield return new WaitForSeconds (time);
		SceneManager.LoadScene ("DataSelect");
	}
}