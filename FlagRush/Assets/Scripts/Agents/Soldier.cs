using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Player {

    protected override void initPlayer(){
		targetTypes = new TypeNPC.type[3];
		targetTypes[0] = TypeNPC.type.Soldier;
		targetTypes [1] = TypeNPC.type.Sniper;
		targetTypes [2] = TypeNPC.type.Nurse;

		defaultHealth = 3;
		anim.SetInteger ("Lives", defaultHealth);
		typeNpc = TypeNPC.type.Soldier;
	}

	public override void removeSoldiers(string type)
    {
        if (type.Equals("enemy"))
            enemies = enemies.FindAll(x => x != null);
    }

    public override void removeDestroyedSounds(string type)
    {
        if (type.Equals("enemy"))
            enemiesSound = enemiesSound.FindAll(x => x != null);
    }
	
	// Update is called once per frame
	void Update () {
        anim.SetBool("Alerta", enemiesSound.Count > 0);
        anim.SetBool("Enemigo", focus != null);
	}

    protected override void setAnimWalk(bool hidden)
    {
        if (hidden) //anda agachado
        {
            getAnimator().SetFloat("Walk", 1.0f);
        }
        else //anda normal
        {
            getAnimator().SetFloat("Walk", 0.0f);

        }
    }
}
