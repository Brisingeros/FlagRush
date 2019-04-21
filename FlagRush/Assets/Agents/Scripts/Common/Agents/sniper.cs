using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Player {

	public int positionBarricade;
	public Barricade barricade;

    protected override void initPlayer(){

		defaultHealth = 2;
		anim.SetInteger ("Lives", defaultHealth);
		typeNpc = TypeNPC.type.Sniper;

	}

	// Update is called once per frame
	void Update () {
		anim.SetBool("Alerta", enemiesSound.Count > 0);
		anim.SetBool("Enemigo", focus != null);
		//fijar "Peligro" según la distancia al enemigo visible más cercano
		if (focus != null){
			float dis = Vector3.Distance(focus.transform.position, transform.position);
			anim.SetBool ("Peligro", dis < 40);
		}
			
	}
}
