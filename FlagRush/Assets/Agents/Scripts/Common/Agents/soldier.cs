using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Player {

    protected override void initPlayer(){

		defaultHealth = 3;
		anim.SetInteger ("Lives", defaultHealth);
		typeNpc = TypeNPC.type.Soldier;

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
	}
}
