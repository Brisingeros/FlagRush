using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectiveAlly : Sense {

	Nurse nurse;

	private void Start()
	{
		nurse = GetComponentInParent<Nurse>();
		targetAspect = Aspect.aspect.NPC;
		targetTeam = nurse.getTeam ();
		targetAlive = false;
	}

	void OnTriggerEnter(Collider other){
		Player al = other.GetComponent<Player>();

		if(assertPerception(al))
			nurse.addAlly(al);
	}

	private void OnTriggerExit(Collider other)
	{
		Player al = other.GetComponent<Player>();

		if (assertPerception(al)) // || other.CompareTag("tomb")
			nurse.removeAlly(al);
	}

	private void FixedUpdate()
	{
		nurse.removeSoldiers ("ally");

		for(int i = 0; i < nurse.getAllySize(); i++)
		{
			Player p = nurse.getAlly(i);
			if (p.alive != targetAlive)
			{
				nurse.removeAlly(p);
				i--;
			}
		}
	}
}
