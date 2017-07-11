using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class checkpoint : MonoBehaviour {

	public List<checkpoint> listp;
	public List<List<checkpoint>> listofpath;
	int i = 1;

	// Use this for initialization
	void Start () {
		BoxCollider2D col = GetComponent<BoxCollider2D> ();
		Vector2 pos = transform.position;
		Physics2D.queriesHitTriggers = true;
		Collider2D[] c = Physics2D.OverlapAreaAll (new Vector2 (pos.x - col.bounds.extents.x, pos.y + col.bounds.extents.y),
			new Vector2 (pos.x + col.bounds.extents.x, pos.y - col.bounds.extents.y));
		Debug.DrawLine (new Vector2 (pos.x - col.bounds.extents.x, pos.y + col.bounds.extents.y), new Vector2 (pos.x + col.bounds.extents.x, pos.y - col.bounds.extents.y), Color.red);
		int i = -1;
		while (++i < c.Length)
		{
//			Debug.Log (this.name + "::::::::" + c [i].name);
			if (c [i].gameObject.GetComponent<checkpoint> () != null) {
//				Debug.Log (this.name + "::::::::" + c [i].name);
				listp.Add (c [i].GetComponent<checkpoint> ());
			}
		}
		Physics2D.queriesHitTriggers = true;
		this.i = 1;
	}


	void OnTriggerEnter (Collider other)
	{

	}

	void OnTriggerexit (Collider other)
	{
		gameObject.SetActive (true);
	}

	public List<Vector2> pathto(checkpoint c, Vector2 a)
	{
		List<Vector2> ret;
		List<checkpoint> tmp;

		ret = new List<Vector2>();
		if (c.GetInstanceID() == this.GetInstanceID()) {
			ret.Add(c.transform.position);
			return (ret);
		}
		int y = 0;
		tmp = listofpath [0];
		while (++y < listofpath.Count &&  tmp[0].GetInstanceID() != c.GetInstanceID())
				tmp = listofpath[y];
		y = - 1;
		while (++y < tmp.Count)
		{
			ret.Add (tmp [y].transform.position);
		}
		ret.Add (a);
		return (ret);
	}

	List<checkpoint> waza(List<checkpoint> l, checkpoint actu, checkpoint dest)
	{
//		List.port
		List<checkpoint> ret = null;
		if (l.FirstOrDefault(n=>n.GetInstanceID() == actu.GetInstanceID()) != null)
			return (null);
		Debug.Log ("              " + this.name +":" + actu.name +":" + dest.name);
		l.Add (actu);
		if (actu.GetInstanceID() == dest.GetInstanceID())
			return l;
		int y = -1;
		while(++y < l[l.Count - 1].listp.Count)
		{
			//Debug.Log (this.name +":::::::::" + l[l.Count - 1].listp[y].gameObject.name +":::::::::::" + l[l.Count - 1].listp.Count);
			if ((ret = waza(l, l[l.Count - 1].listp[y], dest)) != null)
				return ret;
		}
		Debug.Log ("error");
		return (null);
	}

	// Update is called once per frame
	void Update () {
		BoxCollider2D col = GetComponent<BoxCollider2D> ();
		Vector2 pos = transform.position;
		Debug.DrawLine (new Vector2 (pos.x - col.bounds.extents.x, pos.y + col.bounds.extents.y), new Vector2 (pos.x + col.bounds.extents.x, pos.y - col.bounds.extents.y), Color.red);
		if (i == 1) {
			listofpath = new List<List<checkpoint>>();
			Debug.Log ("fds 1");
			checkpoint tmp2;
			i = 0;
			checkpoint[] checktmp = FindObjectsOfType<checkpoint> ();
			int y  = -1;				
			y = -1;
			while (++y < checktmp.Length) {
				List<checkpoint> ca;
				ca = waza (new List<checkpoint>(), this, checktmp [y]);
				listofpath.Add (ca);
				Debug.Log ("fds 2");
				}
			//tmp.Add (this);
//				while (tmp2.GetInstanceID() != check[i].GetInstanceID()) {
//					tmp2 = check[i];
//					tmp.Add(tmp2);
//					if (
		}
	}
}
