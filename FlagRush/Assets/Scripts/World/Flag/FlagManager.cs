using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagManager : MonoBehaviour {

	public Team.team team;
	private WorldManager mG;
    static private bool gameOver;

	void Start(){

        gameOver = false;
        mG = FindObjectOfType<WorldManager> ();
	}

	void OnTriggerEnter(Collider other){

		Player p = other.GetComponent<Player> ();

        if (!gameOver && p != null && p.aspectAct == Aspect.aspect.NPC && p.getTypeNpc() == TypeNPC.type.Soldier && p.teamAct != team)
        {
            gameOver = true;
            mG.GameOver(p.teamAct);
    
        }


    }

}
