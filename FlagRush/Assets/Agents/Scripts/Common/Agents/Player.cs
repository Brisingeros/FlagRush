using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Aspect {

	public int defaultHealth;

	private int health;

	// Use this for initialization
	void Start () {
		aspectAct = Aspect.aspect.NPC;
		health = defaultHealth;
		alive = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Team.team getTeam(){
		return teamAct;
	}

	public void setTeam(Team.team tM){
		teamAct = tM;
	}

	public void getShot(){
		health--;
		alive = health > 0;
	}

	public void revive(){
		health = defaultHealth;
		alive = true;
	}

}
