using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagManager : MonoBehaviour {

	public string teamS;
	private Team.team team;

	void Start(){
		if (teamS.Equals ("blue")) {
			team = Team.team.Blue;
		} else if (teamS.Equals ("red")) {
			team = Team.team.Red;
		}
	}

	void OnTriggerEnter(Collider other){

		Aspect p = other.GetComponent<Aspect> ();

		if (p != null && p.aspectAct == Aspect.aspect.NPC && p.teamAct != team){
			//WIN
		}

	}

}
