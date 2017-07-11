using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMenuChangingColor : MonoBehaviour {
	public Color color1;
	public Color color2;
	public float duration = 5.0F;

	Camera camera;

	void Start() {
		color1.a = 0.5f;

		color2.a = 0.5f;

		camera = GetComponent<Camera>();
		camera.clearFlags = CameraClearFlags.SolidColor;
	}

	void Update() {
		float t = Mathf.PingPong(Time.time, duration) / duration;
		camera.backgroundColor = Color.Lerp(color1, color2, t);
	}
}
