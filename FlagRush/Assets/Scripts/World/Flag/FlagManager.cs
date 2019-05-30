using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagManager : MonoBehaviour {

	public Team.team team;

	void Start(){
	}

	void OnTriggerEnter(Collider other){

		Player p = other.GetComponent<Player> ();

        if (p != null && p.aspectAct == Aspect.aspect.NPC && p.getTypeNpc() == TypeNPC.type.Soldier && p.teamAct != team)
        {

            UIController ui = FindObjectOfType<UIController>();
            ui.setVictoryOnScreen(p.teamAct);
            Invoke("changeScene", 3.0f);

        }


	}

}
