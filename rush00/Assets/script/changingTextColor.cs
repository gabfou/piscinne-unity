using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changingTextColor : MonoBehaviour {

	public Color color1;
	public Color color2;
	public float duration;

	Text text;

	void Start() {
		text = GetComponent<Text> ();
	}

	void Update() {
		float t = Mathf.PingPong(Time.time, duration) / duration;
		text.color = Color.Lerp(color1, color2, t);
	}
}
