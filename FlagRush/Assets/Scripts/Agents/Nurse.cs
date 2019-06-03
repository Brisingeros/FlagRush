﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Nurse : Player {

    List<Aspect> allySounds;
    List<Player> allies;
    public Player focusAlly;

	private readonly float distanceHuir = 20;

    protected override void initPlayer()
    {
		targetTypes = new TypeNPC.type[2];
		targetTypes[0] = TypeNPC.type.Soldier;
		targetTypes [1] = TypeNPC.type.Sniper;

        defaultHealth = 1;
        anim.SetInteger("Lives", defaultHealth);
		typeNpc = TypeNPC.type.Nurse;

        allySounds = new List<Aspect>();
        allies = new List<Player>();
    }

    // Update is called once per frame
    void Update () {
        bool huir = anim.GetBool("Peligro");

        if (!huir)
        {
            huir = focus != null;
            if (!huir)
            {
                if (enemiesSound.Count > 0 || enemies.Count > 0)
                {
					float distanceNow = distanceHuir + (mG.getTeamMoral(teamAct)/10);
					huir = (enemiesSound.Count > 0) ? Vector3.Distance(enemiesSound[0].transform.position, transform.position) < distanceNow : (enemies.Count > 0) ? Vector3.Distance(enemies[0].transform.position, transform.position) < distanceNow : false;
                }
                else
                {
                    anim.SetBool("Alerta", allySounds.Count > 0);
                    anim.SetBool("Aliado", allies.Count > 0);
                }
            }
            anim.SetBool("Peligro", huir);
        }
	}

    public void SetFocus()
    {
        focusAlly = allies.Count > 0?allies[0]: null;
    }

    public void changeAllyToRevive(Player p)
    {

        Player player = allies.Find(x => x == p);

        if (player != null)
        {
            removeAlly(player);
            getAnimator().SetBool("Aliado", getAnimator().GetBool("Aliado") && player != focusAlly);
        }
    }

    public void broadcastToNurses()
    {
        List<Nurse> nurses = FindObjectsOfType<Nurse>().ToList();
        nurses = nurses.FindAll(x => x != this && x.getTeam() == getTeam());

        nurses.ForEach(nurse=> {

            nurse.changeAllyToRevive(focusAlly);

        });
    }

    public void addAlly(Player a)
    {
        allies.Add(a);
        OrderAlliesByDistance("vision");
    }

    public override void removeSoldiers(string type)
    {
        if (type.Equals("enemy"))
        {
			enemies = enemies.FindAll(x => x != null && x.GetComponent<Nurse>() == null);
        }
        else if (type.Equals("ally"))
        {
            allies = allies.FindAll(x => x != null);
        }
    }

    public override void removeDestroyedSounds(string type)
    {
        if (type.Equals("enemy"))
            enemiesSound = enemiesSound.FindAll(x => x != null);
       	else if (type.Equals("ally"))
            allySounds = allySounds.FindAll(x => x != null);
    }

    public bool removeAlly(Player a)
    {
        bool success = allies.Remove(a);
        if (a.gameObject.GetComponentInChildren<Aspect>())
        {
            Aspect sound = a.gameObject.GetComponentInChildren<Aspect>();
            removeSound(sound);
        }
        
        OrderAlliesByDistance("vision");
        focusAlly = allies.Count > 1 ? allies[0] : null;

        return success;
    }

    public override void addSound(Aspect a){
        allySounds.Add(a);
    }

	public override bool removeSound(Aspect a)
    {
        return allySounds.Remove(a);
    }

    public void HelpAlly()
    {
        OrderAlliesByDistance("sound");
        Aspect goal = allySounds.Count > 0? getAllySound(0): null;
		if (goal != null)
			getAgent ().destination = goal.transform.position;
    }

    public void OrderAlliesByDistance(string type)
    {
		if (type.Equals ("vision")) {
            removeSoldiers("ally");
			allies = allies.OrderBy (x => Vector3.Distance (x.transform.position, transform.position)).ToList ();
			SetFocus();
		} else if (type.Equals ("sound")) {
            removeDestroyedSounds("ally");
			allySounds = allySounds.OrderBy(x => Vector3.Distance(x.transform.position, transform.position)).ToList();
		}
    }

    public Aspect getAllySound(int pos)
    {
        return allySounds[pos];
    }

    public Player getAlly(int pos)
    {
        return allies[pos];
    }

    public int getAllySize()
    {
        return allies.Count;
    }


}
