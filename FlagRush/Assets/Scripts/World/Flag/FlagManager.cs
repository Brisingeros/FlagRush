using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagManager : MonoBehaviour {

	public Team.team team;
	private WorldManager mG;

	void Start(){
		mG = FindObjectOfType<WorldManager> ();
	}

	void OnTriggerEnter(Collider other){

		Player p = other.GetComponent<Player> ();

        if (p != null && p.aspectAct == Aspect.aspect.NPC && p.getTypeNpc() == TypeNPC.type.Soldier && p.teamAct != team)
			mG.GameOver (p.teamAct);


	}

}
