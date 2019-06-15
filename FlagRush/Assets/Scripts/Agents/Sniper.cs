using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Player {

	public int positionBarricade;
	public Barricade barricade;

    protected override void initPlayer(){
		targetTypes = new TypeNPC.type[2];
		targetTypes[0] = TypeNPC.type.Soldier;
		targetTypes [1] = TypeNPC.type.Nurse;

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

        bool peligro = anim.GetBool("Peligro");

        if (!peligro)
        {
            anim.SetBool("Alerta", enemiesSound.Count > 0 && !getHidden());
            anim.SetBool("Enemigo", focus != null && !getHidden());

            if (!peligro && focus != null)
            {
                float dis = Vector3.Distance(focus.transform.position, transform.position);
                peligro = dis < 30;
                anim.SetBool("Peligro", peligro && !getHidden());
            }
        }

	}

    public void resetAspect()
    {
        enemiesSound = new List<Aspect>();
        enemies = new List<Player>();
        focus = null;
    }

	override public void generateSound(){
		hidden = false;
		GameObject snd = barricade.generateSound (this, positionBarricade);
	}

    protected override void setAnimWalk(bool hidden)
    {
        if (hidden)
        {
            getAnimator().SetFloat("Crouch", 1.0f);
        }
        else
        {
            getAnimator().SetFloat("Crouch", 0.0f);

        }
    }
}
