using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class dragndrop : MonoBehaviour
{
	Color mouseOverColor = Color.blue;
	Color originalColor = Color.yellow;
	bool dragging = false;
	float distance;
	Vector3 initpos;
	public towerScript obj;
	public GameObject objtmp;
	GameObject tmp;
	bool canpos = false;
	Collider2D c2;

	void start()
	{
		initpos = transform.position;
	}


	public void debutdrag()
	{
		initpos = transform.position;
	}

//	void OnTriggerEnter2D()
//	{
//		Debug.Log ("fds");
//		canpos = true;
//	}

	void OnTriggerStay2D(Collider2D c)
	{
		Debug.Log ("fds2");
		c2 = c;
		canpos = true;
	}

	void OnTriggerExit2D(Collider2D c)
	{
		Debug.Log ("fd3");
		if (c == c2)
		canpos = false;
	}

	public void drop()
	{Debug.Log ("drop");
		RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.down, Mathf.Infinity);
		if (hit.collider != null && hit.collider.tag == "empty") {
			GameObject.Instantiate (obj, hit.collider.transform.position, Quaternion.identity);
			hit.collider.tag = "Untagged";
		}
		transform.position = initpos;
	}

	public void Drag(){Debug.Log ("drag");
		Vector3 tmpv = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		tmpv.z = 0;
		transform.position = tmpv;
	}

	void Update()
	{
		Debug.Log (canpos);
//		if (dragging)
//		{
//			Drag ();
//		}
	}
}
