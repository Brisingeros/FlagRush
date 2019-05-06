using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagManager : MonoBehaviour {

	public Team.team team;

	void Start(){
	}

	void OnTriggerEnter(Collider other){

		Player p = other.GetComponent<Player> ();

		if (p != null && p.aspectAct == Aspect.aspect.NPC && p.getTypeNpc() == TypeNPC.type.Soldier && p.teamAct != team){
			Debug.Log (team.ToString() + " VICTORY");


			#if UNITY_EDITOR
			{
				UnityEditor.EditorApplication.isPlaying = false;
			}
			#else
			{
				Application.Quit ();
			}
			#endif
		}

	}

}
