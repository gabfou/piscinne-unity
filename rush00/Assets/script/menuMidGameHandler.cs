using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class menuMidGameHandler : MonoBehaviour {

	public Button restartButton;
	public Button menuButton;

	void Start() {
		Button start = restartButton.GetComponent<Button>();
		Button stop = menuButton.GetComponent<Button>();
		start.onClick.AddListener(beginGame);
		stop.onClick.AddListener(menuGame);
	}

	void beginGame() {
		Application.LoadLevel (2);
	}

	void menuGame() {
		Application.LoadLevel (0);
	}
}
