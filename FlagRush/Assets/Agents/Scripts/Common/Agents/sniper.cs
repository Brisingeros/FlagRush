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

        if (anim.GetInteger("Lives") > -1)
        {
            anim.SetBool("Alerta", enemiesSound.Count > 0);
            anim.SetBool("Enemigo", focus != null);

            bool peligro = anim.GetBool("Peligro");
            if (!peligro && focus != null)
            {
                float dis = Vector3.Distance(focus.transform.position, transform.position);
                peligro = dis < 20;
                anim.SetBool("Peligro", peligro);
            }
        }
        else
        {
            anim.SetBool("Alerta", false);
            anim.SetBool("Enemigo", false);
            anim.SetBool("Peligro", false);
        }
	
	}

	override public void generateSound(){
		hidden = false;
		GameObject snd = Instantiate(basicSound);
		barricade.generateSound (snd, positionBarricade);

		Sound sndComp = snd.GetComponent<Sound> ();
		sndComp.teamAct = teamAct;
		sndComp.alive = alive;

		if(!alive)
			snd.transform.SetParent(transform);
	}
}
