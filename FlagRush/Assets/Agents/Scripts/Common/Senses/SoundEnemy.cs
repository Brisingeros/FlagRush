using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEnemy : Sense {

    public Player pla;

    private void Start()
    {
        pla = transform.parent.GetComponent<Player>();

    }
    void OnTriggerEnter(Collider other){
	
		Aspect aspect = other.GetComponent<Aspect> ();

		if (aspect != null) {
			
			if (aspect.alive && aspect.aspectAct == Aspect.aspect.Sound && aspect.teamAct != pla.teamAct)
            {
                pla.addSound(aspect);
                Debug.Log("Enemigo escuchado");

            }

        }
	
	}

    private void OnTriggerExit(Collider other)
    {

        Aspect aspect = other.GetComponent<Aspect>();

        if (aspect != null)
        {

            if (aspect.alive && aspect.aspectAct == Aspect.aspect.Sound && aspect.teamAct != pla.teamAct)
            {
                pla.removeSound(other.GetComponent<Aspect>());
            }

        }

    }

    void Update()
    {
        pla.OrderByDistance("sound");
    }

}
