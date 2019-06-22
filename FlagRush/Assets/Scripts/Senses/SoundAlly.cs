using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAlly : Sense
{
	Player nurse;

	private void Start()
	{
		nurse = GetComponentInParent<Player>();

		targetAspect = Aspect.aspect.Sound;
		targetTeam = nurse.getTeam ();
		targetAlive = false;
	}

	void OnTriggerEnter(Collider other)
	{
		Aspect aspect = other.GetComponent<Aspect>();

		if (assertPerception(aspect))
			nurse.addSound(aspect);
	}

	private void OnTriggerExit(Collider other)
	{
		Aspect aspect = other.GetComponent<Aspect>();

		if (assertPerception(aspect))
			nurse.removeSound(other.GetComponent<Aspect>());
	}

	void FixedUpdate()
	{
		nurse.removeDestroyedSounds("ally");
	}

}
