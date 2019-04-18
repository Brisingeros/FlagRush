using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Player {

    protected override void initPlayer(){

		defaultHealth = 3;
		anim.SetInteger ("Lives", defaultHealth);
		typeNpc = TypeNPC.type.Soldier;

	}
	
	// Update is called once per frame
	void Update () {
		anim.SetBool("Alerta", enemiesSound.Count > 0);
		anim.SetBool("Enemigo", focus != null);
	}
}
