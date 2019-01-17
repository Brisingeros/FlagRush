using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectiveEnemy : Sense {

	public GameObject shootPos;

	void OnTriggerEnter(Collider other){

		Aspect aspect = other.GetComponent<Aspect> ();

		if (aspect != null) {

			if (aspect.aspectAct == Aspect.aspect.NPC && aspect.alive && aspect.teamAct != pla.teamAct) {
				Player enemy = (Player)aspect;

				RaycastHit hit;
				Vector3 rayDirection = enemy.transform.position - shootPos.transform.position;

				if (Physics.Raycast(shootPos.transform.position, rayDirection, out hit)){ //Mirar si es necesario maxdistance
					Player plaHit = hit.collider.GetComponent<Player>();

					if (plaHit != null && plaHit == enemy) {
						Debug.Log ("Enemigo a la vista");
					}
				}
			}

		}

	}

}
