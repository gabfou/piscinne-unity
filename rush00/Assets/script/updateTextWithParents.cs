using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class updateTextWithParents : MonoBehaviour {

	public Text parent;
	private Transform _transform;

	// Use this for initialization
	void Start () {
		_transform = transform;
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<Text> ().text = parent.text;
	}
}
