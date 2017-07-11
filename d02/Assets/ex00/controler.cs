using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class controler : MonoBehaviour {

	List<unit> currentunit;
	public List<GameObject> gentil;
	public List<GameObject> mechant;
	int passframe;
	public bool end;
	// Use this for initialization
	void Start () {
		currentunit = new List<unit> ();
		passframe = 0;
		end = false;
	}

	public void CHARGE(GameObject obj)
	{
		int i = -1;
		foreach (unit tmp in currentunit) {
			if (++i == 0)	
				tmp.sayok();
			tmp.newcibleu (obj);
		}
		passframe = 1;
	}

	public void changecontrol(unit obj)
	{
		if (obj.control == false) {
			CHARGE (obj.gameObject);
			return;
		}
		obj.supcibleu ();
		if (!(Input.GetKey (KeyCode.LeftControl)))
			currentunit.Clear ();
		currentunit.Add(obj);
		obj.sayok ();
		passframe = 1;
	}

	// Update is called once per frame
	void Update () {
		int i;

		if (end)
			return;
		gentil.RemoveAll (item => item == null);
		mechant.RemoveAll (item => item == null);
		if (gentil.Count == 0) {
			Debug.Log ("The Orc Team wins.");
			end = true;
		}
		if (mechant.Count == 0) {
			Debug.Log ("The human Team wins.");
			end = true;
		}
		if (passframe == 1) {
			passframe = 0;
			return;
		}
		if (Input.GetMouseButtonDown (1))
			currentunit.Clear ();
		if (Input.GetMouseButtonDown (0) && currentunit != null) {
			i = -1;
			foreach (unit tmp in currentunit) {
				if (++i == 0)
					tmp.sayok();
				tmp.supcibleu ();
				tmp.cible = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				tmp.cible.z = 0;
				tmp.triggerdepl();
			}
		}
	}
}
