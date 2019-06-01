using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEnemy : Sense {

    private void Start()
    {
        pla = transform.parent.GetComponent<Player>();
    }

    void OnTriggerEnter(Collider other){
	
		Aspect aspect = other.GetComponent<Aspect> ();
		if (aspect != null && aspect.alive && aspect.aspectAct == Aspect.aspect.Sound && aspect.teamAct != pla.teamAct)
        {
            pla.addSound(aspect);
            pla.OrderByDistance("sound");
        }
	}

    private void OnTriggerExit(Collider other)
    {
        Aspect aspect = other.GetComponent<Aspect>();

		if (aspect != null && aspect.alive && aspect.aspectAct == Aspect.aspect.Sound && aspect.teamAct != pla.teamAct)
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
