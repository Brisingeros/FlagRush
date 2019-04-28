using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagManager : MonoBehaviour {

	public Team.team team;

	void Start(){
	}

	void OnTriggerEnter(Collider other){

		Aspect p = other.GetComponent<Aspect> ();

		if (p != null && p.aspectAct == Aspect.aspect.NPC && p.teamAct != team){
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
