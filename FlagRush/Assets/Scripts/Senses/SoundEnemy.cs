using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEnemy : Sense {

	Player pla;

	private void Start()
	{
		pla = transform.parent.GetComponent<Player>();

		targetAspect = Aspect.aspect.Sound;
		targetTeam = (pla.getTeam () == Team.team.Blue) ? Team.team.Red : Team.team.Blue;
		targetAlive = true;
	}

	void OnTriggerEnter(Collider other){

		Aspect aspect = other.GetComponent<Aspect> ();
		if (assertPerception(aspect))
		{
			pla.addSound(aspect);
			pla.OrderByDistance("sound");
		}
	}

	private void OnTriggerExit(Collider other)
	{
		Aspect aspect = other.GetComponent<Aspect>();

		if (assertPerception(aspect))
		{
			pla.removeSound(other.GetComponent<Aspect>());
			pla.OrderByDistance("sound");
		}
	}

	void FixedUpdate()
	{
		pla.removeDestroyedSounds("enemy");
	}

}
