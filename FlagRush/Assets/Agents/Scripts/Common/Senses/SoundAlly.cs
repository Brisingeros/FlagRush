using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAlly : Sense
{
    Player nurse;

    private void Start()
    {
        nurse = GetComponentInParent<Player>();
    }

    void OnTriggerEnter(Collider other)
    {
        Aspect aspect = other.GetComponent<Aspect>();

        if (aspect != null && !aspect.alive && aspect.aspectAct == Aspect.aspect.Sound && aspect.teamAct == nurse.teamAct)
        	nurse.addSound(aspect);
    }

    private void OnTriggerExit(Collider other)
    {
        Aspect aspect = other.GetComponent<Aspect>();

        if (aspect != null && !aspect.alive && aspect.aspectAct == Aspect.aspect.Sound && aspect.teamAct == nurse.teamAct)
            nurse.removeSound(other.GetComponent<Aspect>());
    }

	void FixedUpdate()
    {
        nurse.removeDestroyedSounds("ally");
    }

}
