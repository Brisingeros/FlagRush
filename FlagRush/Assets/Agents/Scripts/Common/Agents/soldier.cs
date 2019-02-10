using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Player {

	protected void initPlayer(){

		defaultHealth = 3;
		anim.SetInteger ("Lives", defaultHealth);

	}
	
	// Update is called once per frame
	void Update () {
		anim.SetBool("Alerta", enemiesSound.Count > 0);
		anim.SetBool("Enemigo", focus != null);
	}
}
