using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class perso : MonoBehaviour {

	public	float MaxHP;
	private float fgds;
//	[HideInInspector]	 public float HP;
	[HideInInspector]	 public float HP { get{ return fgds;} set{
			if (value > MaxHP)
				value = MaxHP;
			fgds = value;
		} }
	public	int STR;
	public	int AGI;
	public	int CON;
	public	int Armor;
	public	int minDamage;
	public	int maxDamage;
	public	int level;
	public	float xp;
	public	int money;
	public	float nextxp;
	// Use this for initialization
	void Start () {
		level = 1;
		nextxp = 100;
		xp = 0;
		statupdate ();
		HP = MaxHP;
	}
	
	// Update is called once per frame
	void Update () {
//		if (HP > MaxHP)
//			HP = MaxHP;
		statupdate ();
	}

	public void attack(perso cible)
	{
		int roll = Random.Range(0, 100);
		if (roll > 75 + AGI - cible.AGI)
			return;
		int basedamage = Random.Range (minDamage, maxDamage);
		cible.ouch (basedamage * (1 - cible.Armor / 200));
	}

	public void ouch(int d)
	{
		HP -= d;
	}

	public	void	statupdate() {
		MaxHP = 5 * CON;
		minDamage = STR / 2;
		maxDamage = minDamage + 4;
	}

	public void		brillance()
	{
		
	}
}
