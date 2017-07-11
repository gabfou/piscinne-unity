using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class supertext : MonoBehaviour {

	// Use this for initialization
	bool b = false;
	Text t;
	void Start () {
		t = GetComponent<Text> ();
		t.color = new Color(255, 44, 144, 0);
		sptext("Find the strange guy");
	}

	public IEnumerator FadeTextToFullAlpha(float t, Text i)
	{
		i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
		while (i.color.a < 1.0f)
		{
			i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + 0.01f);
			yield return new WaitForSeconds(t);
		}
	}

	public IEnumerator FadeTextToZeroAlpha(float t, Text i)
	{
		i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
		while (i.color.a > 0.0f)
		{
			i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - 0.01f);
			yield return new WaitForSeconds(t);
		}
	}

	IEnumerator re()
	{
		yield return new WaitForSeconds(3);
		b = false;
		StartCoroutine (FadeTextToZeroAlpha(0.001f, t));
	}

	public void sptext(string s)
	{
		t.text = s;
		b = true;
		StartCoroutine (FadeTextToFullAlpha (0.001f, t));
		StartCoroutine (re ());
	}
	// Update is called once per frame
	void Update () {
	}
}
