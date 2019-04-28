﻿using System.Collections;
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

	public override void removeSoldiers(string type)
    {
        if (type.Equals("enemy"))
        {
            enemies = enemies.FindAll(x => x != null);

        }
    }

    public override void removeDestroyedSounds(string type)
    {

        if (type.Equals("enemy"))
        {
            enemiesSound = enemiesSound.FindAll(x => x != null);

        }

    }

	// Update is called once per frame
	void Update () {
		anim.SetBool("Alerta", enemiesSound.Count > 0);
		anim.SetBool("Enemigo", focus != null);

		//fijar "Peligro" según la distancia al enemigo visible más cercano
		bool peligro = anim.GetBool("Peligro");
		if (!peligro && focus != null){
			float dis = Vector3.Distance(focus.transform.position, transform.position);
			peligro = dis < 20;
			anim.SetBool ("Peligro", peligro);
		}
			
	}
}
