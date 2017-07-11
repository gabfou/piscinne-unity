using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuButtonHandler : MonoBehaviour {

	public Button startButton;
	public Button quitButton;

	void Start() {
		Button start = startButton.GetComponent<Button>();
		Button stop = quitButton.GetComponent<Button>();
		start.onClick.AddListener(beginGame);
		stop.onClick.AddListener(quitGame);
	}

	void beginGame() {
		Application.LoadLevel (1);
	}

	void quitGame() {
		Application.Quit ();
		Debug.Log ("quit");
	}
}
