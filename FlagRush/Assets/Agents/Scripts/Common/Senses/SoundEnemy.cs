using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEnemy : Sense {

	void OnTriggerEnter(Collider other){
	
		Aspect aspect = other.GetComponent<Aspect> ();

		if (aspect != null) {
			
			if (aspect.alive && aspect.teamAct != pla.teamAct)
				Debug.Log ("Enemigo escuchado");
			
		}
	
	}

}
