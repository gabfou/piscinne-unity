using UnityEngine;
using System.Collections;


public class cameraScript : MonoBehaviour {

	public GameObject	activePlayer;

	public Vector2		movingBounds = new Vector2(0, .1f);

	private Camera		camera;
	GM gm;

	void Start() {
		camera = GetComponent< Camera > ();
		gm = GetComponent< GM > ();
	}

	void Update () {
		Vector3 pos = activePlayer.transform.position;
		Vector3 camPos = camera.transform.position;

		if (gm.end > 0)
			return;
//		Debug.Log (Mathf.Abs (camPos.y - pos.y));
		if (Mathf.Abs (camPos.y - pos.y) > movingBounds.y)
			camPos.y = pos.y + movingBounds.y * Mathf.Sign(camPos.y - pos.y);
		camera.transform.position = new Vector3 (pos.x, camPos.y, -9);
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.blue;
		Gizmos.DrawRay (activePlayer.transform.position + Vector3.up * movingBounds.y + Vector3.left / 2f, Vector3.right);
		Gizmos.DrawRay (activePlayer.transform.position + Vector3.down * movingBounds.y + Vector3.left / 2f, Vector3.right);
	}
}
