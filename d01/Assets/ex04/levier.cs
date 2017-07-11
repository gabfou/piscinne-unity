using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levier : MonoBehaviour {
	public GameObject[] g;
	// Use this for initialization
	void Start () {
		
	}

	public void activator(GameObject go)
	{
		transform.localScale = new Vector3(-0.4F, 0.4F, 1);
		int i = -1;
		while (++i < g.Length) {
			if (g[i].tag == "porte")
				g[i].SetActive(false);
			if (g[i].tag == "cabouge")
				g[i].GetComponent<cabouge>().reinit();
			if (g [i].tag == ("becomeb")) {
				g [i].layer = 10;
				g [i].GetComponent<Renderer>().material.color = Color.blue;
			}
			if (g [i].tag == ("becomew")) {
				g [i].layer = go.layer;
				g [i].GetComponent<Renderer>().material.color = go.GetComponent<playerScript_ex01>().color;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
