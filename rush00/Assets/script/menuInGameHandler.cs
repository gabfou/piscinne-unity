using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuInGameHandler : MonoBehaviour {

	public Button restartButton;
	public Button menuButton;

	void Start() {
		Button start = restartButton.GetComponent<Button>();
		Button stop = menuButton.GetComponent<Button>();
		start.onClick.AddListener(beginGame);
		stop.onClick.AddListener(menuGame);
	}

	void beginGame() {
		Debug.Log ("reload");
		Application.LoadLevel(Application.loadedLevel);
	}

	void menuGame() {
		Debug.Log ("menu");
		Application.LoadLevel (0);
	}
}